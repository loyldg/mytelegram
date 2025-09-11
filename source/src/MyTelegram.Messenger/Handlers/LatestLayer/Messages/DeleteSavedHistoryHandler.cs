namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Deletes messages forwarded from a specific peer to <a href="https://corefork.telegram.org/api/saved-messages">saved messages »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteSavedHistory" />
///</summary>
internal sealed class DeleteSavedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteSavedHistory, MyTelegram.Schema.Messages.IAffectedHistory>
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteSavedHistory obj)
    {
        throw new NotImplementedException();
    }
}
