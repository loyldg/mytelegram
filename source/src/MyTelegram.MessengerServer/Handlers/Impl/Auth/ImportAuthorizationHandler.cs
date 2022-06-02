using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ImportAuthorizationHandler : RpcResultObjectHandler<RequestImportAuthorization, IAuthorization>,
    IImportAuthorizationHandler, IProcessedHandler
{
    private readonly ICacheManager<string> _cacheManager;
    private readonly IEventBus _eventBus;
    private readonly IHashHelper _hashHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlAuthorizationConverter _authorizationConverter;
    public ImportAuthorizationHandler(
        IHashHelper hashHelper,
        IQueryProcessor queryProcessor,
        ICacheManager<string> cacheManager,
        IEventBus eventBus,
        ITlAuthorizationConverter authorizationConverter)
    {
        _hashHelper = hashHelper;
        _queryProcessor = queryProcessor;
        _cacheManager = cacheManager;
        _eventBus = eventBus;
        _authorizationConverter = authorizationConverter;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestImportAuthorization obj)
    {
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
            var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(userId), default)
                .ConfigureAwait(false);

            await _cacheManager.RemoveAsync(key).ConfigureAwait(false);

            return _authorizationConverter.CreateAuthorizationFromUser(userReadModel);
        }

        throw new BadRequestException("USER_ID_INVALID");
    }
}
