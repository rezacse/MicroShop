using Microsoft.Identity.Client;
using RabbitMQ.Client;

namespace MicroShop.Services.AuthAPI.MessageSender
{
    public interface IAppMessageSender
    {
        Task SendMessage(string queueName, object message);
    }

    public class AppMessageSender : IAppMessageSender
    {
        private readonly ILogger<IAppMessageSender> logger;
        private string hostName;
        private string username;
        private string password;

        public AppMessageSender(ILogger<IAppMessageSender> logger, IConfiguration configuration)
        {
            hostName = configuration.GetValue<string>("RabbitMQ:Host") ?? string.Empty;
            username = configuration.GetValue<string>("RabbitMQ:UserName") ?? string.Empty;
            password = configuration.GetValue<string>("RabbitMQ:Password") ?? string.Empty;
           

            if (string.IsNullOrEmpty(hostName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("RabbitMQ configuration is not properly set.");
            }

            this.logger = logger;
        }
        public async Task SendMessage(string queueName, object message)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = hostName,
                    UserName = username,
                    Password = password
                };

                using IConnection connection = await factory.CreateConnectionAsync();
                using IChannel channel = await connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                var body = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
                //await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
                var props = new BasicProperties
                {
                    ContentType = "text/plain",
                    DeliveryMode = DeliveryModes.Persistent
                };
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName,
                    mandatory: true, basicProperties: props, body: body);
                logger.LogInformation("Message sent to queue {QueueName}.", queueName);

                await connection.CloseAsync();
                await channel.CloseAsync();
            }
            catch (Exception)
            {
                logger.LogError("Failed to send message to queue {QueueName}.", queueName);
            }
        }
    }
}
