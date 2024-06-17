namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventInterceptor(IMediator mediator)
: SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        var aggregates = context
        .ChangeTracker
        .Entries<IAggregate>()
        .Where(c => c.Entity.DomainEvents.Any())
        .Select(c => c.Entity);

        var domainEvents = aggregates
        .SelectMany(c => c.DomainEvents)
        .ToList();

        aggregates
        .ToList()
        .ForEach(c => c.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}