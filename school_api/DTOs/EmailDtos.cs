using System.ComponentModel.DataAnnotations;

namespace school_api.DTOs
{
    public class EmailDto
    {
        [Required(ErrorMessage = "To email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string To { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; } = string.Empty;

        public bool IsHtml { get; set; } = true;
        public List<string>? Cc { get; set; }
        public List<string>? Bcc { get; set; }
    }

    public class NotificationEmailDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Notification type is required")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; } = string.Empty;

        public Dictionary<string, object>? Data { get; set; }
    }
}
