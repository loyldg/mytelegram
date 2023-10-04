using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetSupportNameHandler : RpcResultObjectHandler<RequestGetSupportName, ISupportName>,
    IGetSupportNameHandler
{
    protected override Task<ISupportName> HandleCoreAsync(IRequestInput input,
        RequestGetSupportName obj)
    {
        throw new NotImplementedException();
    }
}