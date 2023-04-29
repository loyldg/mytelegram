using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class SearchHandler : RpcResultObjectHandler<RequestSearch, IFound>,
    ISearchHandler, IProcessedHandler
{
    private readonly IContactAppService _contactAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public SearchHandler(IContactAppService contactAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _contactAppService = contactAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IFound> HandleCoreAsync(IRequestInput input,
        RequestSearch obj)
    {
        var userId = input.UserId;
        var r = await _contactAppService.SearchAsync(userId, obj.Q);

        return _rpcResultProcessor.ToFound(r);
    }
}
