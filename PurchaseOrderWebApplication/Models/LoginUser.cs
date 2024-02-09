using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderWebApplication.Models
{
    public class LoginUser
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? passwd { get; set; }
        [Required]
        public string? TableAccessName { get; set; }
    }
}
