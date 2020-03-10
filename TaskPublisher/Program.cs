using NServiceBus;
using Shared;
using System;
using System.Threading.Tasks;

namespace TaskPublisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //IServiceCollection serviceCollection = new ServiceCollection();
            //serviceCollection.AddTransient<IGrabber, Grabber>();
            //IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            //var grabber = serviceProvider.GetService<IGrabber>();


            Console.Title = "Samples.PubSub.Grabber";
            var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Grabber");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();


            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop()
                .ConfigureAwait(false);



        }
    }
}
