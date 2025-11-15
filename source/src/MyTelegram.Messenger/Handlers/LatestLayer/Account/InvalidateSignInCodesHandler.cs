namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Invalidate the specified login codes, see <a href="https://corefork.telegram.org/api/auth#invalidating-login-codes">here »</a> for more info.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.invalidateSignInCodes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class InvalidateSignInCodesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInvalidateSignInCodes, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestInvalidateSignInCodes obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}