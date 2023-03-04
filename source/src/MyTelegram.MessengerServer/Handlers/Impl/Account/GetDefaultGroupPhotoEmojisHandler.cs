// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultGroupPhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultGroupPhotoEmojisHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis obj)
    {
        return Task.FromResult<IEmojiList>(new TEmojiListNotModified());
    }
}
