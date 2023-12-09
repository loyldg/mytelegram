// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Got popular <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// See <a href="https://corefork.telegram.org/method/messages.getTopReactions" />
///</summary>
internal sealed class GetTopReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetTopReactions, MyTelegram.Schema.Messages.IReactions>,
    Messages.IGetTopReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetTopReactions obj)
    {
        return Task.FromResult<IReactions>(new TReactions
        {
            Reactions = new()
        });
    }
}
