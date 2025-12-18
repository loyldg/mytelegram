namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// When client-side passcode lock feature is enabled, will not show message texts in incoming <a href="https://corefork.telegram.org/api/push-updates">PUSH notifications</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateDeviceLocked"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateDeviceLockedHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateDeviceLocked, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateDeviceLocked obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}