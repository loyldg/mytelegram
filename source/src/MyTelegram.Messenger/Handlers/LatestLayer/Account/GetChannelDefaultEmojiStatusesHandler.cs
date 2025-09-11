namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get a list of default suggested <a href="https://corefork.telegram.org/api/emoji-status">channel emoji statuses</a>.
/// See <a href="https://corefork.telegram.org/method/account.getChannelDefaultEmojiStatuses" />
///</summary>
internal sealed class GetChannelDefaultEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChannelDefaultEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetChannelDefaultEmojiStatuses obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IEmojiStatuses>(new TEmojiStatuses
        {
            Statuses = []
        });
    }
}
