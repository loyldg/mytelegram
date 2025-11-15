namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Obtain information about specific <a href="https://corefork.telegram.org/api/saved-messages#saved-message-dialogs">saved message dialogs »</a> or <a href="https://corefork.telegram.org/api/monoforum">monoforum topics »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSavedDialogsByID"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedDialogsByIDHandler(ILogger<GetSavedDialogsByIDHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedDialogsByID, MyTelegram.Schema.Messages.ISavedDialogs>
{
    protected override Task<MyTelegram.Schema.Messages.ISavedDialogs> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetSavedDialogsByID obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.ISavedDialogs>(new TSavedDialogs { Chats = [], Dialogs = [], Messages = [], Users = [] });
    }
}