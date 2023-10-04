using MyTelegram.Domain.Commands.QrCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ExportLoginTokenHandler : RpcResultObjectHandler<RequestExportLoginToken, ILoginToken>,
    IExportLoginTokenHandler, IProcessedHandler
{
    private readonly ITlAuthorizationConverter _authorizationConverter;
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ExportLoginTokenHandler> _logger;
    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;

    public ExportLoginTokenHandler(ILoginTokenCacheAppService loginTokenCacheAppService,
        ICommandBus commandBus,
        IQueryProcessor queryProcessor,
        IRandomHelper randomHelper,
        ILogger<ExportLoginTokenHandler> logger,
        IEventBus eventBus,
        ITlAuthorizationConverter authorizationConverter)
    {
        _loginTokenCacheAppService = loginTokenCacheAppService;
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
        _authorizationConverter = authorizationConverter;
    }

    protected override async Task<ILoginToken> HandleCoreAsync(IRequestInput input,
        RequestExportLoginToken obj)
    {
        if (_loginTokenCacheAppService.TryRemoveLoginInfo(input.AuthKeyId, out var cachedLoginInfo))
        {
            _logger.LogInformation("User {UserId} login using qr code", cachedLoginInfo.UserId);

            await _eventBus
                    .PublishAsync(new BindUidToSessionEvent(cachedLoginInfo.UserId,
                        input.AuthKeyId,
                        input.PermAuthKeyId))
                ;

            var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(cachedLoginInfo.UserId), default);

            return new TLoginTokenSuccess
            {
                Authorization = _authorizationConverter.CreateAuthorizationFromUser(userReadModel)
            };
        }

        var token = new byte[32];
        _randomHelper.NextBytes(token);
        var expireDate = DateTime.UtcNow.AddSeconds(MyTelegramServerDomainConsts.QrCodeExpireSeconds).ToTimestamp();
        var qrCodeId = QrCodeId.Create(BitConverter.ToString(token));
        var command = new ExportLoginTokenCommand(qrCodeId,
            input.ReqMsgId,
            input.AuthKeyId,
            input.PermAuthKeyId,
            token,
            expireDate,
            obj.ExceptIds.ToList());
        await _commandBus.PublishAsync(command, default);

        return null!;
    }
}