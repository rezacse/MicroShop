using Azure.Messaging.ServiceBus;
using System.Text;

namespace MircoShop.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(string topic, T message) where T : class;
        //Task SubscribeAsync<T>(string topic, Func<T, Task> handler) where T : class;
        //Task UnsubscribeAsync<T>(string topic) where T : class;
    }

    public class MessageBus : IMessageBus
    {
        // Implementation of the IMessageBus methods would go here.
        private readonly string _connectionString ="";
        public async Task PublishAsync<T>(string topic, T message) where T : class
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(topic);
            var jsonMsg = System.Text.Json.JsonSerializer.Serialize(message);
            var finalMsg = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMsg))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMsg);

            await sender.DisposeAsync();
        }


        //public Task SubscribeAsync<T>(string topic, Func<T, Task> handler) where T : class
        //{
        //    throw new NotImplementedException();
        //}
        //public Task UnsubscribeAsync<T>(string topic) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
    
}
