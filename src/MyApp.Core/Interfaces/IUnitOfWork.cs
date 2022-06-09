using System;

namespace MyApp.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}