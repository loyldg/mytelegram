namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Clear recently used <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.clearRecentReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ClearRecentReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearRecentReactions, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestClearRecentReactions obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}