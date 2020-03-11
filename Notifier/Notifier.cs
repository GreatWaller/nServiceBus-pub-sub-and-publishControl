using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notifier
{
    class Notifier : IHandleMessages<NotificationMessage>
    {
        static ILog log = LogManager.GetLogger<Notifier>();
        static NotificationController controller = NotificationController.GetInstance();
        public Task Handle(NotificationMessage message, IMessageHandlerContext context)
        {
            log.Info("do nothing.");
            controller.Limatation = 2;
            controller.Notifications.Add(new Notification { });

            return Task.CompletedTask;
        }
    }
}
