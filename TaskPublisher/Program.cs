using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Shared;
using System;
using System.Threading.Tasks;
using TaskPublisher.Cache;

namespace TaskPublisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //IServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddTransient<ICacheService, CacheService>();
            //serviceCollection.AddTransient<IGrabber, Grabber>();

            //IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            //var grabber = serviceProvider.GetService<IGrabber>();

            //todo: 启动加载redis中全部subscribe.如何实现同步？


            Console.Title = "Samples.PubSub.Grabber";
            var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Grabber");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();


            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            endpointConfiguration.RegisterComponents(
                registration: configureComponents =>
                {
                    configureComponents.ConfigureComponent<CacheService>(DependencyLifecycle.InstancePerUnitOfWork);
                });

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);



        }
    }
}
