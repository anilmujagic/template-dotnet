using System;
using MyApp.Core.Enums;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data.EF;

namespace MyApp.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly UnitOfWorkMode _mode;
    internal AppDb Db { get; }

    public UnitOfWork(AppDb db, UnitOfWorkMode mode)
    {
        Db = db ?? throw new ArgumentNullException(nameof(db));
        _mode = mode;
    }

    public void Commit()
    {
        if (_mode == UnitOfWorkMode.ReadOnly)
            throw new InvalidOperationException("Commit is not allowed in read-only UnitOfWork.");

        Db.SaveChanges();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}