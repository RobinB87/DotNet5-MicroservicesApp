using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.API.RabbitMQ;

namespace Ordering.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusRabbitMQConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStart);
            life.ApplicationStopped.Register(OnStop);

            return app;
        }

        private static void OnStart()
        {
            Listener.Consume();
        }

        private static void OnStop()
        {
            Listener.Disconnect();
        }
    }
}