using EventBus.Messages.Events;
using MassTransit;

namespace Ordering.API.EventBusConsumer
{
    //IConsumer takes TMessage which is our event object(payload)
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
