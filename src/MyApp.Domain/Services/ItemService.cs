using MyApp.Common.Extensions;
using MyApp.Core.Dtos;
using MyApp.Core.Interfaces;
using MyApp.Core.Models;
using MyApp.Domain.Common;

namespace MyApp.Domain.Services;

public class ItemService : DataService, IItemService
{
    public ItemService(
        IUnitOfWorkFactory unitOfWorkFactory,
        IRepositoryFactory repositoryFactory)
        : base(unitOfWorkFactory, repositoryFactory)
    {
    }

    public async Task<IEnumerable<ItemDto>> GetItems(string? nameSearch)
    {
        using var uow = NewUnitOfWork();
        var items = await NewRepository<Item>(uow)
            .GetAs(
                i => nameSearch.IsNullOrWhiteSpace() || i.Name.Contains(nameSearch),
                i => new ItemDto
                {
                    ItemId = i.ItemId,
                    Name = i.Name,
                    IsProcessed = i.IsProcessed
                });

        return items.OrderBy(i => i.Name);
    }
}
