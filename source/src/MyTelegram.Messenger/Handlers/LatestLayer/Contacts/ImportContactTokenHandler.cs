namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Obtain user info from a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a>.
/// Possible errors
/// Code Type Description
/// 400 IMPORT_TOKEN_INVALID The specified token is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.importContactToken"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ImportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportContactToken, MyTelegram.Schema.IUser>
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestImportContactToken obj)
    {
        throw new NotImplementedException();
    }
}