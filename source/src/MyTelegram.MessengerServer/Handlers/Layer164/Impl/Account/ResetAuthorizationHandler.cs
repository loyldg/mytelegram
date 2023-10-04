using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class ResetAuthorizationHandler : RpcResultObjectHandler<RequestResetAuthorization, IBool>,
    IResetAuthorizationHandler, IProcessedHandler
{
    private readonly IEventBus _eventBus;

    private readonly IQueryProcessor _queryProcessor;

    public ResetAuthorizationHandler(
        IQueryProcessor queryProcessor,
        IEventBus eventBus)
    {
        _queryProcessor = queryProcessor;
        _eventBus = eventBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetAuthorization obj)
    {
        var deviceReadModel = await _queryProcessor
                .ProcessAsync(new GetDeviceByHashQuery(input.UserId, obj.Hash), CancellationToken.None)
            ;
        if (deviceReadModel != null)
            await _eventBus.PublishAsync(new UnRegisterAuthKeyEvent(deviceReadModel.PermAuthKeyId))
                ;

        return new TBoolTrue();
    }
}