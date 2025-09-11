namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get theme information
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_FORMAT_INVALID Invalid theme format provided.
/// 400 THEME_INVALID Invalid theme provided.
/// See <a href="https://corefork.telegram.org/method/account.getTheme" />
///</summary>
internal sealed class GetThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetTheme, MyTelegram.Schema.ITheme>
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetTheme obj)
    {
        throw new NotImplementedException();
    }
}
