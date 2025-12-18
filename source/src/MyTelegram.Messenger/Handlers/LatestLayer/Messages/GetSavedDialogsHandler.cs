namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns the current <a href="https://corefork.telegram.org/api/saved-messages">saved dialog list »</a> or <a href="https://corefork.telegram.org/api/monoforum">monoforum topic list »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSavedDialogs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedDialogs, MyTelegram.Schema.Messages.ISavedDialogs>
{
    protected override Task<MyTelegram.Schema.Messages.ISavedDialogs> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetSavedDialogs obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedDialogs>(new TSavedDialogs { Chats = [], Dialogs = [], Messages = [], Users = [], });
    }
}