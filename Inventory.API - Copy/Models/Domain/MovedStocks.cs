using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory.API.Models.Domain
{
    public class MovedStocks
    {
        //[Key]
        public Guid MovedStockId { get; set; }
        public Guid RawMaterialId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExitDate { get; set; }


        // Navigation property
        [ForeignKey("RawMaterialId")]
        public RawMaterials RawMaterials { get; set; }
    }
}
