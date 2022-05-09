using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class BindTempAuthKeyHandler : RpcResultObjectHandler<RequestBindTempAuthKey, IObject>,
    IBindTempAuthKeyHandler, IProcessedHandler //, IShouldCacheRequest
{
    //private readonly IOldMtpHelper _oldMtpHelper;
    //private readonly IMessageIdHelper _messageIdHelper;
    //private readonly ITempAuthKeyHelper _tempAuthKeyHelper;
    //private readonly IPermAuthKeyHelper _permAuthKeyHelper;

    //private readonly ICommandBus _commandBus;
    //private readonly IQueryProcessor _queryProcessor;
    //private readonly ILocalEventBus _localEventBus;
    //private readonly ILogger<BindTempAuthKeyHandler> _logger;

    //public BindTempAuthKeyHandler(
    //    IOldMtpHelper oldMtpHelper,
    //    IMessageIdHelper messageIdHelper,
    //    ITempAuthKeyHelper tempAuthKeyHelper,
    //    IPermAuthKeyHelper permAuthKeyHelper,
    //    ICommandBus commandBus,
    //    IQueryProcessor queryProcessor,
    //    ILocalEventBus localEventBus,
    //    ILogger<BindTempAuthKeyHandler> logger)
    //{
    //    _oldMtpHelper = oldMtpHelper;
    //    _messageIdHelper = messageIdHelper;
    //    _tempAuthKeyHelper = tempAuthKeyHelper;
    //    _permAuthKeyHelper = permAuthKeyHelper;
    //    _commandBus = commandBus;
    //    _queryProcessor = queryProcessor;
    //    _localEventBus = localEventBus;
    //    _logger = logger;
    //}

    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        RequestBindTempAuthKey obj)
    {
        throw new NotImplementedException();

        ////var authKeyReadModel = await _queryProcessor
        ////    .ProcessAsync(new GetAuthKeyByAuthKeyIdQuery(obj.PermAuthKeyId), CancellationToken.None)
        ////    .ConfigureAwait(false);
        //if (!_permAuthKeyHelper.TryGetAuthKeyData(obj.PermAuthKeyId, out var authKeyData))
        //{
        //    // read authKeyReadModel from Q
        //    // todo:using saga to bind temp auth key
        //    var authKeyReadModel = await _queryProcessor
        //        .ProcessAsync(new GetAuthKeyByAuthKeyIdQuery(obj.PermAuthKeyId), CancellationToken.None)
        //        .ConfigureAwait(false);
        //    if (authKeyReadModel?.Data?.Length > 0)
        //    {
        //        authKeyData = authKeyReadModel.Data;
        //        _permAuthKeyHelper.CreateAuthKey(obj.PermAuthKeyId, authKeyData);
        //    }
        //    else
        //    {
        //        _logger.LogError("Invalid perm auth key id:{PermAuthKeyId} reqAuthKeyId:{AuthKeyId}", obj.PermAuthKeyId, input.AuthKeyId);
        //        throw new BadRequestException($"Invalid perm auth key id {obj.PermAuthKeyId}.");
        //    }
        //}
        //var m = _oldMtpHelper.Decrypt(obj.EncryptedMessage, authKeyData);
        //var bindAuthKeyInner = SerializerFactory.CreateObjectSerializer<TBindAuthKeyInner>()
        //    .Deserialize(new BinaryReader(new MemoryStream(m.MessageData)));
        //_logger.LogDebug("Bind temp auth key to perm auth key,userId={UserId},connectionId={ConnectionId},tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2},sessionId={RequestSessionId:x2},reqMsgId={ReqMsg:x2}",
        //    input.UserId, input.ConnectionId, bindAuthKeyInner.TempAuthKeyId, bindAuthKeyInner.PermAuthKeyId, input.RequestSessionId, input.ReqMsgId
        //    );
        //TestConsoleLogger.WriteLine($"[{input.ConnectionId}] sessionId={input.RequestSessionId:x2} next bindTempAuthKey sessionId={bindAuthKeyInner.TempSessionId:x2} reqMsgId:{input.ReqMsgId}");
        //var permAuthKeyId = AuthKeyId.Create(bindAuthKeyInner.PermAuthKeyId);
        //var bindTempAuthKeyCommand =
        //    new BindTempAuthKeyToPermanentAuthKeyCommand(permAuthKeyId,
        //        input.ReqMsgId,
        //        bindAuthKeyInner.TempAuthKeyId,
        //        input.AuthKeyData,
        //        input.ServerSalt
        //        );
        //await _commandBus.PublishAsync(bindTempAuthKeyCommand, CancellationToken.None);

        //var newSession = new TNewSessionCreated
        //{
        //    FirstMsgId = input.ReqMsgId,
        //    ServerSalt = BitConverter.ToInt64(input.ServerSalt),
        //    UniqueId = _messageIdHelper.GenerateUniqueId()
        //};

        //await _localEventBus.PublishAsync(new SendMessageToClientEto(input.AuthKeyData, input.ServerSalt,
        //    newSession, input.ConnectionId, 1, input.AuthKeyId, input.RequestSessionId, input.ReqMsgId));

        //return new TBoolTrue();
    }
}
