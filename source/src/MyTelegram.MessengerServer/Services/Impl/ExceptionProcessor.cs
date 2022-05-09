namespace MyTelegram.MessengerServer.Services.Impl;

public class ExceptionProcessor : IExceptionProcessor //, ISingletonDependency
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ExceptionProcessor> _logger;

    private readonly IObjectMessageSender _objectMessageSender;

    //private const int BufferSize = 1024 * 4;
    public ExceptionProcessor(ILogger<ExceptionProcessor> logger,
        IObjectMessageSender objectMessageSender,
        IEventBus eventBus)
    {
        _logger = logger;
        _objectMessageSender = objectMessageSender;
        _eventBus = eventBus;
    }

    public async Task HandleExceptionAsync(Exception ex,
        //int errorCode,
        //string errorMessage,
        long userId,
        string? handlerName,
        long reqMsgId,
        //byte[] authKeyData,
        //byte[] serverSalt,
        //string connectionId,
        //int seqNumber,
        long authKeyId,
        //long sessionId, 
        bool isInMsgContainer)
    {
        _logger.LogError(ex,
            "Process request {ReqMsgId} {IsInMsgContainer} failed,handler={HandlerName},userId={UserId},authKeyId={AuthKeyId:x2}",
            reqMsgId,
            isInMsgContainer ? "in msgContainer" : string.Empty,
            handlerName,
            //connectionId,
            userId,
            authKeyId //,
            //sessionId
        );

        string errorMessage;
        int errorCode;
        switch (ex)
        {
            case DuplicateOperationException duplicateOperationException:
                //errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                //errorMessage = MyTelegramServerDomainConsts.InternalErrorMessage;
                var eventData = new DuplicateCommandEvent(reqMsgId, duplicateOperationException.SourceId.Value);
                // Console.WriteLine($"###############DuplicateOperationException,sourceId={duplicateOperationException.SourceId.Value}");
                await _eventBus.PublishAsync(eventData).ConfigureAwait(false);
                return;
            //break;
            case UserFriendlyException userFriendlyException:
                errorCode = userFriendlyException.ErrorCode;
                errorMessage = userFriendlyException.Message;
                break;

            case NotImplementedException:
                errorCode = MyTelegramServerDomainConsts.BadRequestErrorCode;
                errorMessage = string.IsNullOrEmpty(ex.Message) ? "Api not implemented" : ex.Message;
                break;

            case RpcException rpcException:
                errorCode = rpcException.Error.ErrorCode;
                errorMessage = rpcException.Error.ErrorMessage;
                break;

            case DomainError domainError:
                errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                errorMessage = domainError.Message;
                break;

            case SagaPublishException
            {
                InnerException: CommandException
                {
                    InnerException: UserFriendlyException or DomainError
                } commandException
            }:
                errorCode = MyTelegramServerDomainConsts.BadRequestErrorCode;
                errorMessage = commandException.InnerException?.Message ?? MyTelegramServerDomainConsts.InternalErrorMessage;
                break;

            default:
                errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                errorMessage = MyTelegramServerDomainConsts.InternalErrorMessage;
                break;
        }

        // Default buffer size is 4096
        using var owner = MemoryPool<byte>.Shared.Rent();

        var rpcError = new TRpcError { ErrorCode = errorCode, ErrorMessage = errorMessage };
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = rpcError };
        await _objectMessageSender.SendMessageToPeerAsync(reqMsgId, rpcResult).ConfigureAwait(false);
        //await _localEventBus.PublishAsync(new SendMessageToClientEto(authKeyData,
        //    serverSalt,
        //    rpcResult,
        //    connectionId,
        //    seqNumber,
        //    authKeyId,
        //    sessionId,
        //    reqMsgId)).ConfigureAwait(false);
    }
}