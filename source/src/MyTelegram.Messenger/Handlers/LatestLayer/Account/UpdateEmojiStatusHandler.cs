namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Set an <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a>
/// Possible errors
/// Code Type Description
/// 400 COLLECTIBLE_INVALID The specified collectible is invalid.
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateEmojiStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateEmojiStatus, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateEmojiStatus obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}