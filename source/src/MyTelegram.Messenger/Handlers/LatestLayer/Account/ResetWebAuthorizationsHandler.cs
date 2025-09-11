using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Reset all active web <a href="https://corefork.telegram.org/widgets/login">telegram login</a> sessions
/// See <a href="https://corefork.telegram.org/method/account.resetWebAuthorizations" />
///</summary>
internal sealed class ResetWebAuthorizationsHandler(
    IQueryProcessor queryProcessor,
    ICommandBus commandBus,
    IEventBus eventBus) : RpcResultObjectHandler<Schema.Account.RequestResetWebAuthorizations, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestResetWebAuthorizations obj)
    {
        var deviceReadModels = await queryProcessor
            .ProcessAsync(new GetDeviceByUserIdQuery(input.UserId));

        List<long> revokedPermAuthKeyIds = [];
        foreach (var deviceReadModel in deviceReadModels)
        {
            if (deviceReadModel.PermAuthKeyId != input.PermAuthKeyId && deviceReadModel.AppVersion.Contains("web"))
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