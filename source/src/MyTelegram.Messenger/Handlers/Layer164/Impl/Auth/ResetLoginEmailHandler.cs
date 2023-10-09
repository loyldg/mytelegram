// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Reset the <a href="https://core.telegram.org/api/auth#email-verification">login email »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 TASK_ALREADY_EXISTS An email reset was already requested.
/// See <a href="https://corefork.telegram.org/method/auth.resetLoginEmail" />
///</summary>
internal sealed class ResetLoginEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResetLoginEmail, MyTelegram.Schema.Auth.ISentCode>,
    Auth.IResetLoginEmailHandler
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestResetLoginEmail obj)
    {
        throw new NotImplementedException();
    }
}
