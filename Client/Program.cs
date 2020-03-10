using NServiceBus;
using Shared;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Samples.PubSub.Publisher";
            var endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Publisher");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            await Start(endpointInstance)
                .ConfigureAwait(false);
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        static async Task Start(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press 'Enter' to publish the OrderReceived event");
            Console.WriteLine("Press any other key to exit");

            #region PublishLoop

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.Enter)
                {
                    var createEventMessage = new CreateEventMessage
                    {
                        ResourceURI="VIID/" 

                    };
                    await endpointInstance.Publish(createEventMessage)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published CreateEventMessage Event with Id {orderReceivedId}.");
                }
                else
                {
                    return;
                }
            }

            #endregion
        }
    }
}
