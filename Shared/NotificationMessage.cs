using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class NotificationMessage:IEvent
    {
        /// <summary>
        /// 通知标识符 订阅的通知标识符
        /// </summary>
        public string NotificationID { get; set; }

        /// <summary>
        /// 订阅标识符
        /// 该订阅标识符，数据共享接口调用时由VIID生成，级联接口调用时不可为空，取消订阅时必选
        /// </summary>
        public string SubscribeID { get; set; }

        /// <summary>
        /// 订阅标题
        /// 描述订阅的主题和目标，订阅时必选
        /// </summary>
        public string Title { get; set; }
    }
}
