using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory_API.Models.Dtos.RequestDtos
{
    public class AddRawMaterialsRequestDto
    {
        [Required]
        public string RawMaterialName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int AvailableStocks { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        /*        public DateTime createdOn { get; set; }*/
    }
}
