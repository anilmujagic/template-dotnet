using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Core;
using MyApp.Core.Enums;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data.EF;

namespace MyApp.Infrastructure.Data;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
#if DEBUG
    private static readonly ILoggerFactory EfLoggerFactory =
        LoggerFactory.Create(builder =>
        {
            builder
                //.AddFilter(level => level >= LogLevel.Warning)
                .AddConsole();
        });
#endif

    public IUnitOfWork Create(UnitOfWorkMode mode = UnitOfWorkMode.ReadOnly)
    {
        var options = new DbContextOptionsBuilder()
#if DEBUG
            .UseLoggerFactory(EfLoggerFactory)
            .EnableSensitiveDataLogging()
#endif
            .UseNpgsql(Config.DB)
            .Options;

        var db = new AppDb(options);

        db.ChangeTracker.LazyLoadingEnabled = false;

        if (mode == UnitOfWorkMode.ReadOnly)
        {
            db.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        return new UnitOfWork(db, mode);
    }
}