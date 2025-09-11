namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getCollectibleEmojiStatuses" />
///</summary>
internal sealed class GetCollectibleEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetCollectibleEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetCollectibleEmojiStatuses obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IEmojiStatuses>(new TEmojiStatuses
        {
            Statuses = []
        });
    }
}
