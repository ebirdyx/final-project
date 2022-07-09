namespace Cart.Repository;

public interface ICartRepository
{
    public Task<Entities.Cart> GetCart(string userName);

    public Task<Entities.Cart> UpdateCart(Entities.Cart cart);

    public Task DeleteCart(string userName);
}