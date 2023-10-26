namespace InventoryUI.Models.Dtos.PurchasedStocksDtos
{
    public class AddPurchasedStocksViewModel
    {
        public Guid? RawMaterialId { get; set; }
        public int Quantity { get; set; }
    }
}
