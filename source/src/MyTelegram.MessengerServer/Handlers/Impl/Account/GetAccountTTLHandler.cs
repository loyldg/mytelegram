using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetAccountTtlHandler : RpcResultObjectHandler<RequestGetAccountTTL, IAccountDaysTTL>,
    IGetAccountTTLHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetAccountTtlHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAccountDaysTTL> HandleCoreAsync(IRequestInput input,
        RequestGetAccountTTL obj)
    {
        var user = await _queryProcessor
            .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            ;

        return new TAccountDaysTTL { Days = user!.AccountTtl };
    }
}
