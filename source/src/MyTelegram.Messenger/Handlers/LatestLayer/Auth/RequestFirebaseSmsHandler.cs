namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Request an SMS code via Firebase.
/// Possible errors
/// Code Type Description
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_NUMBER_INVALID The phone number is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.requestFirebaseSms"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class RequestFirebaseSmsHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestRequestFirebaseSms, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Auth.RequestRequestFirebaseSms obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}