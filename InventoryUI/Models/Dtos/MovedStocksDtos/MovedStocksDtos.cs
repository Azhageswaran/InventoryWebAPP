namespace InventoryUI.Models.Dtos.MovedStocksDtos
{
    public class MovedStocksDtos
    {
        public Guid MovedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExitDate { get; set; }
    }
}
