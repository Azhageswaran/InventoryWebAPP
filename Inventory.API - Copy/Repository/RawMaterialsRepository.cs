using Inventory.API.Data;
using Inventory.API.IRepository;
using Inventory.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Repository
{
    public class RawMaterialsRepository : IRawMaterialsRepository
    {
        private readonly InventoryDbContext dbContext;

        public RawMaterialsRepository(InventoryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RawMaterials> CreateAsync(RawMaterials rawMaterials)
        {
            await dbContext.RawMaterials.AddAsync(rawMaterials);
            await dbContext.SaveChangesAsync();

            return rawMaterials;
        }

        public async Task<RawMaterials?> DeleteAsync(Guid id)
        {
            var existingRawMaterials = await dbContext.RawMaterials.FirstOrDefaultAsync(x => x.RawMaterialId == id);

            if(existingRawMaterials == null) 
            {
                return null;    
            }

            dbContext.RawMaterials.Remove(existingRawMaterials);
            await dbContext.SaveChangesAsync();

            return existingRawMaterials;
        }

        public async Task<RawMaterials> GetRawMaterialsByID(Guid id)
        {
            return await dbContext.RawMaterials.FindAsync(id);
        }

        public async Task<List<RawMaterials>> GetSPAllAsync()
        {
            return await dbContext.RawMaterials.FromSqlRaw("EXEC GetRawMaterials").ToListAsync();
        }

      /*  public async Task<int> GetSpCount()
        {
            return await dbContext.Database.ExecuteSqlAsync("Exec GetRawMaterialCount");
        }*/

        public async Task<RawMaterials> UpdateAsync(Guid id, RawMaterials rawMaterials)
        {
            var existingRawMaterials = await dbContext.RawMaterials.FirstOrDefaultAsync(x => x.RawMaterialId == id);
            if(existingRawMaterials == null) 
            {
                return null;
            }

            existingRawMaterials.RawMaterialName = rawMaterials.RawMaterialName;
            existingRawMaterials.Price = rawMaterials.Price;
            existingRawMaterials.AvailableStocks = rawMaterials.AvailableStocks;
            existingRawMaterials.Supplier = rawMaterials.Supplier;
            existingRawMaterials.Description = rawMaterials.Description;

            
            await dbContext.SaveChangesAsync();
            return existingRawMaterials;
        }
    }
}
