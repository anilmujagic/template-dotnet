////////////////////////////////////////////////////////////////////////////////////////////////////
// THIS CODE IS GENERATED - DO NOT CHANGE IT MANUALLY!
////////////////////////////////////////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using MyApp.Core.Models;

namespace MyApp.Infrastructure.Data.EF;

public partial class AppDb : DbContext
{
    public virtual DbSet<Item> Items { get; set; }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        // Item
        var item = modelBuilder.Entity<Item>()
            .ToTable("item", "app");
        item.HasKey(c => c.ItemId);
        item.Property(e => e.ItemId)
            .HasColumnName("item_id")
            .IsRequired();
        item.Property(e => e.Name)
            .HasColumnName("name")
            .IsRequired();
        item.Property(e => e.IsProcessed)
            .HasColumnName("is_processed")
            .IsRequired();
    }
}
