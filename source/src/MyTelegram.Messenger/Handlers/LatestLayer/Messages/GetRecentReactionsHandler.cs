using MyTelegram.Schema.Messages;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get recently used <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getRecentReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetRecentReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentReactions, MyTelegram.Schema.Messages.IReactions>
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetRecentReactions obj)
    {
        return Task.FromResult<IReactions>(new TReactions { Reactions = [] });
    }
}