// Entities/ProductVariant.cs
namespace KunuzStore.Domain.Entities;

public class ProductVariant
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public string Label { get; set; } = default!;
    public ICollection<ProductVariantOption> Options { get; set; } = new List<ProductVariantOption>();
}

public class ProductVariantOption
{
    public Guid Id { get; set; }
    public Guid ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = default!;

    public string Value { get; set; } = default!;
}