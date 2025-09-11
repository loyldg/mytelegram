using EventFlow;
using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Log out an active web <a href="https://corefork.telegram.org/widgets/login">telegram login</a> session
/// <para>Possible errors</para>
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.resetWebAuthorization" />
///</summary>
internal sealed class ResetWebAuthorizationHandler(
    IQueryProcessor queryProcessor,
    ILogger<ResetAuthorizationHandler> logger,
    ICommandBus commandBus,
    IEventBus eventBus) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetWebAuthorization, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetWebAuthorization obj)
    {
        var deviceReadModel = await queryProcessor
            .ProcessAsync(new GetDeviceByHashQuery(input.UserId, obj.Hash));
        if (deviceReadModel != null)
        {
            await eventBus.PublishAsync(new UnRegisterAuthKeyEvent(deviceReadModel.PermAuthKeyId, deviceReadModel.UserId));
            var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(deviceReadModel.PermAuthKeyId),
                deviceReadModel.PermAuthKeyId, deviceReadModel.TempAuthKeyId);
            await commandBus.PublishAsync(command);
        }
        else
        {
            logger.LogWarning("Cannot find device, userId: {UserId}, hash: {Hash}", input.UserId, obj.Hash);
        }

        return new TBoolTrue();
    }
}
