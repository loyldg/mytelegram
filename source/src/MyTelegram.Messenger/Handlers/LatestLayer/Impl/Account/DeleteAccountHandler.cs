// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Delete the user's account from the telegram servers.Can also be used to delete the account of a user that provided the login code, but forgot the 2FA password and no recovery method is configured, see <a href="https://corefork.telegram.org/api/srp#password-recovery">here »</a> for more info on password recovery, and <a href="https://corefork.telegram.org/api/account-deletion">here »</a> for more info on account deletion.
/// <para>Possible errors</para>
/// Code Type Description
/// 420 2FA_CONFIRM_WAIT_%d Since this account is active and protected by a 2FA password, we will delete it in 1 week for security purposes. You can cancel this process at any time, you'll be able to reset your account in %d seconds.
/// See <a href="https://corefork.telegram.org/method/account.deleteAccount" />
///</summary>
internal sealed class DeleteAccountHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteAccount, IBool>,
    Account.IDeleteAccountHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeleteAccount obj)
    {
        throw new NotImplementedException();
    }
}
