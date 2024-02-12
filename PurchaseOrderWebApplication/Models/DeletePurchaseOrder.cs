using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class DeletePurchaseOrder
    {

        [Required]
        [Range(1, 100)]
        public int PurchaseId { get; set; }
    }
}
