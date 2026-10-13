// Entities/AdminUser.cs
namespace KunuzStore.Domain.Entities;

public class AdminUser
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}