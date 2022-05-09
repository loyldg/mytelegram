using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ExportMessageLinkHandler : RpcResultObjectHandler<RequestExportMessageLink, IExportedMessageLink>,
    IExportMessageLinkHandler
{
    protected override Task<IExportedMessageLink> HandleCoreAsync(IRequestInput input,
        RequestExportMessageLink obj)
    {
        throw new NotImplementedException();
    }
}
