// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get installed themes
/// See <a href="https://corefork.telegram.org/method/account.getThemes" />
///</summary>
internal sealed class GetThemesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetThemes, MyTelegram.Schema.Account.IThemes>,
    Account.IGetThemesHandler
{
    protected override Task<MyTelegram.Schema.Account.IThemes> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetThemes obj)
    {
        throw new NotImplementedException();
    }
}
