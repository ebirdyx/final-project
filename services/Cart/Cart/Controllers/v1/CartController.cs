using System.Net;
using Cart.Repository;
using Cart.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;
    private readonly DiscountService _discountService;

    public CartController(ICartRepository repository, DiscountService discountService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
    }

    [HttpGet("{userName}", Name = "GetCart")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Entities.Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Cart>> GetCart(string userName)
    {
        return Ok(await _repository.GetCart(userName));
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Entities.Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.Cart>> UpdateCart([FromBody] Entities.Cart cart)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await _discountService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }
        
        return Ok(await _repository.UpdateCart(cart));
    }
    
    [HttpDelete("{userName}", Name = "DeleteCart")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Entities.Cart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCart(string userName)
    {
        await _repository.DeleteCart(userName);
        return Ok();
    }
}