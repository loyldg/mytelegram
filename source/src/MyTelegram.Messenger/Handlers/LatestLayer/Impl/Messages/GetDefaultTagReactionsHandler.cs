// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getDefaultTagReactions" />
///</summary>
internal sealed class GetDefaultTagReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDefaultTagReactions, MyTelegram.Schema.Messages.IReactions>,
    Messages.IGetDefaultTagReactionsHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDefaultTagReactions obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IReactions>(new TReactions
        {
            Reactions = new()
        });
    }
}
