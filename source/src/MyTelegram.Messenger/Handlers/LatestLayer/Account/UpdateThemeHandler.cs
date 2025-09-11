namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Update theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_INVALID Invalid theme provided.
/// See <a href="https://corefork.telegram.org/method/account.updateTheme" />
///</summary>
internal sealed class UpdateThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateTheme, MyTelegram.Schema.ITheme>
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateTheme obj)
    {
        throw new NotImplementedException();
    }
}
