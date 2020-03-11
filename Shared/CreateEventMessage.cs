using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Shared
{
    public class CreateEventMessage:IEvent
    {
        public object Entity { get; set; }
        public string DeviceId { get; set; }
        /// <summary>
        /// 订阅类别
        /// 订阅时必选，可同时带多个类别（此时是一个string），用英文半角逗号分隔
        /// GA/T 1400.3 B.3.49　订阅类别（SubscribeDetailType）
        /// </summary>
        public string SubscribeDetail { get; set; }

        /// <summary>
        /// 订阅资源路径
        /// 资源路径URI(卡口ID、设备ID、采集内容ID、案件ID、目标视图库ID、行政区编号2/4/6位等)支持批量和单个订阅，订阅时必选
        /// </summary>
        public string ResourceURI { get; set; }
    }
}
