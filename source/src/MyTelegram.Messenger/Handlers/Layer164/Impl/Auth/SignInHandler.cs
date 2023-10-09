// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Signs in a user with a validated phone number.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 AUTH_RESTART Restart the authorization process.
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_INVALID The provided phone code is invalid.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_UNOCCUPIED The phone number is not yet being used.
/// 500 SIGN_IN_FAILED Failure while signing in.
/// See <a href="https://corefork.telegram.org/method/auth.signIn" />
///</summary>
internal sealed class SignInHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestSignIn, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.ISignInHandler
{
    protected override Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestSignIn obj)
    {
        throw new NotImplementedException();
    }
}
