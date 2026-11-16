namespace MyTelegram.GatewayServer.EventHandlers;

public class QuickAckEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<QuickAckEvent>, ITransientDependency
{
    public Task HandleEventAsync(QuickAckEvent eventData)
    {
        return clientDataSender.SendAsync(eventData);
    }
}
