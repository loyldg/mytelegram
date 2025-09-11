namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Whether the user will receive notifications when contacts sign up
/// See <a href="https://corefork.telegram.org/method/account.getContactSignUpNotification" />
///</summary>
internal sealed class GetContactSignUpNotificationHandler(IUserAppService userAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContactSignUpNotification, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContactSignUpNotification obj)
    {
        var user = await userAppService.GetAsync(input.UserId);

        return user!.ShowContactSignUpNotification ? new TBoolTrue() : new TBoolFalse();
    }
}
