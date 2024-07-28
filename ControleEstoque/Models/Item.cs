using ControleEstoque.Models.Enums;

namespace ControleEstoque.Models;
public sealed class Item
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public ItemClass? Class { get; set; }
    public Staff? Staff { get; set; }
    public Guid? StaffId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}