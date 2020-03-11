using NServiceBus.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notifier
{
    public class NotificationController
    {
        private static NotificationController _notificationController = null;
        private List<Notification> notifications = new List<Notification>();
        public List<Notification> Notifications
        {
            get
            {
                if (notifications.Count >= Limatation)
                {
                    DoWork();
                }
                return notifications;
            }
            set => notifications = value;
        }
        public int Limatation { get; set; } = 1;
        static ILog log = LogManager.GetLogger<Notifier>();

        static NotificationController()
        {
            _notificationController = new NotificationController();
        }
        public static NotificationController GetInstance()
        {
            return _notificationController;
        }

        void DoWork()
        {
            log.Info("do some work.");
            notifications.Clear();
        }
    }
}
