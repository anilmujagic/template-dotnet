using MyApp.Core.Dtos;

namespace MyApp.Core.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetItems(string? name);
}
