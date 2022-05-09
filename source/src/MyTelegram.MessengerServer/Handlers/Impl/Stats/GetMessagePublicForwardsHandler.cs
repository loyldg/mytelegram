using MyTelegram.Handlers.Stats;
using MyTelegram.Schema.Messages;
using MyTelegram.Schema.Stats;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stats;

public class GetMessagePublicForwardsHandler : RpcResultObjectHandler<RequestGetMessagePublicForwards, IMessages>,
    IGetMessagePublicForwardsHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetMessagePublicForwards obj)
    {
        throw new NotImplementedException();
    }
}
