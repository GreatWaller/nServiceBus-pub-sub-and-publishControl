using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPublisher.Cache
{
    public interface ICacheService
    {
        List<Subscribe> GetAllSubscribes();
        List<Device> GetAllDevices();
    }
}
