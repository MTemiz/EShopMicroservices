using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext, ILogger<GetOrdersByNameHandler> logger)
: IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
        .Include(c => c.OrderItems)
        .AsNoTracking()
        .Where(o => o.OrderName.Value.Contains(request.Name))
        .OrderBy(o => o.OrderName.Value)
        .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult(orders.ToOrderDtoList());
    }
}