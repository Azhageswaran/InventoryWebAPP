namespace Inventory_API.Models.Dtos.ResponseDtos
{
    public class MovedStocksResponseDto
    {
        public Guid MovedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExitDate { get; set; }
        public int AvailableStocks { get; set; }
    }
}
