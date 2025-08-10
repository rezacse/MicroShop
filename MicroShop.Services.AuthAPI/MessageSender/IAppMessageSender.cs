using Microsoft.Identity.Client;
using RabbitMQ.Client;

namespace MicroShop.Services.AuthAPI.MessageSender
{
    public interface IAppMessageSender
    {
        Task SendMessage(string queueName, object message);
        Task SendTopic(string topicName, object message);
    }

    public class AppMessageSender : IAppMessageSender
    {
        private readonly ILogger<IAppMessageSender> logger;
        private readonly ConnectionFactory factory;
        private readonly BasicProperties props;

        public AppMessageSender(ILogger<IAppMessageSender> logger, IConfiguration configuration)
        {
            var hostName = configuration.GetValue<string>("RabbitMQ:Host") ?? string.Empty;
            var username = configuration.GetValue<string>("RabbitMQ:UserName") ?? string.Empty;
            var password = configuration.GetValue<string>("RabbitMQ:Password") ?? string.Empty;
           

            if (string.IsNullOrEmpty(hostName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("RabbitMQ configuration is not properly set.");
            }

            this.logger = logger;

            factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = username,
                Password = password
            };
            props = new BasicProperties
            {
                ContentType = "text/plain",
                DeliveryMode = DeliveryModes.Persistent
            };
        }

        public async Task SendMessage(string queueName, object message)
        {
            try
            {
                using IConnection connection = await factory.CreateConnectionAsync();
                using IChannel channel = await connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName,
                    mandatory: true, basicProperties: props, body: GenerateMsg(message));

                logger.LogInformation("Message sent to queue {QueueName}.", queueName);

                await connection.CloseAsync();
                await channel.CloseAsync();
            }
            catch (Exception ex)
            {
                logger.LogError( ex, "Failed to send message to queue {QueueName}.", queueName);
            }
        }

        private static byte[] GenerateMsg(object message)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message);
        }

        public async Task SendTopic(string topicName, object message)
        {
            try
            {
                using IConnection connection = await factory.CreateConnectionAsync();
                using IChannel channel = await connection.CreateChannelAsync();
                await channel.ExchangeDeclareAsync(exchange: topicName, type: ExchangeType.Fanout, durable: true, autoDelete: false);

                await channel.BasicPublishAsync(exchange: topicName, routingKey: string.Empty,
                    mandatory: true, basicProperties: props, body: GenerateMsg(message));

                logger.LogInformation($"Message sent to Topic {topicName}.");

                await connection.CloseAsync();
                await channel.CloseAsync();
            }
            catch (Exception ex)
            {
                logger.LogError( ex, $"Failed to send message to Topic {topicName}.");
            }
        }
    }
}
