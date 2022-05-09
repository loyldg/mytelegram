using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetWebPagePreviewHandler : RpcResultObjectHandler<RequestGetWebPagePreview, IMessageMedia>,
    IGetWebPagePreviewHandler, IProcessedHandler
{
    protected override Task<IMessageMedia> HandleCoreAsync(IRequestInput input,
        RequestGetWebPagePreview obj)
    {
        return Task.FromResult<IMessageMedia>(new TMessageMediaEmpty());
        //throw new NotImplementedException();
    }
}
