namespace MyTelegram.MessengerServer.Services.Caching;

public interface IInvokeAfterMsgProcessor
{
    void AddToRecentMessageIdList(long messageId);

    void Enqueue(long reqMsgId,
        IRequestInput input,
        IObject query);

    bool ExistsInRecentMessageId(long messageId);
    Task HandleAsync(long reqMsgId);

    Task<IObject> HandleAsync(IRequestInput input,
        IObject query);
    Task ProcessAsync();
}