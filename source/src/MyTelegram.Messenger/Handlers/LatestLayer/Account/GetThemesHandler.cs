namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get installed themes
/// See <a href="https://corefork.telegram.org/method/account.getThemes" />
///</summary>
internal sealed class GetThemesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetThemes, MyTelegram.Schema.Account.IThemes>
{
    protected override Task<MyTelegram.Schema.Account.IThemes> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetThemes obj)
    {
        var r = new TThemes { Themes = new TVector<ITheme>(), Hash = obj.Hash };

        return Task.FromResult<IThemes>(r);
    }
}
