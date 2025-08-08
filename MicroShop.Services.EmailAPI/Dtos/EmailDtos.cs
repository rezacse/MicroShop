namespace MicroShop.Services.EmailAPI.Dtos
{
    public class AddEmailDto
    {
        public AddEmailDto(string to, string subject, string body)
        {
            ToEmail = to;
            Subject = subject;
            Body = body;
        }

        public string ToEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class EmailMsgDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
