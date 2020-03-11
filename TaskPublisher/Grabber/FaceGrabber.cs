using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using TaskPublisher.Cache;

namespace TaskPublisher.Grabber
{
    public class FaceGrabber : GrabberBase
    {
        public FaceGrabber(ICacheService cacheService) : base(cacheService)
        {
        }

        public override bool IsSubscribed(CreateEventMessage createEvent)
        {
            GrabberBase.log.Info("i am from face grabber");
            return true;
        }
    }
}
