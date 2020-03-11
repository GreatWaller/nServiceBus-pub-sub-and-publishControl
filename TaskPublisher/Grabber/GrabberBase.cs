using NServiceBus;
using NServiceBus.Logging;
using Shared;
using Shared.Entity.Faces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskPublisher.Cache;

namespace TaskPublisher
{
    public abstract class GrabberBase : IGrabber, 
        IHandleMessages<CreateEventMessage>
    {
        static protected ILog log = LogManager.GetLogger<GrabberBase>();
        protected readonly ICacheService _cacheService;

        public GrabberBase(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public List<Subscribe> GetALLSubscribes(CreateEventMessage createEvent)
        {
            //todo:规则梳理,暂时仅通过多数类型都涉及到的 设备号来决定
            var subscribes = _cacheService.GetAllSubscribes();
            var result = new List<Subscribe>();
            foreach (var item in subscribes)
            {
                if (IsSubscribed(createEvent))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public abstract bool IsSubscribed(CreateEventMessage createEvent);
        public async Task Handle(CreateEventMessage message, IMessageHandlerContext context)
        {
            log.Info($"Subscriber has received CreateEventMessage event with URI {message.ResourceURI}.");

            var subscribes = GetALLSubscribes(message);

            foreach (var subscribe in subscribes)
            {
                var response = new NotificationMessage
                {
                    Entity = message.Entity,
                    SubscribeID = subscribe.SubscribeID,
                    ReportInterval=subscribe.ReportInterval,
                    ReceiveAddr=subscribe.ReceiveAddr
                };

                await context.Publish(response).ConfigureAwait(false);
            }
            //return Task.CompletedTask;
        }
    }
}
