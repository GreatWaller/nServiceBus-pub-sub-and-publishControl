using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskPublisher
{
    public class Grabber : IGrabber
    {
        static ILog log = LogManager.GetLogger<Grabber>();
        public async Task Handle(CreateEventMessage message, IMessageHandlerContext context)
        {
            log.Info($"Subscriber has received CreateEventMessage event with URI {message.ResourceURI}.");
            var response = new NotificationMessage
            {
                NotificationID = "NotificationMessage"
            };
            await context.Publish(response).ConfigureAwait(false);
            //return Task.CompletedTask;
        }

        public bool IsSubscribed(CreateEventMessage createEvent)
        {
            return true;
        }
    }
}
