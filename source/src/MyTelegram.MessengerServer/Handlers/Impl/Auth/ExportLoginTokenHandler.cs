using MyTelegram.Domain.Commands.QrCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ExportLoginTokenHandler : RpcResultObjectHandler<RequestExportLoginToken, ILoginToken>,
    IExportLoginTokenHandler, IProcessedHandler //, IShouldCacheRequest
{
    //private readonly IRequestCacheAppService _requestCacheAppService;
    //private readonly ITempAuthKeyHelper _tempAuthKeyHelper;
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;
    private readonly ILogger<ExportLoginTokenHandler> _logger;
    private readonly ILoginTokenCacheAppService _loginTokenCacheAppService;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public ExportLoginTokenHandler(ILoginTokenCacheAppService loginTokenCacheAppService,
        //IRequestCacheAppService requestCacheAppService,
        //ITempAuthKeyHelper tempAuthKeyHelper,
        ICommandBus commandBus,
        IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor,
        IRandomHelper randomHelper,
        ILogger<ExportLoginTokenHandler> logger,
        IEventBus eventBus)
    {
        _loginTokenCacheAppService = loginTokenCacheAppService;
        //_requestCacheAppService = requestCacheAppService;
        //_tempAuthKeyHelper = tempAuthKeyHelper;
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
        _randomHelper = randomHelper;
        _logger = logger;
        _eventBus = eventBus;
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
            //await _commandBus.PublishAsync(bindUserIdToAuthKeyIdCommand, default).ConfigureAwait(false);

            _logger.LogInformation("User {UserId} login using qr code", cachedLoginInfo.UserId);

            await _eventBus
                .PublishAsync(new BindUidToSessionEvent(cachedLoginInfo.UserId, input.AuthKeyId, input.PermAuthKeyId))
                .ConfigureAwait(false);

            var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(cachedLoginInfo.UserId), default).ConfigureAwait(false);

            return new TLoginTokenSuccess {
                Authorization = _rpcResultProcessor.CreateAuthorizationFromUser(userReadModel)
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
        await _commandBus.PublishAsync(command, default).ConfigureAwait(false);

        return null!;
    }
}
