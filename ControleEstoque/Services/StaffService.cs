using ControleEstoque.Infrastructure;
using ControleEstoque.Models;
using ControleEstoque.Models.DTOs;

using ControleEstoque.Services.Interfaces;
using ControleEstoque.Services.Interfaces.Notifications;
using ControleEstoque.Services.Validators;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Services;

public class StaffService : IStaffService
{
    private readonly AppDbContext _db;
    private readonly INotificationHandler _notificationHandler;

    public StaffService(AppDbContext db, INotificationHandler notificationHandler)
    {
        _db = db;
        _notificationHandler = notificationHandler;
    }

    public async Task<StaffDto?> CreateStaffAsync(StaffDto dto)
    {
        try
        {
            var validation = await new StaffValidator().ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _notificationHandler.AddNotification("InvalidStaff", error.ErrorMessage);
                return null;
            }
            var staff = new Staff()
            {
                Name = dto.Name,
                Role = dto.Role
            };

            await _db.AddAsync(staff);
            await _db.SaveChangesAsync();

            _notificationHandler.AddNotification("StaffCreated", "Equipe criada com sucesso");
            return new StaffDto()
            {
                Name = staff.Name,
                Role = staff.Role
            };
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("StaffCreationFailed", $"Falha ao criar a equipe: {ex.Message}");
            return null;
        }
    }

    public async Task<StaffDto?> DeleteStaffAsync(Guid id)
    {
        try
        {
            var staff = await _db.Staff.FindAsync(id);
            if (staff == null)
            {
                _notificationHandler.AddNotification("StaffDeletionFailed", "A equipe nao pode ser encontrada para exclusao");
                return null;
            }

            _db.Staff.Remove(staff);
            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                _notificationHandler.AddNotification("StaffDeleted", "A equipe foi excluída com sucesso.");
                return new StaffDto()
                {
                    Name = staff.Name,
                    Role = staff.Role
                };
            }
            _notificationHandler.AddNotification("StaffDeletionFailed", "Falha ao deletar a equipe.");
            return null;
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("StaffDeletionFailed", $"Falha ao deletar a equipe: {ex.Message}");
            return null;
        }
    }

    public async Task<List<StaffDto?>> GetStaffAsync()
    {
        try
        {
            var staffList = await _db.Staff.ToListAsync();
            if (!staffList.Any())
            {
                _notificationHandler.AddNotification("StaffNotFound", "Nenhuma equipe encontrada.");
                return null;
            }

            var staffDtoList = staffList.Select(staff => new StaffDto
            {
                Name = staff.Name,
                Role = staff.Role,
                Items = staff.Items
            }).ToList();

            return staffDtoList;

        }
        catch (Exception ex)
        {

            _notificationHandler.AddNotification("StaffFetchFailed", $"Falha ao buscar a equipe: {ex.Message}");
            return null;
        }
    }

    public async Task<StaffDto?> UpdateStaffAsync(StaffDto dto)
    {
        try
        {
            var validation = await new StaffValidator().ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _notificationHandler.AddNotification("InvalidStaff", error.ErrorMessage);
                return null;
            }

            var staff = await _db.Staff.FindAsync(dto.Id);
            if (staff == null)
            {
                _notificationHandler.AddNotification("StaffNotFound", "A equipe não foi encontrada");
                return null;
            }

            staff.Name = dto.Name;
            staff.Role = dto.Role;

            _db.Staff.Update(staff);
            await _db.SaveChangesAsync();

            _notificationHandler.AddNotification("StaffUpdated", "Equipe atualizada com sucesso");
            return new StaffDto()
            {
                Name = staff.Name,
                Role = staff.Role,
                Items = staff.Items
            };
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("StaffUpdateFailed", $"Falha ao atualizar a equipe: {ex.Message}");
            return null;
        }
    }
}