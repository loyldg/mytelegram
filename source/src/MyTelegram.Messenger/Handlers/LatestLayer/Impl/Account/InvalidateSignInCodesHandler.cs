// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.invalidateSignInCodes" />
///</summary>
internal sealed class InvalidateSignInCodesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestInvalidateSignInCodes, IBool>,
    Account.IInvalidateSignInCodesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestInvalidateSignInCodes obj)
    {
        throw new NotImplementedException();
    }
}
