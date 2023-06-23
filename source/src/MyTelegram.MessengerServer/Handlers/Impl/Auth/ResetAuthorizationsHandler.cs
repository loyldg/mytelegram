using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class ResetAuthorizationsHandler : RpcResultObjectHandler<RequestResetAuthorizations, IBool>,
    IResetAuthorizationsHandler, IProcessedHandler
{
    private readonly IEventBus _eventBus;
    private readonly IObjectMessageSender _messageSender;
    private readonly IQueryProcessor _queryProcessor;

    public ResetAuthorizationsHandler(
        IQueryProcessor queryProcessor,
        IObjectMessageSender messageSender,
        IEventBus eventBus)
    {
        _queryProcessor = queryProcessor;
        _messageSender = messageSender;
        _eventBus = eventBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetAuthorizations obj)
    {
        var deviceList = await _queryProcessor
            .ProcessAsync(new GetDeviceByUidQuery(input.UserId), CancellationToken.None);
        foreach (var deviceReadModel in deviceList)
        {
            if (deviceReadModel.PermAuthKeyId == input.PermAuthKeyId) continue;

            // var command = new UnRegisterAuthKeyCommand(AuthKeyId.Create(deviceReadModel.PermAuthKeyId));
            // await _commandBus.PublishAsync(command, CancellationToken.None);
            await _eventBus.PublishAsync(new UnRegisterAuthKeyEvent(deviceReadModel.PermAuthKeyId))
                ;
        }

        var updatesTooLong = new TUpdatesTooLong();
        //await SendMessageToPeerAsync(new Peer(PeerType.User, input.UserId),
        //    updatesTooLong,
        //    3,
        //    excludeAuthKeyId: input.AuthKeyId);
        await _messageSender.PushMessageToPeerAsync(new Peer(PeerType.User, input.UserId),
            updatesTooLong,
            //3,
            input.AuthKeyId);

        return new TBoolTrue();
    }
}