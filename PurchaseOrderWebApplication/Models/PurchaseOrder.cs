using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class PurchaseOrder
    {
        [Required]
        [Range(1, 100)]
        public int PurchaseId { get; set; }
        [Required]
        [StringLength(20)]
        public string? PurchaseFromLocation { get; set; }
        public string? PurchaseToLocation { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal ShipmentCost { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
