// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Set a new 2FA password
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMAIL_UNCONFIRMED_%d The provided email isn't confirmed, %d is the length of the verification code that was just sent to the email: use <a href="https://corefork.telegram.org/method/account.verifyEmail">account.verifyEmail</a> to enter the received verification code and enable the recovery email.
/// 400 EMAIL_INVALID The specified email is invalid.
/// 400 EMAIL_UNCONFIRMED Email unconfirmed.
/// 400 NEW_SALT_INVALID The new salt is invalid.
/// 400 NEW_SETTINGS_EMPTY No password is set on the current account, and no new password was specified in <code>new_settings</code>.
/// 400 NEW_SETTINGS_INVALID The new password settings are invalid.
/// 400 PASSWORD_HASH_INVALID The provided password hash is invalid.
/// 400 SRP_ID_INVALID Invalid SRP ID provided.
/// 400 SRP_PASSWORD_CHANGED Password has changed.
/// See <a href="https://corefork.telegram.org/method/account.updatePasswordSettings" />
///</summary>
internal sealed class UpdatePasswordSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdatePasswordSettings, IBool>,
    Account.IUpdatePasswordSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdatePasswordSettings obj)
    {
        throw new NotImplementedException();
    }
}
