// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.deleteSavedHistory" />
///</summary>
internal sealed class DeleteSavedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteSavedHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IDeleteSavedHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteSavedHistory obj)
    {
        throw new NotImplementedException();
    }
}
