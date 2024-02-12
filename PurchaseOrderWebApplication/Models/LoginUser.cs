using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class LoginUser
    {
        [Required]
        [StringLength(20)]
        public string? username { get; set; }
        [Required]
        public string? passwd { get; set; }
        [Required]
        public string? TableAccessName { get; set; }
    }
}
