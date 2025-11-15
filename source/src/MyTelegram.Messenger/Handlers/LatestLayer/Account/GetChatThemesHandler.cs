namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get all available chat <a href="https://corefork.telegram.org/api/themes">themes »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getChatThemes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetChatThemesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChatThemes, MyTelegram.Schema.Account.IThemes>
{
    protected override Task<MyTelegram.Schema.Account.IThemes> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetChatThemes obj)
    {
        return Task.FromResult<IThemes>(new TThemes { Themes = new(), });
    }
}