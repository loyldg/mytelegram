// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Generates a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a> for the currently logged-in user.
/// See <a href="https://corefork.telegram.org/method/contacts.exportContactToken" />
///</summary>
internal sealed class ExportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestExportContactToken, MyTelegram.Schema.IExportedContactToken>,
    Contacts.IExportContactTokenHandler
{
    protected override Task<MyTelegram.Schema.IExportedContactToken> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestExportContactToken obj)
    {
        throw new NotImplementedException();
    }
}
