using ControleEstoque.Models.Enums;

namespace ControleEstoque.Models;
public sealed class Staff
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public StaffRole? Role { get; set; }
    public List<Item>? Items { get; set; } = new List<Item>();
    public Staff() { }
}