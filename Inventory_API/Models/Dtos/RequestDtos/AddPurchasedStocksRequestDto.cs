namespace Inventory_API.Models.Dtos.RequestDtos
{
    public class AddPurchasedStocksRequestDto
    {
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
    }
}
