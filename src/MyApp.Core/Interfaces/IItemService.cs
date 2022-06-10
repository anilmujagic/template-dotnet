using MyApp.Core.Dtos;

namespace MyApp.Core.Interfaces;

public interface IItemService
{
    IEnumerable<ItemDto> GetItems(string name);
}
