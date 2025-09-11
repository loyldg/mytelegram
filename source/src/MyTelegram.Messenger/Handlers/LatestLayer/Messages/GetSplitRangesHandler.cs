namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get message ranges for saving the user's chat history
/// See <a href="https://corefork.telegram.org/method/messages.getSplitRanges" />
///</summary>
internal sealed class GetSplitRangesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSplitRanges, TVector<MyTelegram.Schema.IMessageRange>>
{
    protected override Task<TVector<MyTelegram.Schema.IMessageRange>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSplitRanges obj)
    {
        throw new NotImplementedException();
    }
}
