namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Reset the <a href="https://core.telegram.org/api/auth#email-verification">login email »</a>.
/// Possible errors
/// Code Type Description
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 TASK_ALREADY_EXISTS An email reset was already requested.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.resetLoginEmail"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class ResetLoginEmailHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestResetLoginEmail, MyTelegram.Schema.Auth.ISentCode>
{
    protected override Task<MyTelegram.Schema.Auth.ISentCode> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestResetLoginEmail obj)
    {
        throw new NotImplementedException();
    }
}