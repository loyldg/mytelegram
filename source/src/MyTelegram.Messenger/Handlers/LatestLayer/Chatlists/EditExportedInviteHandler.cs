namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;
/// <summary>
/// Edit a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// 400 INVITE_SLUG_EMPTY The specified invite slug is empty.
/// 400 INVITE_SLUG_EXPIRED The specified chat folder link has expired.
/// 400 PEERS_LIST_EMPTY The specified list of peers is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/chatlists.editExportedInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditExportedInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestEditExportedInvite, MyTelegram.Schema.IExportedChatlistInvite>
{
    protected override Task<MyTelegram.Schema.IExportedChatlistInvite> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Chatlists.RequestEditExportedInvite obj)
    {
        throw new NotImplementedException();
    }
}