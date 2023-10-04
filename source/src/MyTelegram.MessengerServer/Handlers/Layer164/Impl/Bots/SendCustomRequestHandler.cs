using MyTelegram.Handlers.Bots;
using MyTelegram.Schema.Bots;

namespace MyTelegram.MessengerServer.Handlers.Impl.Bots;

public class SendCustomRequestHandler : RpcResultObjectHandler<RequestSendCustomRequest, IDataJSON>,
    ISendCustomRequestHandler
{
    protected override Task<IDataJSON> HandleCoreAsync(IRequestInput input,
        RequestSendCustomRequest obj)
    {
        throw new NotImplementedException();
    }
}