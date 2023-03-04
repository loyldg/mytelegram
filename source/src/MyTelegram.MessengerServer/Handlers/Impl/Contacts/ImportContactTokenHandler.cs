// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

internal sealed class ImportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportContactToken, MyTelegram.Schema.IUser>,
    Contacts.IImportContactTokenHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestImportContactToken obj)
    {
        throw new NotImplementedException();
    }
}
