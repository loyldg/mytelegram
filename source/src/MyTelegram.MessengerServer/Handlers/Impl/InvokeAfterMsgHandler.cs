namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeAfterMsgHandler : BaseObjectHandler<RequestInvokeAfterMsg, IObject>,
    IInvokeAfterMsgHandler, IProcessedHandler //,IShouldCacheRequest
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;

    public InvokeAfterMsgHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeAfterMsg obj)
    {
        //Logger.LogDebug($"InvokeAfterMsg,msgId{obj.MsgId},query:{obj.Query.GetType().Name}");
        if (_invokeAfterMsgProcessor.ExistsInRecentMessageId(obj.MsgId))
            return await _invokeAfterMsgProcessor.HandleAsync(input, obj.Query);

        _invokeAfterMsgProcessor.Enqueue(obj.MsgId, input, obj.Query);

        return null!;
    }
}