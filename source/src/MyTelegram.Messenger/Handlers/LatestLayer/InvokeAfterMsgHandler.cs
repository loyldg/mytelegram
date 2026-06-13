namespace MyTelegram.Messenger.Handlers.LatestLayer;
/// <summary>
/// Invokes a query after successful completion of one of the previous queries.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeAfterMsg"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeAfterMsgHandler(IInvokeAfterMsgProcessor invokeAfterMsgProcessor) : BaseObjectHandler<RequestInvokeAfterMsg, IObject> //,IShouldCacheRequest
{
    protected override async Task<IObject> HandleCoreAsync(IRequestInput input, RequestInvokeAfterMsg obj)
    {
        if (invokeAfterMsgProcessor.ExistsInRecentMessageId(obj.MsgId))
        {
            return await invokeAfterMsgProcessor.HandleAsync(input, obj.Query);
        }
        invokeAfterMsgProcessor.Enqueue(obj.MsgId, input, obj.Query);

        return null!;
    }
}