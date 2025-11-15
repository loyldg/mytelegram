namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get theme information
/// Possible errors
/// Code Type Description
/// 400 THEME_FORMAT_INVALID Invalid theme format provided.
/// 400 THEME_INVALID Invalid theme provided.
/// 400 THEME_SLUG_INVALID The specified theme slug is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getTheme"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetTheme, MyTelegram.Schema.ITheme>
{
    protected override Task<MyTelegram.Schema.ITheme> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetTheme obj)
    {
        throw new NotImplementedException();
    }
}