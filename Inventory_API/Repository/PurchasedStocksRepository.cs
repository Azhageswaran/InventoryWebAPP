using Inventory_API.Data;
using Inventory_API.IRepository;
using Inventory_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Repository
{
    public class PurchasedStocksRepository : IPurchasedStocksRepository
    {
        private readonly InventoryDbContext dbContext;

        public PurchasedStocksRepository(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PurchasedStocks> CreateAsync(PurchasedStocks purchasedStocks)
        {
           await dbContext.PurchasedStocks.AddAsync(purchasedStocks);
           await dbContext.SaveChangesAsync();

            return purchasedStocks;
        }

        public async Task<List<PurchasedStocks>> GetAllPurchasedStocksAsync()
        {
           return await dbContext.PurchasedStocks.FromSqlRaw("EXEC GetPurchasedStocks").ToListAsync();
        }
    }
}
