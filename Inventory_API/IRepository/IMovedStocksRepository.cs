using Inventory_API.Models.Domain;

namespace Inventory_API.IRepository
{
    public interface IMovedStocksRepository
    {
        Task<List<MovedStocks>> GetSPMovedStocksAllAsync();

        Task<MovedStocks> CreateAsync(MovedStocks movedStocks);
    }
}
