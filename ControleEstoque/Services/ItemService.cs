using ControleEstoque.Infrastructure;
using ControleEstoque.Models;
using ControleEstoque.Models.DTOs;
using ControleEstoque.Models.Enums;
using ControleEstoque.Services.Interfaces;
using ControleEstoque.Services.Interfaces.Notifications;
using ControleEstoque.Services.Validators;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Services;

public class ItemService : IItemService
{
    private readonly AppDbContext _db;
    private readonly INotificationHandler _notificationHandler;

    public ItemService(AppDbContext db, INotificationHandler notificationHandler)
    {
        _db = db;
        _notificationHandler = notificationHandler;
    }

    public async Task<ItemDto?> CreateItemAsync(ItemDto dto)
    {
        try
        {
            var validation = await new ItemValidator().ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _notificationHandler.AddNotification("InvalidItem", error.ErrorMessage);
                return null;
            }

            var item = new Item()
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                Description = dto.Description,
                Class = Enum.TryParse<ItemClass>(dto.Class, out var itemClass) ? itemClass : (ItemClass?)null,
                StaffId = dto.StaffId,
                Start = dto.Start,
                End = dto.End
            };

            await _db.Item.AddAsync(item);
            await _db.SaveChangesAsync();

            _notificationHandler.AddNotification("ItemCreated", "Item criado com sucesso");
            return new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                Description = item.Description,
                Class = item.Class?.ToString(),
                StaffId = item.StaffId,
                Start = item.Start,
                End = item.End
            };
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("ItemCreationFailed", $"Falha ao criar o item: {ex.Message}");
            return null;
        }
    }

    public async Task<ItemDto?> DeleteItemAsync(Guid id)
    {
        try
        {
            var item = await _db.Item.FindAsync(id);
            if (item == null)
            {
                _notificationHandler.AddNotification("ItemDeletionFailed", "O item não pode ser encontrado para exclusão");
                return null;
            }

            _db.Item.Remove(item);
            var result = await _db.SaveChangesAsync();

            if (result > 0)
            {
                _notificationHandler.AddNotification("ItemDeleted", "Item excluído com sucesso.");
                return new ItemDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Description = item.Description,
                    Class = item.Class?.ToString(),
                    StaffId = item.StaffId,
                    Start = item.Start,
                    End = item.End
                };
            }

            _notificationHandler.AddNotification("ItemDeletionFailed", "Falha ao deletar o item.");
            return null;
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("ItemDeletionFailed", $"Falha ao deletar o item: {ex.Message}");
            return null;
        }
    }

    public async Task<List<ItemDto?>> GetItemsByStaffIdAsync(Guid staffId)
    {
        try
        {
            var items = await _db.Item
                .Where(i => i.StaffId == staffId)
                .ToListAsync();

            if (!items.Any())
            {
                _notificationHandler.AddNotification("ItemsNotFound", "Nenhum item encontrado para o staff especificado.");
                return null;
            }

            var itemDtoList = items.Select(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                Description = item.Description,
                Class = item.Class?.ToString(),
                StaffId = item.StaffId,
                Start = item.Start,
                End = item.End
            }).ToList();

            return itemDtoList;
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("ItemsFetchFailed", $"Falha ao buscar os itens: {ex.Message}");
            return null;
        }
    }

    public async Task<ItemDto?> UpdateItemAsync(ItemDto dto)
    {
        try
        {
            var validation = await new ItemValidator().ValidateAsync(dto);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _notificationHandler.AddNotification("InvalidItem", error.ErrorMessage);
                return null;
            }

            var item = await _db.Item.FindAsync(dto.Id);
            if (item == null)
            {
                _notificationHandler.AddNotification("ItemNotFound", "O item não foi encontrado");
                return null;
            }

            item.Name = dto.Name;
            item.Quantity = dto.Quantity;
            item.Description = dto.Description;
            item.Class = Enum.TryParse<ItemClass>(dto.Class, out var itemClass) ? itemClass : (ItemClass?)null;
            item.StaffId = dto.StaffId;
            item.Start = dto.Start;
            item.End = dto.End;

            _db.Item.Update(item);
            await _db.SaveChangesAsync();

            _notificationHandler.AddNotification("ItemUpdated", "Item atualizado com sucesso");
            return new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                Description = item.Description,
                Class = item.Class?.ToString(),
                StaffId = item.StaffId,
                Start = item.Start,
                End = item.End
            };
        }
        catch (Exception ex)
        {
            _notificationHandler.AddNotification("ItemUpdateFailed", $"Falha ao atualizar o item: {ex.Message}");
            return null;
        }
    }
}
