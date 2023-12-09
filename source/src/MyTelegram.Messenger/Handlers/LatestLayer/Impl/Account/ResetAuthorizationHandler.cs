// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Log out an active <a href="https://corefork.telegram.org/api/auth">authorized session</a> by its hash
/// <para>Possible errors</para>
/// Code Type Description
/// 406 FRESH_RESET_AUTHORISATION_FORBIDDEN You can't logout other sessions if less than 24 hours have passed since you logged on the current session.
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.resetAuthorization" />
///</summary>
internal sealed class ResetAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetAuthorization, IBool>,
    Account.IResetAuthorizationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
