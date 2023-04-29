namespace MyTelegram.MessengerServer.Handlers.Impl;

public class InvokeWithLayerHandler : BaseObjectHandler<RequestInvokeWithLayer, IObject>,
    IInvokeWithLayerHandler, IProcessedHandler
{
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<InvokeWithLayerHandler> _logger;

    public InvokeWithLayerHandler(IHandlerHelper handlerHelper,
        ILogger<InvokeWithLayerHandler> logger)
    {
        _handlerHelper = handlerHelper;
        _logger = logger;
    }

    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithLayer obj)
    {
        IObject? query = null;
        if (obj.Query is RequestInitConnection initConnection)
        {
            query = initConnection.Query;
            ////Logger.LogDebug($"initConnection:{query.GetType().Name}");
            ////_sessionAppService.SetSessionLayer(input.AuthKeyId, obj.Layer);
            ////SessionAppService.SetDeviceInfo(input.AuthKeyId, obj.Layer, initConnection.LangPack);
            //await SaveDeviceInfoAsync(input.ReqMsgId,
            //     input.PermAuthKeyId,
            //     input.AuthKeyId,
            //     input.UserId,
            //     //input.ClientIp,
            //     null,
            //     initConnection.ApiId,
            //     initConnection.AppVersion,
            //     initConnection.DeviceModel,
            //     initConnection.SystemVersion,
            //     initConnection.SystemLangCode,
            //     initConnection.LangPack,
            //     initConnection.LangCode,
            //     obj.Layer

            // );
        }

        if (obj.Query is Schema.LayerN.RequestInitConnection initConnectionLayerN)
        {
            query = initConnectionLayerN.Query;
            ////Logger.LogDebug($"initConnection(LayerN),seq={input.SeqNumber},query={query.GetType().Name}");
            ////_sessionAppService.SetSessionLayer(input.AuthKeyId, obj.Layer);
            ////SessionAppService.SetDeviceInfo(input.AuthKeyId, obj.Layer, initConnectionLayerN.LangPack);
            //await SaveDeviceInfoAsync(input.ReqMsgId,
            //    input.PermAuthKeyId,
            //    input.AuthKeyId,
            //    input.UserId,
            //    //input.ClientIp,
            //    null,
            //    initConnectionLayerN.ApiId,
            //    initConnectionLayerN.AppVersion,
            //    initConnectionLayerN.DeviceModel,
            //    initConnectionLayerN.SystemVersion,
            //    initConnectionLayerN.SystemLangCode,
            //    initConnectionLayerN.LangPack,
            //    initConnectionLayerN.LangCode,
            //    obj.Layer

            //);
        }

        //if (input.UserId != 0)
        //{
        //    Logger.LogInformation($"Init Connection Ok.UserId={input.UserId}");
        //}

        if (query == null)
        {
            throw new ArgumentException("InitConnection.query can not be null.");
        }

        if (!_handlerHelper.TryGetHandler(query.ConstructorId, out var handler))
        {
            throw new NotSupportedException($"Not supported query:{query.ConstructorId:x2}");
        }

        //if (handler is IShouldCacheRequest)
        //{
        //    _requestCacheAppService.AddRequest(input.ReqMsgId, input.AuthKeyId, input.RequestSessionId, input.SeqNumber);
        //}

        //// todo:check permission
        //_permissionChecker.CheckPermission(input.AuthKeyId, query.ConstructorId);
        _handlerHelper.TryGetHandlerShortName(query.ConstructorId, out var handlerShortName);
        _logger.LogDebug("UserId={UserId} InvokeWithLayer->{Layer},handler={HandlerShortName}",
            input.UserId,
            obj.Layer,
            handlerShortName);

        var result = await handler.HandleAsync(input, query);

        return result;
    }

    //private async Task SaveDeviceInfoAsync(long reqMsgId,
    //    long permAuthKeyId,
    //    long tempAuthKeyId,
    //    long userId,
    //    string clientIp,
    //    int apiId,
    //    //string appName,
    //    string appVersion,
    //    //bool officialApp,
    //    //bool passwordPending,
    //    string deviceModel,
    //    //string platform,
    //    string systemVersion,
    //    string systemLangCode,
    //    string langPack,
    //    string langCode,
    //    int layer
    //    )
    //{
    //    return;
    //    ////if (userId == 0)
    //    ////{
    //    ////    return;
    //    ////}

    //    //if (permAuthKeyId == 0)
    //    //{
    //    //    return;
    //    //}

    //    //var session = _sessionAppService.GetSession(tempAuthKeyId);
    //    //if (session.DeviceType != DeviceType.Unknown && session.PermAuthKeyId == permAuthKeyId)
    //    //{
    //    //    return;
    //    //}

    //    //_sessionAppService.SetDeviceInfo(tempAuthKeyId, layer, langPack);
    //    //// todo:save device info
    //    //var hashBytes = _hashHelper.Sha1(Encoding.UTF8.GetBytes($"{permAuthKeyId}_{deviceModel}_{systemLangCode}_{langPack}"));
    //    //var hash = BitConverter.ToInt64(hashBytes);
    //    //var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(permAuthKeyId),
    //    //    reqMsgId,
    //    //    permAuthKeyId,
    //    //    tempAuthKeyId,
    //    //    userId,
    //    //    apiId,
    //    //    appVersion,
    //    //    appVersion,
    //    //    hash,
    //    //    true,
    //    //    false,
    //    //    deviceModel,
    //    //    string.Empty,
    //    //    systemVersion,
    //    //    systemLangCode,
    //    //    langPack,
    //    //    langCode,
    //    //    clientIp,
    //    //    layer
    //    //);

    //    ////if (permAuthKeyId != 0)
    //    ////{
    //    ////    TestConsoleLogger.WriteLine("PermAuthKey is binding.");
    //    ////}
    //    ////Console.WriteLine($"init connection:{JsonConvert.SerializeObject(createDeviceCommand)}");

    //    //await _commandBus.PublishAsync(createDeviceCommand, CancellationToken.None);
    //}
}
