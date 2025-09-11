namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Returns list of chats with non-default notification settings
/// See <a href="https://corefork.telegram.org/method/account.getNotifyExceptions" />
///</summary>
internal sealed class GetNotifyExceptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetNotifyExceptions, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetNotifyExceptions obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = [],
            Chats = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
