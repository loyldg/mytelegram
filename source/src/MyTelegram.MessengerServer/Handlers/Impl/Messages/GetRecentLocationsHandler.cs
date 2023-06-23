using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetRecentLocationsHandler : RpcResultObjectHandler<RequestGetRecentLocations, IMessages>,
    IGetRecentLocationsHandler
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetRecentLocations obj)
    {
        throw new NotImplementedException();
    }
}