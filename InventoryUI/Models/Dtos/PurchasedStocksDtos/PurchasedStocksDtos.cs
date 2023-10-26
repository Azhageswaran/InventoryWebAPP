namespace InventoryUI.Models.Dtos.PurchasedStocksDtos
{
    public class PurchasedStocksDtos
    {
        public Guid PurchasedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
