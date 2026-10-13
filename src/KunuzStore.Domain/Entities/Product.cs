// Entities/Product.cs
namespace KunuzStore.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public string Description { get; set; } = default!;
    public bool Featured { get; set; }
    public bool InStock { get; set; }
    public bool IsDeleted { get; set; }

    public byte[] RowVersion { get; set; } = default!;

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}