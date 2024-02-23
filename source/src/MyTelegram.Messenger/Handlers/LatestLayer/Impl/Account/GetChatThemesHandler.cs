// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get all available chat themes
/// See <a href="https://corefork.telegram.org/method/account.getChatThemes" />
///</summary>
internal sealed class GetChatThemesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChatThemes, MyTelegram.Schema.Account.IThemes>,
    Account.IGetChatThemesHandler
{
    protected override Task<MyTelegram.Schema.Account.IThemes> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetChatThemes obj)
    {
        return Task.FromResult<IThemes>(new TThemes
        {
            Themes = new(),
        });
    }
}
