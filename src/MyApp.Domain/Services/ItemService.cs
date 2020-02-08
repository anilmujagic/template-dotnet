using System.Collections.Generic;
using System.Linq;
using MyApp.Core.Dtos;
using MyApp.Core.Interfaces;
using MyApp.Core.Models;
using MyApp.Domain.Common;

namespace MyApp.Domain.Services
{
    public class ItemService : DataService, IItemService
    {
        public ItemService(
            IUnitOfWorkFactory unitOfWorkFactory,
            IRepositoryFactory repositoryFactory)
            : base(unitOfWorkFactory, repositoryFactory)
        {
        }

        public IEnumerable<ItemDto> GetItems(string name)
        {
            using var uow = NewUnitOfWork();
            return NewRepository<Item>(uow)
                .GetAs(
                    i => i.Name.Contains(name),
                    i => new ItemDto
                    {
                        ItemId = i.ItemId,
                        Name = i.Name,
                        IsProcessed = i.IsProcessed
                    })
                .OrderBy(i => i.Name);
        }
    }
}
