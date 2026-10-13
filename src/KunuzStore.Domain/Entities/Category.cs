namespace KunuzStore.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Icon { get; set; } = default!;
    public string Color { get; set; } = default!;

    public Guid? ParentCategoryId { get; set; }      // null = قسم رئيسي
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();

    public ICollection<Product> Products { get; set; } = new List<Product>();
}