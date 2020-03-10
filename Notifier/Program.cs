using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Notifier
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Samples.PubSub.Notifier";
            var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Notifier");
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
