using NServiceBus;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public class NotificationEventMessage:NotifacationMessageBase,IEvent
    {
        public IEntity Entity { get; set; }
        public string Type { get; set; }
    }
}
