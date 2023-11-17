// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Whether the user will receive notifications when contacts sign up
/// See <a href="https://corefork.telegram.org/method/account.getContactSignUpNotification" />
///</summary>
internal sealed class GetContactSignUpNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContactSignUpNotification, IBool>,
    Account.IGetContactSignUpNotificationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContactSignUpNotification obj)
    {
        throw new NotImplementedException();
    }
}
