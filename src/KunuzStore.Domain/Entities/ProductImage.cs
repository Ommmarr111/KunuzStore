namespace KunuzStore.Domain.Entities;

public class ProductImage
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public string Url { get; set; } = default!;
    public int SortOrder { get; set; }
}