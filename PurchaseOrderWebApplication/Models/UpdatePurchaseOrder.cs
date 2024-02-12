using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class UpdatePurchaseOrder
    {
        [Required]
        [Range(1, 100)]
        public int PurchaseId { get; set; }
    }
}
