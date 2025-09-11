namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get pinned <a href="https://corefork.telegram.org/api/saved-messages">saved dialogs, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/messages.getPinnedSavedDialogs" />
///</summary>
internal sealed class GetPinnedSavedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPinnedSavedDialogs, MyTelegram.Schema.Messages.ISavedDialogs>
{
    protected override Task<MyTelegram.Schema.Messages.ISavedDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPinnedSavedDialogs obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedDialogs>(new TSavedDialogs
        {
            Dialogs = [],
            Chats = [],
            Messages = [],
            Users = []
        });
    }
}
