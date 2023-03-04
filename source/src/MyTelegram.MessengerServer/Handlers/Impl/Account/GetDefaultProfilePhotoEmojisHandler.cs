// ReSharper disable All

namespace MyTelegram.Handlers.Account;

internal sealed class GetDefaultProfilePhotoEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultProfilePhotoEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultProfilePhotoEmojis obj)
    {
        throw new NotImplementedException();
    }
}
