using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.Core;
using MyApp.Core.Enums;
using MyApp.Core.Interfaces;
using MyApp.Infrastructure.Data.EF;

namespace MyApp.Infrastructure.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create(UnitOfWorkMode mode = UnitOfWorkMode.ReadOnly)
        {
            var options = new DbContextOptionsBuilder()
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
}
