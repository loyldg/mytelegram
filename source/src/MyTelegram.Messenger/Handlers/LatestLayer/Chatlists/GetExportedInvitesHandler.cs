namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;
/// <summary>
/// List all <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep links »</a> associated to a folder
/// Possible errors
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/chatlists.getExportedInvites"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetExportedInvitesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestGetExportedInvites, MyTelegram.Schema.Chatlists.IExportedInvites>
{
    protected override Task<MyTelegram.Schema.Chatlists.IExportedInvites> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Chatlists.RequestGetExportedInvites obj)
    {
        throw new NotImplementedException();
    }
}