// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Returns list of chats with non-default notification settings
/// See <a href="https://corefork.telegram.org/method/account.getNotifyExceptions" />
///</summary>
internal sealed class GetNotifyExceptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetNotifyExceptions, MyTelegram.Schema.IUpdates>,
    Account.IGetNotifyExceptionsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetNotifyExceptions obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = new TVector<IUpdate>(),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = CurrentDate
        });
    }
}
