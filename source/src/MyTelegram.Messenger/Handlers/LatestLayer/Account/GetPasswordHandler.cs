namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Obtain configuration for two-factor authorization with password
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getPassword"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class GetPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPassword, MyTelegram.Schema.Account.IPassword>
{
    private readonly IRandomHelper _randomHelper;
    public GetPasswordHandler(IRandomHelper randomHelper)
    {
        _randomHelper = randomHelper;
    }

    protected override Task<MyTelegram.Schema.Account.IPassword> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetPassword obj)
    {
        var password = new TPassword();
        password.NewAlgo = new TPasswordKdfAlgoUnknown();
        password.NewSecureAlgo = new TSecurePasswordKdfAlgoUnknown();
        var secureRandom = new byte[256];
        _randomHelper.NextBytes(secureRandom);
        password.SecureRandom = secureRandom;
        return Task.FromResult<IPassword>(password);
    }
}