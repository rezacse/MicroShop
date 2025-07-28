namespace MicroShop.Services.EmailAPI.Messaging
{
    public static class AppBuilderExtension
    {
        private static IServiceBusConsumer? serviceBusConsumer { get; set; }
        public static IApplicationBuilder UseServiceBusConsumer(this IApplicationBuilder app)
        {
            serviceBusConsumer = app.ApplicationServices.GetService<IServiceBusConsumer>();
            if(serviceBusConsumer == null)
            {
                throw new InvalidOperationException("ServiceBusConsumer is not registered in the application services.");
            }

            var hostApplicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            hostApplicationLifetime.ApplicationStarted.Register(OnStart);
            hostApplicationLifetime.ApplicationStarted.Register(OnStop);

            app.ApplicationServices.GetRequiredService<IServiceBusConsumer>();
            return app;
        }

        private static void OnStop()
        {
            serviceBusConsumer.Start();
        }

        private static void OnStart()
        {
            serviceBusConsumer.Stop();
        }
    }
}
