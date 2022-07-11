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
                EmailAddress = "noreply@orcuslab.com",
                AddressLine = "Tirana",
                Country = "Albania",
                TotalPrice = 1035,
                State = "CA",
                ZipCode = "5000",
                CardName = "Visa",
                CardNumber = "4333433343334333",
                Expiration = "0325",
                CVV = "433",
                PaymentMethod = 1,
                CreatedBy = "swn",
                CreatedDate = DateTime.Now,
                LastModifiedBy = "swn",
                LastModifiedDate = DateTime.Now
            }
        };
    }
}