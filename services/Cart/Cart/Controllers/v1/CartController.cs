using System.Net;
using Cart.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;

    public CartController(ICartRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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