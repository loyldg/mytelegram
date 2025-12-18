namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Cancel the login verification code
/// Possible errors
/// Code Type Description
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.cancelCode"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class CancelCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestCancelCode, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, RequestCancelCode obj)
    {
        // todo:cancel code
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}