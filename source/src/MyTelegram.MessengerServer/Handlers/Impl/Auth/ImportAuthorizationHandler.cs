using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ImportAuthorizationHandler : RpcResultObjectHandler<RequestImportAuthorization, IAuthorization>,
    IImportAuthorizationHandler, IProcessedHandler
{
    //private readonly IDistributedCache<string> _distributedCache;
    private readonly ICacheManager<string> _cacheManager;
    private readonly IEventBus _eventBus;

    private readonly IHashHelper _hashHelper;

    //private readonly ISessionAppService _sessionAppService;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public ImportAuthorizationHandler(
        IHashHelper hashHelper,
        //ISessionAppService sessionAppService,
        IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor,
        ICacheManager<string> cacheManager,
        IEventBus eventBus)
    {
        _hashHelper = hashHelper;
        //_sessionAppService = sessionAppService;
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
        _cacheManager = cacheManager;
        _eventBus = eventBus;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestImportAuthorization obj)
    {
        //throw new NotImplementedException();

        var keyBytes = _hashHelper.Sha1(obj.Bytes);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        var uidText = await _cacheManager.GetAsync(key).ConfigureAwait(false);
        if (string.IsNullOrEmpty(uidText))
        {
            throw new BadRequestException("AUTH_BYTES_INVALID");
        }

        if (int.TryParse(uidText, out var userId))
        {
            if (userId != obj.Id)
            {
                throw new BadRequestException("USER_ID_INVALID");
            }

            await _eventBus.PublishAsync(new BindUidToSessionEvent(input.UserId, input.AuthKeyId, input.PermAuthKeyId))
                .ConfigureAwait(false);
            //await _sessionAppService.BindUserIdToSessionAsync(input.AuthKeyId, userId).ConfigureAwait(false);
            var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(userId), default)
                .ConfigureAwait(false);

            //await _distributedCache.RemoveAsync(key).ConfigureAwait(false);
            await _cacheManager.RemoveAsync(key).ConfigureAwait(false);

            return _rpcResultProcessor.CreateAuthorizationFromUser(userReadModel);
        }

        throw new BadRequestException("USER_ID_INVALID");
    }
}
