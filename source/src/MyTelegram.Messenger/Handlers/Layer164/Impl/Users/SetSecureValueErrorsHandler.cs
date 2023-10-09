// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// Notify the user that the sent <a href="https://corefork.telegram.org/passport">passport</a> data contains some errors The user will not be able to re-submit their Passport data to you until the errors are fixed (the contents of the field for which you returned the error must change).Use this if the data submitted by the user doesn't satisfy the standards your service requires for any reason. For example, if a birthday date seems invalid, a submitted document is blurry, a scan shows evidence of tampering, etc. Supply some details in the error message to make sure the user knows how to correct the issues.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/users.setSecureValueErrors" />
///</summary>
internal sealed class SetSecureValueErrorsHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestSetSecureValueErrors, IBool>,
    Users.ISetSecureValueErrorsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestSetSecureValueErrors obj)
    {
        throw new NotImplementedException();
    }
}
