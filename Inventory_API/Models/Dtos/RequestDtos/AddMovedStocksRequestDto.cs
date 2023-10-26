namespace Inventory_API.Models.Dtos.RequestDtos
{
    public class AddMovedStocksRequestDto
    {
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
    }
}
