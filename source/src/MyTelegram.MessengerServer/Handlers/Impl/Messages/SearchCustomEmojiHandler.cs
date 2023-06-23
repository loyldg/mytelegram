// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

internal sealed class SearchCustomEmojiHandler : RpcResultObjectHandler<RequestSearchCustomEmoji, Schema.IEmojiList>,
    Messages.ISearchCustomEmojiHandler
{
    protected override Task<Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        RequestSearchCustomEmoji obj)
    {
        throw new NotImplementedException();
    }
}