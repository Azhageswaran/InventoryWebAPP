using Inventory_API.Models.Domain;

namespace Inventory_API.IRepository
{
    public interface IPurchasedStocksRepository
    {
        Task<List<PurchasedStocks>> GetAllPurchasedStocksAsync();

        Task<PurchasedStocks> CreateAsync(PurchasedStocks purchasedStocks);
    }
}
