// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Check whether chat history exported from another chat app can be <a href="https://corefork.telegram.org/api/import">imported into a specific Telegram chat, click here for more info »</a>.If the check succeeds, and no RPC errors are returned, a <a href="https://corefork.telegram.org/type/messages.CheckedHistoryImportPeer">messages.CheckedHistoryImportPeer</a> constructor will be returned, with a confirmation text to be shown to the user, before actually initializing the import.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_NOT_MUTUAL_CONTACT The provided user is not a mutual contact.
/// See <a href="https://corefork.telegram.org/method/messages.checkHistoryImportPeer" />
///</summary>
internal sealed class CheckHistoryImportPeerHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCheckHistoryImportPeer, MyTelegram.Schema.Messages.ICheckedHistoryImportPeer>,
    Messages.ICheckHistoryImportPeerHandler
{
    protected override Task<MyTelegram.Schema.Messages.ICheckedHistoryImportPeer> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestCheckHistoryImportPeer obj)
    {
        throw new NotImplementedException();
    }
}
