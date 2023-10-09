// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Log out an active web <a href="https://corefork.telegram.org/widgets/login">telegram login</a> session
/// <para>Possible errors</para>
/// Code Type Description
/// 400 HASH_INVALID The provided hash is invalid.
/// See <a href="https://corefork.telegram.org/method/account.resetWebAuthorization" />
///</summary>
internal sealed class ResetWebAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResetWebAuthorization, IBool>,
    Account.IResetWebAuthorizationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResetWebAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
