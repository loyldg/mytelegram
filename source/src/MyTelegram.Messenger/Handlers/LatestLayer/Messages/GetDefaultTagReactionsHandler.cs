namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Fetch a default recommended list of <a href="https://corefork.telegram.org/api/saved-messages#tags">saved message tag reactions</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getDefaultTagReactions" />
///</summary>
internal sealed class GetDefaultTagReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDefaultTagReactions, MyTelegram.Schema.Messages.IReactions>
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDefaultTagReactions obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IReactions>(new TReactions
        {
            Reactions = []
        });
    }
}
