using Microsoft.Extensions.Logging;
using Orders.Domain.Entities;

namespace Orders.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(GetPreconfiguredOrders());
            await context.SaveChangesAsync();
            logger.LogInformation("Orders database seeded!");
        }
    }

    public static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "swn",
                FirstName = "Pamela",
                LastName = "Braholli",
                EmailAddress = "ebirdyxx@gmail.com",
                AddressLine = "Tirana",
                Country = "Albania",
                TotalPrice = 1035
            }
        };
    }
}