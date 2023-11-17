// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Obtain available <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
/// See <a href="https://corefork.telegram.org/method/messages.getAvailableReactions" />
///</summary>
internal sealed class GetAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAvailableReactions, MyTelegram.Schema.Messages.IAvailableReactions>,
    Messages.IGetAvailableReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAvailableReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAvailableReactions obj)
    {
        return Task.FromResult<IAvailableReactions>(new TAvailableReactions
        {
            Reactions = new()
        });
    }
}
