using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class ExportMessageLinkHandler : RpcResultObjectHandler<RequestExportMessageLink, IExportedMessageLink>,
    IExportMessageLinkHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;

    public ExportMessageLinkHandler(IPeerHelper peerHelper)
    {
        _peerHelper = peerHelper;
    }

    protected override Task<IExportedMessageLink> HandleCoreAsync(IRequestInput input,
        RequestExportMessageLink obj)
    {
        var peer = _peerHelper.GetChannel(obj.Channel);
        return Task.FromResult<IExportedMessageLink>(new TExportedMessageLink
        {
            Link = $"Not support export link.Id={obj.Id},channelId={peer.PeerId}",
            Html = "No html"
        });
    }
}
