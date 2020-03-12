using Shared;
using Shared.Entities.Faces;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TaskPublisher.Cache;

namespace TaskPublisher.Grabber
{
    public class PersonGrabber : GrabberBase<PersonEvent,Person>
    {
        public PersonGrabber(ICacheService cacheService) : base(cacheService)
        {
        }

        public override bool IsSubscribed(PersonEvent createEvent)
        {
            log.Info("i am from person grabber");
            return true;
        }
    }
}
