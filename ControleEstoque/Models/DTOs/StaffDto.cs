using ControleEstoque.Models.Enums;

namespace ControleEstoque.Models.DTOs;
public class StaffDto
{
    public Guid? Id {get; set;}
    public string? Name { get; set; }
    public StaffRole? Role { get; set; }
    public List<Item>? Items { get; set; }
    public StaffDto() { }
}