using NServiceBus;
using NServiceBus.Logging;
using Shared;
using Shared.Entities;
using Shared.Entities.Faces;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskPublisher.Cache;

namespace TaskPublisher
{
    public abstract class GrabberBase<TEvent,TEntity> : IGrabber<TEvent>, 
        IHandleMessages<TEvent>
        where TEvent : EventMessageBase<TEntity>
        where TEntity: IEntity
    {
        static protected ILog log = LogManager.GetLogger<GrabberBase<TEvent,TEntity>>();
        protected readonly ICacheService _cacheService;

        public GrabberBase(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public List<Subscribe> GetALLSubscribes(TEvent createEvent)
        {
            //todo:规则梳理,暂时仅通过多数类型都涉及到的 设备号来决定
            var subscribes = _cacheService.GetAllSubscribes();
            var result = new List<Subscribe>();
            foreach (var subscribe in subscribes)
            {
                if (IsSubscribed(createEvent,subscribe))
                {
                    result.Add(subscribe);
                }
            }
            return result;
        }

        public abstract bool IsSubscribed(TEvent createEvent, Subscribe subscribe);
        public async Task Handle(TEvent message, IMessageHandlerContext context)
        {
            log.Info($"Subscriber has received CreateEventMessage event with URI {message.ResourceURI}.");

            var subscribes = GetALLSubscribes(message);

            foreach (var subscribe in subscribes)
            {
                var response = new NotificationEventMessage
                {
                    Entity = message.Entity,
                    Type = typeof(TEntity).Name.ToString(),
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
