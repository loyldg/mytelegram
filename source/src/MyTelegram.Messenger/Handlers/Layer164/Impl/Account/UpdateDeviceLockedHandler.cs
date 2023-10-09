// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// When client-side passcode lock feature is enabled, will not show message texts in incoming <a href="https://corefork.telegram.org/api/push-updates">PUSH notifications</a>.
/// See <a href="https://corefork.telegram.org/method/account.updateDeviceLocked" />
///</summary>
internal sealed class UpdateDeviceLockedHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateDeviceLocked, IBool>,
    Account.IUpdateDeviceLockedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateDeviceLocked obj)
    {
        throw new NotImplementedException();
    }
}
