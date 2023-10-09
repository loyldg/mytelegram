// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Returns a Telegram Passport authorization form for sharing data with a service
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PUBLIC_KEY_REQUIRED A public key is required.
/// See <a href="https://corefork.telegram.org/method/account.getAuthorizationForm" />
///</summary>
internal sealed class GetAuthorizationFormHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAuthorizationForm, MyTelegram.Schema.Account.IAuthorizationForm>,
    Account.IGetAuthorizationFormHandler
{
    protected override Task<MyTelegram.Schema.Account.IAuthorizationForm> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAuthorizationForm obj)
    {
        throw new NotImplementedException();
    }
}
