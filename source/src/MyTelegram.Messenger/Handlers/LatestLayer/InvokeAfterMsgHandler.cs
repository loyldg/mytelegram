namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Invokes a query after successful completion of one of the previous queries.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeAfterMsg"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeAfterMsgHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeAfterMsg, IObject>
{
    private readonly IInvokeAfterMsgProcessor _invokeAfterMsgProcessor;
    public InvokeAfterMsgHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    {
        _invokeAfterMsgProcessor = invokeAfterMsgProcessor;
    }

    protected override async Task<IObject> HandleCoreAsync(IRequestInput input, RequestInvokeAfterMsg obj)
    {
        //Logger.LogDebug($"InvokeAfterMsg,msgId{obj.MsgId},query:{obj.Query.GetType().Name}");
        if (_invokeAfterMsgProcessor.ExistsInRecentMessageId(obj.MsgId))
        {
            return await _invokeAfterMsgProcessor.HandleAsync(input, obj.Query);
        }

        _invokeAfterMsgProcessor.Enqueue(obj.MsgId, input, obj.Query);
        return null !;
    }
}