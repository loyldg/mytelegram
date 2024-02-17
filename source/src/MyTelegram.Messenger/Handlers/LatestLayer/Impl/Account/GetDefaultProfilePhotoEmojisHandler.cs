// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get a set of suggested <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickers</a> that can be <a href="https://corefork.telegram.org/api/files#sticker-profile-pictures">used as profile picture</a>
/// See <a href="https://corefork.telegram.org/method/account.getDefaultProfilePhotoEmojis" />
///</summary>
internal sealed class GetDefaultProfilePhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultProfilePhotoEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = new()
        });
    }
}
