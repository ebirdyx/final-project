using Dapper;
using Discount.Common.Configuration;
using Discount.Common.Entities;
using Discount.Repository;
using Npgsql;

namespace Discount.Common.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly DatabaseSettings _databaseSettings;

    public DiscountRepository(DatabaseSettings databaseSettings)
    {
        _databaseSettings = databaseSettings ??
                            throw new ArgumentNullException(nameof(databaseSettings));
    }
    
    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var conn = new NpgsqlConnection(_databaseSettings.ConnectionString);

        var coupon = await conn.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", 
                new {ProductName = productName});

        if (coupon == null)
            return new Coupon
            {
                ProductName = "No Discount",
                Description = "No Discount Description",
                Amount = 0
            };

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var conn = new NpgsqlConnection(_databaseSettings.ConnectionString);

        var affected =
            await conn.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                });

        return affected != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var conn = new NpgsqlConnection(_databaseSettings.ConnectionString);

        var affected =
            await conn.ExecuteAsync(
                "UPDATE Coupon Set ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
                new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount,
                    coupon.Id
                });

        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var conn = new NpgsqlConnection(_databaseSettings.ConnectionString);

        var affected =
            await conn.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName=@ProductName",
                new { ProductName = productName });

        return affected != 0;
    }
}