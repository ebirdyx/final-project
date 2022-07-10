using Microsoft.EntityFrameworkCore;
using Orders.Application.Contracts.Persistence;
using Orders.Domain.Entities;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orders = await _context.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
        
        return orders;
    }
}