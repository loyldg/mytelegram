// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get private info associated to the password info (recovery email, telegram <a href="https://corefork.telegram.org/passport">passport</a> info &amp; so on)
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.getPasswordSettings" />
///</summary>
internal sealed class GetPasswordSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPasswordSettings, MyTelegram.Schema.Account.IPasswordSettings>,
    Account.IGetPasswordSettingsHandler
{
    protected override Task<MyTelegram.Schema.Account.IPasswordSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetPasswordSettings obj)
    {
        throw new NotImplementedException();
    }
}
