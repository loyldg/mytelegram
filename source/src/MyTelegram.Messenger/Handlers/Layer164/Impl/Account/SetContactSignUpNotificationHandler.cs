// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Toggle contact sign up notifications
/// See <a href="https://corefork.telegram.org/method/account.setContactSignUpNotification" />
///</summary>
internal sealed class SetContactSignUpNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSetContactSignUpNotification, IBool>,
    Account.ISetContactSignUpNotificationHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSetContactSignUpNotification obj)
    {
        throw new NotImplementedException();
    }
}
