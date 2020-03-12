﻿using NServiceBus.Logging;
using Shared;
using Shared.Entities.Faces;
using Shared.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Notifier
{
    public class NotificationController
    {
        private static NotificationController _notificationController = null;
        private static Timer timer = new Timer();
        /// <summary>
        /// key: subscribeId;
        /// </summary>
        public ConcurrentDictionary<string, Queue<NotificationEventMessage>> NotificationDictionary { get; set; } 
            = new ConcurrentDictionary<string, Queue<NotificationEventMessage>>();

        public TimeNode[] timeCircle;
        private int location;

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
        //启动定时器：初始化TimeCircle及挂载每个TimeNode中的Subscribe
        //节点数量=最小公位数/最小间隔
        public void StartTimer()
        {
            //初始化time circle
            InitTimeCircle();

            timer.Elapsed += new ElapsedEventHandler(Tick);
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.AutoReset = true;

        }

        private void InitTimeCircle()
        {
            var count = 2;
            timeCircle = new TimeNode[count];
            //
            for (int i = 0; i < count; i++)
            {
                timeCircle[i] = new TimeNode();
                timeCircle[i].Subscribes.Add("subscribeid");
            }
        }

        //拨动时间轮
        private void Tick(object source, ElapsedEventArgs e)
        {
            location += 1;
            location = location % 2;
            HandleNode(timeCircle[location]);
        }

        //当时间拨动到某个TimeNode上时，依次处理该时间点需要发送的订阅
        private void HandleNode(TimeNode node)
        {
            foreach (var subscribe in node.Subscribes)
            {
                var notification = PrepareNotificationBySubscribeId(subscribe);
                DoPost(notification);
            }
        }
        // 准备每个订阅：从字典中去取
        private Notification PrepareNotificationBySubscribeId(string subscribeId)
        {
            Queue<NotificationEventMessage> notificationEventQueue;
            if (!NotificationDictionary.TryGetValue(subscribeId,out notificationEventQueue))
            {
                return null;
            }
            var count = notificationEventQueue.Count;
            if (count ==0)
            {
                return null;
            }
            var notification=new Notification { NotificationID = "notificationid", SubscribeID = subscribeId };
            //依据类型存入对应字段
            for (int i = 0; i < count; i++)
            {
                var message = notificationEventQueue.Dequeue();
                switch (message.Type?.ToLower())
                {
                    case "face":
                        notification.FaceObjectList.Add(message.Entity as Face);
                            break;
                    case "person":
                        notification.PersonObjectList.Add(message.Entity as Person);
                        break;
                    default:
                        break;
                }

            }
            return notification;
        }
        

        private void DoPost(Notification notification)
        {
            if (notification!=null)
            {
                log.Info($"do {notification.FaceObjectList.Count} task");
            }
        }

        

    }

    public class TimeNode
    {
        public List<string> Subscribes { get; set; } = new List<string>();
    }
}
