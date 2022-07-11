using System.Net;
using AutoMapper;
using Cart.Repository;
using Cart.Services;
using MassTransit;
using Messaging.Events;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;
    private readonly DiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public CartController(ICartRepository repository, DiscountService discountService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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

    [HttpPost]
    [Route("[action]")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CheckoutCart([FromBody] CartCheckout cartCheckout)
    {
        var cart = await _repository.GetCart(cartCheckout.UserName);
        if (cart == null)
        {
            return BadRequest();
        }

        var msg = _mapper.Map<CartCheckoutEvent>(cartCheckout);
        msg.TotalPrice = cart.TotalPrice;
        await _publishEndpoint.Publish(msg);

        await _repository.DeleteCart(cartCheckout.UserName);
        return Accepted();
    }
}