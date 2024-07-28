using ControleEstoque.Models.DTOs;

namespace ControleEstoque.Services.Interfaces;

public interface IItemService
{
    Task<ItemDto?> CreateItemAsync(ItemDto dto);
    Task<List<ItemDto?>> GetItemsByStaffIdAsync(Guid id);
    Task<ItemDto?> DeleteItemAsync(Guid id);
    Task<ItemDto?> UpdateItemAsync(ItemDto dto);
}