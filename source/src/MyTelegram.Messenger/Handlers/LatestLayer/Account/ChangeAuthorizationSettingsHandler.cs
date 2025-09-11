namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Change settings related to a session.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.changeAuthorizationSettings" />
///</summary>
internal sealed class ChangeAuthorizationSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestChangeAuthorizationSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestChangeAuthorizationSettings obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
