// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getSavedHistory" />
///</summary>
internal sealed class GetSavedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedHistory, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetSavedHistoryHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSavedHistory obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessages>(new TMessages
        {
            Chats = new(),
            Messages = new(),
            Users = new(),
        });
    }
}
