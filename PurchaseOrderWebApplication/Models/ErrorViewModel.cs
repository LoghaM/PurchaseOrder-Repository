using System.Text.Json;

namespace PurchaseOrderWebApplication.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }

        public ErrorViewModel( int statusCode, string? message, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }

    }

