namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Toggle contact sign up notifications
/// <para><c>See <a href="https://corefork.telegram.org/method/account.setContactSignUpNotification"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetContactSignUpNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetContactSignUpNotification, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestSetContactSignUpNotification obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}