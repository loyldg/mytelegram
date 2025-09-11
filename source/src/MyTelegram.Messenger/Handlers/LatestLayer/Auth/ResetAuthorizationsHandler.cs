using EventFlow;
using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

///<summary>
/// Terminates all user's authorized sessions except for the current one.After calling this method it is necessary to reregister the current device using the method <a href="https://corefork.telegram.org/method/account.registerDevice">account.registerDevice</a>
/// See <a href="https://corefork.telegram.org/method/auth.resetAuthorizations" />
///</summary>
internal sealed class ResetAuthorizationsHandler(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    IEventBus eventBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResetAuthorizations, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestResetAuthorizations obj)
    {
        var deviceReadModels = await queryProcessor
            .ProcessAsync(new GetDeviceByUserIdQuery(input.UserId));

        List<long> revokedPermAuthKeyIds = [];
        foreach (var deviceReadModel in deviceReadModels)
        {
            if (deviceReadModel.PermAuthKeyId != input.PermAuthKeyId)
            {
                var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(deviceReadModel.PermAuthKeyId),
                    deviceReadModel.PermAuthKeyId, deviceReadModel.TempAuthKeyId);
                await commandBus.PublishAsync(command);
                revokedPermAuthKeyIds.Add(deviceReadModel.PermAuthKeyId);
            }
        }
        await eventBus.PublishAsync(new SessionRevokedEvent(input.PermAuthKeyId, input.UserId, revokedPermAuthKeyIds));

        return new TBoolTrue();
    }
}
