// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Change authorization settings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.changeAuthorizationSettings" />
///</summary>
internal sealed class ChangeAuthorizationSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestChangeAuthorizationSettings, IBool>,
    Account.IChangeAuthorizationSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestChangeAuthorizationSettings obj)
    {
        throw new NotImplementedException();
    }
}
