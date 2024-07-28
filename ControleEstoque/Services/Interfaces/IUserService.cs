using System.Security.Claims;
using ControleEstoque.Models.DTOs;

namespace ControleEstoque.Services.Interfaces;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(UserDto dto);
    Task<UserDto?> DeleteUserAsync(ClaimsPrincipal user);
    Task<UserDto?> UpdateUserAsync(ClaimsPrincipal user, UserDto dto);
}