// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Sends a Telegram Passport authorization form, effectively sharing data with the service
/// See <a href="https://corefork.telegram.org/method/account.acceptAuthorization" />
///</summary>
internal sealed class AcceptAuthorizationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestAcceptAuthorization, IBool>,
    Account.IAcceptAuthorizationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestAcceptAuthorization obj)
    {
        throw new NotImplementedException();
    }
}
