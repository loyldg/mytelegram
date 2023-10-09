// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Obtain user info from a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 IMPORT_TOKEN_INVALID The specified token is invalid.
/// See <a href="https://corefork.telegram.org/method/contacts.importContactToken" />
///</summary>
internal sealed class ImportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportContactToken, MyTelegram.Schema.IUser>,
    Contacts.IImportContactTokenHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestImportContactToken obj)
    {
        throw new NotImplementedException();
    }
}
