using NServiceBus;
using Shared;
using Shared.Entities.Faces;
using Shared.Events;
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
                    var faceEvent = new FaceEvent
                    {
                        ResourceURI = orderReceivedId.ToString(),
                        Entity = new Face { Id = 1 },
                        DeviceId = "deviceid"
                    };
                    await endpointInstance.Publish(faceEvent)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published CreateEventMessage Event with ResourceURI {orderReceivedId}.");
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
