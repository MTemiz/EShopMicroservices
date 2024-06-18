namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageSize = query.PaginationRequest.PageSize;

        var pageIndex = query.PaginationRequest.PageIndex;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext
        .Orders
        .Include(c => c.OrderItems)
        .OrderBy(c => c.OrderName.Value)
        .Skip(pageSize * pageIndex)
        .Take(pageSize).ToListAsync();

        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
    }
}