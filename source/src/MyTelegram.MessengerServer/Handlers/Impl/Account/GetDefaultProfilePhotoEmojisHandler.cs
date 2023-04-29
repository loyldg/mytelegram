// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultProfilePhotoEmojisHandler :
    RpcResultObjectHandler<RequestGetDefaultProfilePhotoEmojis, Schema.IEmojiList>,
    Account.IGetDefaultProfilePhotoEmojisHandler, IProcessedHandler
{
    protected override Task<Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        RequestGetDefaultProfilePhotoEmojis obj)
    {
        return Task.FromResult<Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = new()
        });
    }
}
