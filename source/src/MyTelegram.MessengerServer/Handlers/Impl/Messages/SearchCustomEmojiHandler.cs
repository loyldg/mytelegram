// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

internal sealed class SearchCustomEmojiHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchCustomEmoji, MyTelegram.Schema.IEmojiList>,
    Messages.ISearchCustomEmojiHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchCustomEmoji obj)
    {
        throw new NotImplementedException();
    }
}
