using Shared;
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

        public override bool IsSubscribed(FaceEvent createEvent, Subscribe subscribe)
        {
            log.Info("i am from face grabber");
            var device=_cacheService.GetDeviceById(createEvent.DeviceId);
            if (subscribe.ResourceURI.Contains(device.DeviceId)
                ||subscribe.ResourceURI.Contains(device.TollgateId)
                ||subscribe.ResourceURI.Contains(device.LaneId))
            {
                return true;
            }
            return false;
        }
    }
}
