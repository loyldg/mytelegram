// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Set time-to-live of current session
/// <para>Possible errors</para>
/// Code Type Description
/// 406 FRESH_RESET_AUTHORISATION_FORBIDDEN You can't logout other sessions if less than 24 hours have passed since you logged on the current session.
/// 400 TTL_DAYS_INVALID The provided TTL is invalid.
/// See <a href="https://corefork.telegram.org/method/account.setAuthorizationTTL" />
///</summary>
internal sealed class SetAuthorizationTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetAuthorizationTTL, IBool>,
    Account.ISetAuthorizationTTLHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetAuthorizationTTL obj)
    {
        throw new NotImplementedException();
    }
}
