// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invokes a query after successful completion of one of the previous queries.
/// See <a href="https://corefork.telegram.org/method/invokeAfterMsg" />
///</summary>
internal sealed class InvokeAfterMsgHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeAfterMsg, IObject>,
    IInvokeAfterMsgHandler
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
        {
            return await _invokeAfterMsgProcessor.HandleAsync(input, obj.Query);
        }

        _invokeAfterMsgProcessor.Enqueue(obj.MsgId, input, obj.Query);

        return null!;
    }
}
