using MicroShop.Services.EmailAPI.Dtos;
using MicroShop.Services.EmailAPI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MicroShop.Services.EmailAPI.Messaging
{
    public class OrderTopicConsumer : BackgroundService//, IServiceBusConsumer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;
        //private readonly IEmailService emailService;
        private readonly ConnectionFactory factory;
        //private readonly IServiceScopeFactory scopeFactory;       
        //public readonly string queueName;
        private readonly string topicName;

        public OrderTopicConsumer(
        IServiceProvider serviceProvider,
        ILogger<OrderTopicConsumer> logger,
        IConfiguration configuration
            )
        {
            var hostName = configuration.GetValue<string>("RabbitMQ:Host") ?? string.Empty;
            var username = configuration.GetValue<string>("RabbitMQ:UserName") ?? string.Empty;
            var password = configuration.GetValue<string>("RabbitMQ:Password") ?? string.Empty;

            topicName = configuration.GetValue<string>("RabbitMQ:TopicName") ?? string.Empty;


            if (string.IsNullOrEmpty(hostName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("RabbitMQ configuration is not properly set.");
            }

            factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = username,
                Password = password
            };
            this.serviceProvider = serviceProvider;
            //this.emailService = emailService;
            //this.scopeFactory = scopeFactory;

            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var scope = scopeFactory.CreateScope();
            //this.logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            //this.emailService = scope.ServiceProvider.GetRequiredService<EmailSerivce>();

            try
            {
                stoppingToken.ThrowIfCancellationRequested();


                IConnection connection = await factory.CreateConnectionAsync(stoppingToken);
                IChannel channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
                await channel.ExchangeDeclareAsync(exchange: topicName, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null, cancellationToken: stoppingToken);

                var queueName = (await channel.QueueDeclareAsync(cancellationToken: stoppingToken)).QueueName;
                await channel.QueueBindAsync(queue: queueName, exchange: topicName, routingKey: "", cancellationToken: stoppingToken);
               
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = System.Text.Encoding.UTF8.GetString(body);
                        var obj = System.Text.Json.JsonSerializer.Deserialize<EmailMsgDto>(message);
                        logger.LogInformation("Received message: {Message}", obj);
                        if (obj == null) return;

                        await ProcessMessageAsync(obj);

                        await channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error processing message.");
                    }
                };

                await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

                //await connection.DisposeAsync();
                //await channel.DisposeAsync();

                //Start();
                logger.LogInformation("RabbitMQ Consumer started successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error starting RabbitMQ Consumer.");
            }
        }

        private async Task ProcessMessageAsync(EmailMsgDto obj)
        {
            using var scope = serviceProvider.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            await emailService.SendEmailAsync(obj.Email, obj.Subject, obj.Body);
        }

        //public Task Start()
        //{

        //    throw new NotImplementedException();
        //}

        //public Task Stop()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
