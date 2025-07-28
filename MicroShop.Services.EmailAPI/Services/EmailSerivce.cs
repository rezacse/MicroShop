using MicroShop.Services.EmailAPI.Dtos;

namespace MicroShop.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }

    public class EmailSerivce : IEmailService
    {
        private readonly IEmailRepository emailRepository;

        public EmailSerivce(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            //SEND EMAIL LOGIC HERE


            //ADD LOGS
            var dto = new AddEmailDto(to, subject, body);
            await emailRepository.AddEmailLog(dto);

            return true;
        }
    }
}
