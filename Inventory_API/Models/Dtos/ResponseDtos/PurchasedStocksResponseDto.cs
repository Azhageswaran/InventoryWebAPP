namespace Inventory_API.Models.Dtos.ResponseDtos
{
    public class PurchasedStocksResponseDto
    {
        public Guid PurchasedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }

        public int AvailableStocks {  get; set; }
    }
}
