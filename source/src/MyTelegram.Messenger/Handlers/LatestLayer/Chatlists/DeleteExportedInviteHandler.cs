namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;
/// <summary>
/// Delete a previously created <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// Possible errors
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// 400 INVITE_SLUG_EXPIRED The specified chat folder link has expired.
/// 400 INVITE_SLUG_INVALID The specified invitation slug is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/chatlists.deleteExportedInvite"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteExportedInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestDeleteExportedInvite, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Chatlists.RequestDeleteExportedInvite obj)
    {
        throw new NotImplementedException();
    }
}