// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

internal sealed class ExportContactTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestExportContactToken, MyTelegram.Schema.IExportedContactToken>,
    Contacts.IExportContactTokenHandler
{
    protected override Task<MyTelegram.Schema.IExportedContactToken> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestExportContactToken obj)
    {
        throw new NotImplementedException();
    }
}
