namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get installed themes
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getThemes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetThemesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetThemes, MyTelegram.Schema.Account.IThemes>
{
    protected override Task<MyTelegram.Schema.Account.IThemes> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetThemes obj)
    {
        var r = new TThemes
        {
            Themes = new TVector<ITheme>(),
            Hash = obj.Hash
        };
        return Task.FromResult<IThemes>(r);
    }
}