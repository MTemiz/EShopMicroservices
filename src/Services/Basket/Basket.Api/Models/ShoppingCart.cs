namespace Basket.Api.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCardItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x=>x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        this.UserName = userName;
    }

    // required for mapping
    public ShoppingCart()
    {

    }
}