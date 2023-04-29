namespace MyTelegram.MessengerServer.Services.Impl;

public class ExceptionProcessor : IExceptionProcessor
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ExceptionProcessor> _logger;
    private readonly IObjectMessageSender _objectMessageSender;

    public ExceptionProcessor(ILogger<ExceptionProcessor> logger,
        IObjectMessageSender objectMessageSender,
        IEventBus eventBus)
    {
        _logger = logger;
        _objectMessageSender = objectMessageSender;
        _eventBus = eventBus;
    }

    public async Task HandleExceptionAsync(Exception ex,
        long userId,
        string? handlerName,
        long reqMsgId,
        long authKeyId,
        bool isInMsgContainer)
    {
        _logger.LogError(ex,
            "Process request {ReqMsgId} {IsInMsgContainer} failed,handler={HandlerName},userId={UserId},authKeyId={AuthKeyId:x2}",
            reqMsgId,
            isInMsgContainer ? "in msgContainer" : string.Empty,
            handlerName,
            userId,
            authKeyId
        );

        string errorMessage;
        int errorCode;
        switch (ex)
        {
            case DuplicateOperationException duplicateOperationException:
                var eventData = new DuplicateCommandEvent(reqMsgId, duplicateOperationException.SourceId.Value);
                await _eventBus.PublishAsync(eventData);
                return;
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

        var rpcError = new TRpcError { ErrorCode = errorCode, ErrorMessage = errorMessage };
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = rpcError };
        await _objectMessageSender.SendMessageToPeerAsync(reqMsgId, rpcResult);
    }
}