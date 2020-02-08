using MyApp.Core.Interfaces;

namespace MyApp.Infrastructure.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> Create<T>(IUnitOfWork unitOfWork)
            where T : class
        {
            return new Repository<T>((unitOfWork as UnitOfWork).Db);
        }
    }
}
