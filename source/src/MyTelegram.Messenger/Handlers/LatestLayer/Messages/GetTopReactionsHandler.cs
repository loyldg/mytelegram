using MyTelegram.Schema.Messages;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Got popular <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getTopReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetTopReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetTopReactions, MyTelegram.Schema.Messages.IReactions>
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetTopReactions obj)
    {
        return Task.FromResult<IReactions>(new TReactions { Reactions = [] });
    }
}