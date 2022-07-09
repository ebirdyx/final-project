using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cart.Repository;

public class CartRepository : ICartRepository
{
    private readonly IDistributedCache _cache;

    public CartRepository(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<Entities.Cart> GetCart(string userName)
    {
        var cart = await _cache.GetStringAsync(userName);
        
        return (string.IsNullOrEmpty(cart) 
            ? new Entities.Cart(userName) 
            : JsonConvert.DeserializeObject<Entities.Cart>(cart))!;
    }

    public async Task<Entities.Cart> UpdateCart(Entities.Cart cart)
    {
        await _cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
        return await GetCart(cart.UserName);
    }

    public async Task DeleteCart(string userName)
    {
        await _cache.RemoveAsync(userName);
    }
}