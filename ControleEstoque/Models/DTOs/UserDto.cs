namespace ControleEstoque.Models.DTOs;
public class UserDto
{
    public Guid? Id {get; set;}
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UserDto() { }
}