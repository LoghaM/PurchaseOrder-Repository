using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class PurchaseOrder
    {
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public string? PurchaseFromLocation { get; set; }
        public string? PurchaseToLocation { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal ShipmentCost { get; set; }
        public decimal TotalAmount { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
