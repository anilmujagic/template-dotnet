namespace MyApp.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task Commit();
}
