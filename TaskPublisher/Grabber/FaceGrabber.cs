﻿using Shared;
using Shared.Entities.Faces;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using TaskPublisher.Cache;

namespace TaskPublisher.Grabber
{
    public class FaceGrabber : GrabberBase<FaceEvent,Face>
    {
        public FaceGrabber(ICacheService cacheService) : base(cacheService)
        {
        }

        public override bool IsSubscribed(FaceEvent createEvent)
        {
            log.Info("i am from face grabber");
            return true;
        }
    }
}
