using Microsoft.AspNetCore.Identity;

namespace ControleEstoque.Models;
public sealed class User : IdentityUser<Guid>
{
    public Staff? Staff { get; set; }
    public User() { }
}