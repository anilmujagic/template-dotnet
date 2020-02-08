using System;
using MyApp.Core.Enums;
using MyApp.Core.Interfaces;

namespace MyApp.Domain.Common
{
    public abstract class DataService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepositoryFactory _repositoryFactory;

        protected DataService(IUnitOfWorkFactory unitOfWorkFactory, IRepositoryFactory repositoryFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        protected IUnitOfWork NewUnitOfWork(UnitOfWorkMode mode = UnitOfWorkMode.ReadOnly)
        {
            return _unitOfWorkFactory.Create(mode);
        }

        protected IRepository<T> NewRepository<T>(IUnitOfWork unitOfWork)
            where T : class
        {
            return _repositoryFactory.Create<T>(unitOfWork);
        }
    }
}
