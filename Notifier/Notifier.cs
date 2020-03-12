using NServiceBus;
using NServiceBus.Logging;
using Shared;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notifier
{
    public class Notifier : IHandleMessages<NotificationEventMessage>
    {
        static ILog log = LogManager.GetLogger<Notifier>();
        static NotificationController controller = NotificationController.GetInstance();

        public Task Handle(NotificationEventMessage message, IMessageHandlerContext context)
        {
            log.Info("put in the queue.");
            //controller.Limatation = 2;
            //controller.Notifications.Add(new Notification { });

            //todo:按照 subscribeId 存入 NotificationDictionary
            if (message.SubscribeID != null)
            {
                controller.NotificationDictionary.GetOrAdd(message.SubscribeID, (p) => new Queue<NotificationEventMessage>()).Enqueue(message);
            }
            
            
            return Task.CompletedTask;
        }
    }
}
