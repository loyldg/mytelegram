// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Generate a login token, for <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.<br>
/// The generated login token should be encoded using base64url, then shown as a <code>tg://login?token=base64encodedtoken</code> <a href="https://corefork.telegram.org/api/links#qr-code-login-links">deep link »</a> in the QR code.For more info, see <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// See <a href="https://corefork.telegram.org/method/auth.exportLoginToken" />
///</summary>
internal sealed class ExportLoginTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportLoginToken, MyTelegram.Schema.Auth.ILoginToken>,
    Auth.IExportLoginTokenHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ExportLoginTokenHandler> _logger;
    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    //private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly ILayeredService<IAuthorizationConverter> _layeredService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;

    public ExportLoginTokenHandler(ILoginTokenCacheAppService loginTokenCacheAppService,
        //IRequestCacheAppService requestCacheAppService,
        //ITempAuthKeyHelper tempAuthKeyHelper,
        ICommandBus commandBus,
        IQueryProcessor queryProcessor,
        IRandomHelper randomHelper,
        ILogger<ExportLoginTokenHandler> logger,
        IEventBus eventBus,
        ILayeredService<IAuthorizationConverter> layeredService,
        ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService)
    {
        _loginTokenCacheAppService = loginTokenCacheAppService;
        //_requestCacheAppService = requestCacheAppService;
        //_tempAuthKeyHelper = tempAuthKeyHelper;
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
        _layeredService = layeredService;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
    }

    protected override async Task<ILoginToken> HandleCoreAsync(IRequestInput input,
        RequestExportLoginToken obj)
    {
        if (_loginTokenCacheAppService.TryRemoveLoginInfo(input.AuthKeyId, out var cachedLoginInfo))
        {
            // login token has accepted
            //var bindUserIdToAuthKeyIdCommand = new BindUserIdToAuthKeyCommand(AuthKeyId.Create(input.AuthKeyId),
            //    0,
            //    input.AuthKeyId,
            //    input.PermAuthKeyId,
            //    cachedLoginInfo.UserId,
            //    Guid.NewGuid());
            //await _commandBus.PublishAsync(bindUserIdToAuthKeyIdCommand, default);

            _logger.LogInformation("User {UserId} login using QRCode", cachedLoginInfo.UserId);

            await _eventBus
                .PublishAsync(new BindUidToSessionEvent(cachedLoginInfo.UserId, input.AuthKeyId, input.PermAuthKeyId))
         ;

            var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(cachedLoginInfo.UserId), default);
            var photos = await _photoAppService.GetPhotosAsync(userReadModel);
            ILayeredUser? user = userReadModel == null ? null : _layeredUserService.GetConverter(input.Layer).ToUser(input.UserId, userReadModel, photos);
            return new TLoginTokenSuccess
            {
                Authorization = _layeredService.GetConverter(input.Layer).CreateAuthorization(user)
            };
        }

        var token = new byte[32];
        _randomHelper.NextBytes(token);
        var expireDate = DateTime.UtcNow.AddSeconds(MyTelegramServerDomainConsts.QrCodeExpireSeconds).ToTimestamp();
        var qrCodeId = QrCodeId.Create(BitConverter.ToString(token));
        var command = new ExportLoginTokenCommand(qrCodeId,
            input.ToRequestInfo(),
            input.AuthKeyId,
            input.PermAuthKeyId,
            token,
            expireDate,
            obj.ExceptIds.ToList());
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}
