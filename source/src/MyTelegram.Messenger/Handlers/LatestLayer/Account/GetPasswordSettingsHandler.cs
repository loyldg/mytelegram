namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get private info associated to the password info (recovery email, telegram <a href="https://corefork.telegram.org/passport">passport</a> info &amp; so on)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getPasswordSettings" />
///</summary>
internal sealed class GetPasswordSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPasswordSettings, MyTelegram.Schema.Account.IPasswordSettings>
{
    protected override Task<MyTelegram.Schema.Account.IPasswordSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetPasswordSettings obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IPasswordSettings>(
            new MyTelegram.Schema.Account.TPasswordSettings
            {
                
            });
    }
}
