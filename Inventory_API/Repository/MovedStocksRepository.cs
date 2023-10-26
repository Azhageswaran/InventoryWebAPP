using Inventory_API.Data;
using Inventory_API.IRepository;
using Inventory_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Repository
{
    public class MovedStocksRepository : IMovedStocksRepository
    {
        private readonly InventoryDbContext dbContext;

        public MovedStocksRepository(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MovedStocks> CreateAsync(MovedStocks movedStocks)
        {
            await dbContext.MovedStocks.AddAsync(movedStocks);
            await dbContext.SaveChangesAsync();
            return movedStocks;
        }

        public async Task<List<MovedStocks>> GetSPMovedStocksAllAsync()
        {
           return await dbContext.MovedStocks.FromSqlRaw("Exec GetMovedStocks").ToListAsync();
        }
    }
}
