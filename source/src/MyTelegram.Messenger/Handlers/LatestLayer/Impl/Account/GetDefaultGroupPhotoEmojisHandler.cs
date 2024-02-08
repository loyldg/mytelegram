// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get a set of suggested <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickers</a> that can be <a href="https://corefork.telegram.org/api/files#sticker-profile-pictures">used as group picture</a>
/// See <a href="https://corefork.telegram.org/method/account.getDefaultGroupPhotoEmojis" />
///</summary>
internal sealed class GetDefaultGroupPhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultGroupPhotoEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis obj)
    {
        return Task.FromResult<IEmojiList>(new TEmojiList
        {
            DocumentId = new(),
        });
    }
}
