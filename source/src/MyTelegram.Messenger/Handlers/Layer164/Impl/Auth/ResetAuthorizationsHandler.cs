// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Terminates all user's authorized sessions except for the current one.After calling this method it is necessary to reregister the current device using the method <a href="https://corefork.telegram.org/method/account.registerDevice">account.registerDevice</a>
/// See <a href="https://corefork.telegram.org/method/auth.resetAuthorizations" />
///</summary>
internal sealed class ResetAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResetAuthorizations, IBool>,
    Auth.IResetAuthorizationsHandler, IProcessedHandler
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
            .ProcessAsync(new GetDeviceByUidQuery(input.UserId), default);
        foreach (var deviceReadModel in deviceList)
        {
            if (deviceReadModel.PermAuthKeyId == input.PermAuthKeyId)
            {
                continue;
            }

            await _eventBus.PublishAsync(new UnRegisterAuthKeyEvent(deviceReadModel.PermAuthKeyId));
        }

        var updatesTooLong = new TUpdatesTooLong();

        await _messageSender.PushMessageToPeerAsync(new Peer(PeerType.User, input.UserId),
            updatesTooLong,
            input.AuthKeyId);

        return new TBoolTrue();
    }
}
