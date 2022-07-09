namespace Cart.Entities;

public class Cart
{
    public string UserName { get; set; }

    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public Cart()
    {
    }

    public Cart(string userName)
    {
        UserName = userName;
    }

    public decimal TotalPrice => Items
        .Select(i => i.Price * i.Quantity)
        .Aggregate((a, b) => a + b);
}