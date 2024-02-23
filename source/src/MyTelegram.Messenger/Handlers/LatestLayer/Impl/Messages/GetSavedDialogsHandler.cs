// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getSavedDialogs" />
///</summary>
internal sealed class GetSavedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedDialogs, MyTelegram.Schema.Messages.ISavedDialogs>,
    Messages.IGetSavedDialogsHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISavedDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSavedDialogs obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedDialogs>(new TSavedDialogs
        {
            Chats = new(),
            Dialogs = new(),
            Messages = new(),
            Users = new(),
        });
    }
}
