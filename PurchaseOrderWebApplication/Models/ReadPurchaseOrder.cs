using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class ReadPurchaseOrder
    {

        [Required]
        [Range(1, 100)]
        public int PurchaseId { get; set; }
    }
}
