// ReSharper disable All

using MyTelegram.Schema.Contacts;

namespace MyTelegram.Handlers.Contacts;

internal sealed class ExportContactTokenHandler :
    RpcResultObjectHandler<RequestExportContactToken, Schema.IExportedContactToken>,
    Contacts.IExportContactTokenHandler
{
    protected override Task<Schema.IExportedContactToken> HandleCoreAsync(IRequestInput input,
        RequestExportContactToken obj)
    {
        throw new NotImplementedException();
    }
}