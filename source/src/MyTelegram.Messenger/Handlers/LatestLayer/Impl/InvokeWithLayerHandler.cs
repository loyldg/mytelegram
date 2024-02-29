// ReSharper disable All
namespace MyTelegram.Handlers;

///<summary>
/// Invoke the specified query using the specified API <a href="https://corefork.telegram.org/api/invoking#layers">layer</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTH_BYTES_INVALID The provided authorization is invalid.
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 CONNECTION_API_ID_INVALID The provided API id is invalid.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// See <a href="https://corefork.telegram.org/method/invokeWithLayer" />
///</summary>

internal sealed class InvokeWithLayerHandler : BaseObjectHandler<RequestInvokeWithLayer, IObject>,
    IInvokeWithLayerHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IHandlerHelper _handlerHelper;
    private readonly ILogger<InvokeWithLayerHandler> _logger;
    private readonly IEventBus _eventBus;

    public InvokeWithLayerHandler(IHandlerHelper handlerHelper,
        ICommandBus commandBus,
        ILogger<InvokeWithLayerHandler> logger, IEventBus eventBus)
    {
        _handlerHelper = handlerHelper;
        _commandBus = commandBus;
        _logger = logger;
        _eventBus = eventBus;
    }

    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestInvokeWithLayer obj)
    {
        IObject? query = null;
        if (obj.Query is RequestInitConnection initConnection)
        {
            query = initConnection.Query;
            await SaveDeviceInfoAsync(input,
                initConnection.ApiId,
                initConnection.AppVersion,
                initConnection.DeviceModel,
                initConnection.SystemVersion,
                initConnection.SystemLangCode,
                initConnection.LangPack,
                initConnection.LangCode
            );
        }
        else if (obj.Query is Schema.LayerN.RequestInitConnection initConnectionLayerN)
        {
            query = initConnectionLayerN.Query;
            await SaveDeviceInfoAsync(input,
                initConnectionLayerN.ApiId,
                initConnectionLayerN.AppVersion,
                initConnectionLayerN.DeviceModel,
                initConnectionLayerN.SystemVersion,
                initConnectionLayerN.SystemLangCode,
                initConnectionLayerN.LangPack,
                initConnectionLayerN.LangCode
            );
        }

        if (query == null)
        {
            throw new ArgumentException("InitConnection.query can not be null.");
        }

        if (!_handlerHelper.TryGetHandler(query.ConstructorId, out var handler))
        {
            throw new NotSupportedException($"Not supported query:{query.ConstructorId:x2}");
        }

        _handlerHelper.TryGetHandlerShortName(query.ConstructorId, out var handlerShortName);
        _logger.LogInformation("ReqMsgId={ReqMsgId} UserId={UserId} InvokeWithLayer->{Layer},handler={HandlerShortName}",
            input.ReqMsgId,
            input.UserId,
            obj.Layer,
            handlerShortName);

        var result = await handler.HandleAsync(input, query);

        return result;
    }

    private async Task SaveDeviceInfoAsync(IRequestInput requestInput,
        int apiId,
        //string appName,
        string appVersion,
        //bool officialApp,
        //bool passwordPending,
        string deviceModel,
        //string platform,
        string systemVersion,
        string systemLangCode,
        string langPack,
        string langCode
    )
    {
        if (requestInput.PermAuthKeyId == 0)
        {
            return;
        }

        var eventData = new NewDeviceCreatedEvent(requestInput.ToRequestInfo(), requestInput.PermAuthKeyId, requestInput.AuthKeyId,
            requestInput.UserId,
            apiId,
            appVersion,
            appVersion,
            0,
            false,
            false,
            deviceModel,
            systemVersion,
            systemVersion,
            systemLangCode,
            langPack,
            langCode,
            requestInput.ClientIp,
            requestInput.Layer
        );
        await _eventBus.PublishAsync(eventData);
    }
}
