using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Log out an active <a href="https://corefork.telegram.org/api/auth">authorized session</a> by its hash
/// <para>Possible errors</para>
/// Code Type Description
/// 406 FRESH_RESET_AUTHORISATION_FORBIDDEN You can't logout other sessions if less than 24 hours have passed since you logged on the current session.
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.resetAuthorization" />
///</summary>
internal sealed class ResetAuthorizationHandler(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IObjectMessageSender messageSender,
    ILogger<ResetAuthorizationHandler> logger,
    IEventBus eventBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetAuthorization, IBool>

{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetAuthorization obj)
    {
        var deviceReadModel = await queryProcessor
            .ProcessAsync(new GetDeviceByHashQuery(input.UserId, obj.Hash));
        if (deviceReadModel != null)
        {
            await eventBus.PublishAsync(new UnRegisterAuthKeyEvent(deviceReadModel.PermAuthKeyId, input.UserId));
            var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(deviceReadModel.PermAuthKeyId),
                deviceReadModel.PermAuthKeyId, deviceReadModel.TempAuthKeyId);
            await commandBus.PublishAsync(command);
        }
        else
        {
            logger.LogWarning("Cannot find device data, userId: {UserId}, hash: {Hash}", input.UserId, obj.Hash);
        }

        return new TBoolTrue();
    }
}
