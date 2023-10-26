using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_API.Models.Domain
{
    public class PurchasedStocks
    {
        //[Key]
        public Guid PurchasedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }


        // Navigation property
        [ForeignKey("RawMaterialId")]
        public RawMaterials RawMaterials { get; set; }
    }
}
