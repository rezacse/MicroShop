using MicroShop.Services.EmailAPI.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MicroShop.Services.EmailAPI.Tables
{
    public class EmailLog
    {
        public EmailLog()
        {
            
        }
        public EmailLog(AddEmailDto data)
        {
            Email = data.ToEmail;
            Subject = data.Subject;
            Body = data.Body;
            SendOn = DateTime.UtcNow;
        }

        [Key]
        public int ID { get; private set; }

        [Required]
        [StringLength(100)]
        public string Email { get; private set; } = string.Empty;

        [StringLength(500)]
        public string Subject { get; private set; } = string.Empty;

        public string Body { get; private set; } = string.Empty;
        public DateTime SendOn { get; private set; }
    }
}
