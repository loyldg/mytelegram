// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultProfilePhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultProfilePhotoEmojisHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = new(),
        });
    }
}
