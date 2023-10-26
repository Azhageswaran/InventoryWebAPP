using Inventory_API.Models.Domain;

namespace Inventory_API.IRepository
{
    public interface IRawMaterialsRepository
    {

        Task<List<RawMaterials>> GetSPAllAsync();

        /*  Task<int> GetSpCount();*/
        Task<RawMaterials> GetRawMaterialsByID(Guid id);

        Task<RawMaterials> CreateAsync(RawMaterials rawMaterials);

        Task<RawMaterials?> UpdateAsync(Guid id, RawMaterials rawMaterials);

        Task<RawMaterials?> DeleteAsync(Guid id);
    }
}
