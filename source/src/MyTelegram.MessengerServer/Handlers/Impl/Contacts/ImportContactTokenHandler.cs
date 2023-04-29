// ReSharper disable All

using MyTelegram.Schema.Contacts;

namespace MyTelegram.Handlers.Contacts;

internal sealed class ImportContactTokenHandler : RpcResultObjectHandler<RequestImportContactToken, Schema.IUser>,
    Contacts.IImportContactTokenHandler
{
    protected override Task<Schema.IUser> HandleCoreAsync(IRequestInput input,
        RequestImportContactToken obj)
    {
        throw new NotImplementedException();
    }
}
