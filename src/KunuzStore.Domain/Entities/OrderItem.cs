// Entities/OrderItem.cs
namespace KunuzStore.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    public int Qty { get; private set; }
    public string? VariantLabel { get; private set; }
    public string? VariantChoice { get; private set; }

    private OrderItem() { }   // EF Core

    internal OrderItem(Guid productId, string name, decimal price, int qty,
        string? variantLabel, string? variantChoice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Name = name;
        Price = price;
        Qty = qty;
        VariantLabel = variantLabel;
        VariantChoice = variantChoice;
    }
}