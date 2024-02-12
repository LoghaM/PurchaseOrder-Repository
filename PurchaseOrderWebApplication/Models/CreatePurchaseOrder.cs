using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class CreatePurchaseOrder
    {
       
        [Required]
        [StringLength(20)]
        public string? PurchaseFromLocation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal ShipmentCost { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
