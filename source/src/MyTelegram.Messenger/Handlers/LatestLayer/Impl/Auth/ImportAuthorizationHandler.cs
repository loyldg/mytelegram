// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Logs in a user using a key transmitted from his native data-center.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTH_BYTES_INVALID The provided authorization is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/auth.importAuthorization" />
///</summary>
internal sealed class ImportAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportAuthorization, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.IImportAuthorizationHandler
{
    private readonly ICacheManager<string> _cacheManager;
    private readonly IEventBus _eventBus;

    private readonly IHashHelper _hashHelper;

    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IAuthorizationConverter> _layeredService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    public ImportAuthorizationHandler(
        IHashHelper hashHelper,
        IQueryProcessor queryProcessor,
        ICacheManager<string> cacheManager,
        IEventBus eventBus,
        ILayeredService<IAuthorizationConverter> layeredService,
        ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService)
    {
        _hashHelper = hashHelper;
        _queryProcessor = queryProcessor;
        _cacheManager = cacheManager;
        _eventBus = eventBus;
        _layeredService = layeredService;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestImportAuthorization obj)
    {
        var keyBytes = _hashHelper.Sha1(obj.Bytes);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        var cacheKey = MyCacheKey.With("authorizations", key);
        var userIdText = await _cacheManager.GetAsync(cacheKey);

        if (int.TryParse(userIdText, out var userId))
        {
            if (userId != obj.Id)
            {
                RpcErrors.RpcErrors400.AuthBytesInvalid.ThrowRpcError();
            }

            await _eventBus.PublishAsync(new BindUidToSessionEvent(input.UserId, input.AuthKeyId, input.PermAuthKeyId));
            var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByIdQuery(userId), default);

            await _cacheManager.RemoveAsync(key);

            var photos = await _photoAppService.GetPhotosAsync(userReadModel);
            ILayeredUser? user = userReadModel == null ? null : _layeredUserService.GetConverter(input.Layer).ToUser(input.UserId, userReadModel, photos);

            return _layeredService.GetConverter(input.Layer).CreateAuthorization(user);
        }
        RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        return null!;
    }
}
