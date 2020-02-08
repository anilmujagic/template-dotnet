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
                    c => c.Name.Contains(name),
                    c => new ItemDto
                    {
                        ItemId = c.ItemId,
                        Name = c.Name
                    })
                .OrderBy(c => c.Name);
        }
    }
}
