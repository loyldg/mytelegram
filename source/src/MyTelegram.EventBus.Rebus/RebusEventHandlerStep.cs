using Rebus.Messages;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;

namespace MyTelegram.EventBus.Rebus;

public class RebusEventHandlerStep : IIncomingStep
{
    public Task Process(IncomingStepContext context, Func<Task> next)
    {
        var message = context.Load<Message>();
        var handlerInvokers = context.Load<HandlerInvokers>().ToList();

        if (handlerInvokers.All(x => x.Handler is IRebusDistributedEventHandlerAdapter))
        {
            handlerInvokers = new List<HandlerInvoker> { handlerInvokers.Last() };
            context.Save(new HandlerInvokers(message, handlerInvokers));
        }

        return next();
    }
}