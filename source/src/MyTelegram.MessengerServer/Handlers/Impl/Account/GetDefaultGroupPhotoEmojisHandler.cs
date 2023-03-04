// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultGroupPhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultGroupPhotoEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultGroupPhotoEmojis obj)
    {
        throw new NotImplementedException();
    }
}
