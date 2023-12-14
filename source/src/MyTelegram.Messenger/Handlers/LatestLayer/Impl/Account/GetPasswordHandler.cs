// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Obtain configuration for two-factor authorization with password
/// See <a href="https://corefork.telegram.org/method/account.getPassword" />
///</summary>
internal sealed class GetPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPassword, MyTelegram.Schema.Account.IPassword>,
    Account.IGetPasswordHandler
{
    private readonly IRandomHelper _randomHelper;

    public GetPasswordHandler(IRandomHelper randomHelper)
    {
        _randomHelper = randomHelper;
    }

    protected override Task<MyTelegram.Schema.Account.IPassword> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetPassword obj)
    {
        var password = new TPassword();

        password.NewAlgo = new TPasswordKdfAlgoUnknown();
        password.NewSecureAlgo = new TSecurePasswordKdfAlgoUnknown();

        var secureRandom = new byte[256];
        _randomHelper.NextBytes(secureRandom);

        password.SecureRandom=secureRandom;


        return Task.FromResult<IPassword>(password);
    }
}
