// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Deletes a device by its token, stops sending PUSH-notifications to it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOKEN_INVALID The provided token is invalid.
/// See <a href="https://corefork.telegram.org/method/account.unregisterDevice" />
///</summary>
internal sealed class UnregisterDeviceHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUnregisterDevice, IBool>,
    Account.IUnregisterDeviceHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUnregisterDevice obj)
    {
        throw new NotImplementedException();
    }
}
