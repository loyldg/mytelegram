namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Deletes messages from a <a href="https://corefork.telegram.org/api/monoforum">monoforum topic »</a>, or deletes messages forwarded from a specific peer to <a href="https://corefork.telegram.org/api/saved-messages">saved messages »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteSavedHistory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteSavedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteSavedHistory, MyTelegram.Schema.Messages.IAffectedHistory>
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeleteSavedHistory obj)
    {
        throw new NotImplementedException();
    }
}