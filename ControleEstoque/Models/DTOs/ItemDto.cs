namespace ControleEstoque.Models.DTOs;
public class ItemDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public string? Description { get; set; }
    public string? Class { get; set; }
    public Guid? StaffId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}