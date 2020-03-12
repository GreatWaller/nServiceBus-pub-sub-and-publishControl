using NServiceBus;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPublisher
{
    public interface IGrabber<T>
    {
        List<Subscribe> GetALLSubscribes(T createEvent);
    }
}
