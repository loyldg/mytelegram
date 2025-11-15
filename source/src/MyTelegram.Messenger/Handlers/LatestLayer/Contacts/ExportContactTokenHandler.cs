namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Generates a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a> for the currently logged-in user.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.exportContactToken"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ExportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestExportContactToken, MyTelegram.Schema.IExportedContactToken>
{
    protected override Task<MyTelegram.Schema.IExportedContactToken> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestExportContactToken obj)
    {
        throw new NotImplementedException();
    }
}