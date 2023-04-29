// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultGroupPhotoEmojisHandler :
    RpcResultObjectHandler<RequestGetDefaultGroupPhotoEmojis, Schema.IEmojiList>,
    Account.IGetDefaultGroupPhotoEmojisHandler, IProcessedHandler
{
    protected override Task<Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        RequestGetDefaultGroupPhotoEmojis obj)
    {
        return Task.FromResult<IEmojiList>(new TEmojiListNotModified());
    }
}
