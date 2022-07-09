using Discount.gRPC.Protos;

namespace Cart.Services;

public class DiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountClient;

    public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountClient)
    {
        _discountClient = discountClient;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var getDiscountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountClient.GetDiscountAsync(getDiscountRequest);
    }
}