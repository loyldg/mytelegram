namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Auth;

///<summary>
/// Registers a validated phone number in the system.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// 400 LASTNAME_INVALID The last name is invalid.
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_INVALID The provided phone code is invalid.
/// 400 PHONE_NUMBER_FLOOD You asked for the code too many times.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_OCCUPIED The phone number is already in use.
/// See <a href="https://corefork.telegram.org/method/auth.signUp" />
///</summary>
internal sealed class SignUpHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Auth.LayerN.RequestSignUp,
        MyTelegram.Schema.Auth.RequestSignUp> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Auth.LayerN.RequestSignUp,
            MyTelegram.Schema.Auth.RequestSignUp
        >(handlerHelper, dataConverter), IDistinctObjectHandler
{
}