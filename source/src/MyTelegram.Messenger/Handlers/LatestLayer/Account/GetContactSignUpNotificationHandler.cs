namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Whether the user will receive notifications when contacts sign up
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getContactSignUpNotification"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetContactSignUpNotificationHandler(IUserAppService userAppService) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContactSignUpNotification, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetContactSignUpNotification obj)
    {
        var user = await userAppService.GetAsync(input.UserId);
        return user!.ShowContactSignUpNotification ? new TBoolTrue() : new TBoolFalse();
    }
}