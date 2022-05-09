using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

//using MyTelegram.Queries.MongoDB.Device;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetAuthorizationsHandler : RpcResultObjectHandler<RequestGetAuthorizations, IAuthorizations>,
    IGetAuthorizationsHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetAuthorizationsHandler(IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor)
    {
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IAuthorizations> HandleCoreAsync(IRequestInput input,
        RequestGetAuthorizations obj)
    {
        var deviceReadModelList = await _queryProcessor
            .ProcessAsync(new GetDeviceByUidQuery(input.UserId), CancellationToken.None).ConfigureAwait(false);
        var r = _rpcResultProcessor.ToAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TAuthorizations { Authorizations = new TVector<IAuthorization>(r) };
    }
}
