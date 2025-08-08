using Azure.Messaging.ServiceBus;
using MicroShop.Services.EmailAPI.Services;
using System.Text;

namespace MicroShop.Services.EmailAPI.Messaging
{
    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private readonly string? connectionString;
        private readonly string emailQueue;
        private readonly ServiceBusProcessor emailCartProcessor;
        private readonly ILogger logger;
        private readonly IEmailService emailService;

        public ServiceBusConsumer(
            IConfiguration configuration
            , ILogger logger
            , IEmailService emailService)
        {
            connectionString = configuration.GetConnectionString("ServiceBusConnection") ?? throw new Exception("Connection String Missing");
            emailQueue = configuration.GetConnectionString("TOPIC_QUEUE:EMAIL") ?? throw new Exception("Email Queue Name Missing");
            // Initialization logic can go here

            var client = new ServiceBusClient(connectionString);
            emailCartProcessor = client.CreateProcessor(emailQueue, new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            });
            this.logger = logger;
            this.emailService = emailService;
        }


        public async Task Start()
        {
            emailCartProcessor.ProcessMessageAsync += OnEmailRequestReceived;
            emailCartProcessor.ProcessErrorAsync += ErrorHandler;

            await emailCartProcessor.StartProcessingAsync();
        }

        private async Task OnEmailRequestReceived(ProcessMessageEventArgs args)
        {
            try
            {
                var messageBody = Encoding.UTF8.GetString(args.Message.Body);

                var to = "";
                var subject = "";
                var body = "";
                //SERIALIZE THE MESSAGE
                await emailService.SendEmailAsync(to, subject, body);


                // Complete the message
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing message");
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            logger.LogError(args.Exception, "Error processing message");
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await emailCartProcessor.StopProcessingAsync();
            await emailCartProcessor.DisposeAsync();
        }
    }
}
