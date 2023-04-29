using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

// ReSharper disable once UnusedMember.Global
public class CheckUsernameHandler : RpcResultObjectHandler<RequestCheckUsername, IBool>,
    ICheckUsernameHandler, IProcessedHandler
{
    //private readonly IQueryProcessor _queryProcessor;
    private readonly ICuckooFilter _cuckooFilter;

    public CheckUsernameHandler( /*IQueryProcessor queryProcessor,*/
        ICuckooFilter cuckooFilter)
    {
        //_queryProcessor = queryProcessor;
        _cuckooFilter = cuckooFilter;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCheckUsername obj)
    {
        if (await _cuckooFilter
                .ExistsAsync( Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{obj.Username}"))
                .ConfigureAwait(false))
        {
            return new TBoolFalse();
        }

        return new TBoolTrue();

        //var item = await _queryProcessor
        //    .ProcessAsync(new GetUserNameByIdQuery(obj.Username),
        //        CancellationToken.None);
        //if (item == null)
        //{
        //    return new TBoolTrue();
        //}

        //return new TBoolFalse();
    }
}
