using MyApp.Core.Enums;

namespace MyApp.Core.Interfaces;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create(UnitOfWorkMode mode = UnitOfWorkMode.ReadOnly);
}