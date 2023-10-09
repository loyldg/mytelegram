// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Changes username for the current user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
/// See <a href="https://corefork.telegram.org/method/account.updateUsername" />
///</summary>
internal sealed class UpdateUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateUsername, MyTelegram.Schema.IUser>,
    Account.IUpdateUsernameHandler
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateUsername obj)
    {
        throw new NotImplementedException();
    }
}
