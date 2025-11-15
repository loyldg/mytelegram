namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Obtain a list of <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses »</a> for owned <a href="https://corefork.telegram.org/api/gifts#collectible-gifts">collectible gifts</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getCollectibleEmojiStatuses"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetCollectibleEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetCollectibleEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetCollectibleEmojiStatuses obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IEmojiStatuses>(new TEmojiStatuses { Statuses = [] });
    }
}