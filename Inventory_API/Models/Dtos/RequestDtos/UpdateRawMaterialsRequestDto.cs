using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_API.Models.Dtos.RequestDtos
{
    public class UpdateRawMaterialsRequestDto
    {
        public string RawMaterialName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int AvailableStocks { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
    }
}
