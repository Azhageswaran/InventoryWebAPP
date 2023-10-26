using Inventory.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<RawMaterials> RawMaterials  { get; set; }
        public DbSet<PurchasedStocks> PurchasedStocks { get; set; }
        public DbSet<MovedStocks> MovedStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawMaterials>()
                .HasKey(m => m.RawMaterialId);

            modelBuilder.Entity<MovedStocks>()
                .HasKey(m => m.MovedStockId); // Assuming 'Id' is the primary key property

            modelBuilder.Entity<PurchasedStocks>()
                .HasKey(m => m.PurchasedStockId);
        }
    }
}
