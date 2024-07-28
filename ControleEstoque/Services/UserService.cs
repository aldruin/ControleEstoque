using System.Security.Claims;
using ControleEstoque.Infrastructure;
using ControleEstoque.Models;
using ControleEstoque.Models.DTOs;
using ControleEstoque.Services.Interfaces;
using ControleEstoque.Services.Interfaces.Notifications;
using ControleEstoque.Services.Validators;
using Microsoft.AspNetCore.Identity;

namespace ControleEstoque.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly INotificationHandler _notificationHandler;

    public UserService(UserManager<User> userManager, INotificationHandler notificationHandler)
    {
        _userManager = userManager;
        _notificationHandler = notificationHandler;
    }

    public async Task<UserDto?> CreateUserAsync(UserDto dto)
    {
        var validationResult = await new UserValidator().ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                _notificationHandler.AddNotification("InvalidUser", error.ErrorMessage);
            return null;
        }

        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Name.Trim().ToLower()
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (result.Succeeded)
        {
            _notificationHandler.AddNotification("UserCreated", "Usuario criado com sucesso.");
            return new UserDto()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
            };
        }

        foreach (var error in result.Errors)
            _notificationHandler.AddNotification("UserCreationFailed", $"Falha ao criar o usuario: {error.Description}");
        return null;
    }

    public async Task<UserDto> DeleteUserAsync(ClaimsPrincipal user)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        if (currentUser == null)
        {
            _notificationHandler.AddNotification("UserDeletionFailed", "O usuario nao pode ser encontrado para exclus�o.");
            return null;
        }

        var result = await _userManager.DeleteAsync(currentUser);
        if (result.Succeeded)
        {
            _notificationHandler.AddNotification("UserDeleted", "O usuario foi excluido com sucesso.");
            return new UserDto()
            {
                Id = currentUser.Id,
                Name = currentUser.UserName,
                Email = currentUser.Email,
                Password = currentUser.PasswordHash
            };
        }

        foreach (var error in result.Errors)
            _notificationHandler.AddNotification("UserDeletionFailed", $"Falha ao excluir o usuario: {error.Description}");
        return null;
    }

    public async Task<UserDto> UpdateUserAsync(ClaimsPrincipal user, UserDto dto)
    {
        var validationResult = await new UserValidator().ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
                _notificationHandler.AddNotification("InvalidUser", error.ErrorMessage);
            return null;
        }

        var currentUser = await _userManager.GetUserAsync(user);
        if (currentUser == null)
        {
            _notificationHandler.AddNotification("UserNotFound", "O usuario nao pode ser encontrado.");
            return null;
        }
        var updatedUser = new User() { UserName = dto.Name.Trim().ToLower(), Email = dto.Email, Id = currentUser.Id };
        var result = await _userManager.UpdateAsync(updatedUser);
        if (result.Succeeded)
        {
            _notificationHandler.AddNotification("UserUpdated", "Os  dados de usuario foram atualizados com sucesso.");
            return new UserDto()
            {
                Id = currentUser.Id,
                Name = currentUser.UserName,
                Email = currentUser.Email,
                Password = currentUser.PasswordHash
            };
        }
        foreach (var error in result.Errors)
            _notificationHandler.AddNotification("UserUpdateFailed", $"Falha ao atualizar o usuario: {error.Description}");
        return null;
    }
}
