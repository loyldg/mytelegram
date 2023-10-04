using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDocumentByHashHandler : RpcResultObjectHandler<RequestGetDocumentByHash, IDocument>,
    IGetDocumentByHashHandler
{
    protected override Task<IDocument> HandleCoreAsync(IRequestInput input,
        RequestGetDocumentByHash obj)
    {
        throw new NotImplementedException();
    }
}