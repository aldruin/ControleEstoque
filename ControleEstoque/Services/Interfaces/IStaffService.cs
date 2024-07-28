using ControleEstoque.Models.DTOs;

namespace ControleEstoque.Services.Interfaces;

public interface IStaffService
{
    Task<StaffDto?> CreateStaffAsync(StaffDto dto);
    Task<List<StaffDto?>> GetStaffAsync();
    Task<StaffDto?> UpdateStaffAsync(StaffDto dto);
    Task<StaffDto?> DeleteStaffAsync(Guid id);
}