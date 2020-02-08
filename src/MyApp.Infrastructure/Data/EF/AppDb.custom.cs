using Microsoft.EntityFrameworkCore;

namespace MyApp.Infrastructure.Data.EF
{
    public partial class AppDb
    {
        public AppDb(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ConfigureEntities(modelBuilder);
        }
    }
}
