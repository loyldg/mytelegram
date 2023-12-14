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
internal sealed class InvokeWithLayerHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeWithLayer, IObject>,
    IInvokeWithLayerHandler
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
        }

        if (obj.Query is Schema.LayerN.RequestInitConnection initConnectionLayerN)
        {
            query = initConnectionLayerN.Query;
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
}
