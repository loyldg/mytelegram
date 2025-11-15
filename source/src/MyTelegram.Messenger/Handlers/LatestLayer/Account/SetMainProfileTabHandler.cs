namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Changes the main profile tab of the current user, see <a href="https://corefork.telegram.org/api/profile#tabs">here »</a> for more info.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.setMainProfileTab"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetMainProfileTabHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetMainProfileTab, IBool>, IObjectHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSetMainProfileTab obj)
    {
        return new TBoolTrue();
    }
}