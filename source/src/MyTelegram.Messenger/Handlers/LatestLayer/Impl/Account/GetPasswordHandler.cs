// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Obtain configuration for two-factor authorization with password
/// See <a href="https://corefork.telegram.org/method/account.getPassword" />
///</summary>
internal sealed class GetPasswordHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPassword, MyTelegram.Schema.Account.IPassword>,
    Account.IGetPasswordHandler
{
    protected override Task<MyTelegram.Schema.Account.IPassword> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetPassword obj)
    {
        throw new NotImplementedException();
    }
}
