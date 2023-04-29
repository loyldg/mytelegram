// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetCustomEmojiDocumentsHandler :
    RpcResultObjectHandler<RequestGetCustomEmojiDocuments, TVector<Schema.IDocument>>,
    Messages.IGetCustomEmojiDocumentsHandler
{
    protected override Task<TVector<Schema.IDocument>> HandleCoreAsync(IRequestInput input,
        RequestGetCustomEmojiDocuments obj)
    {
        throw new NotImplementedException();
    }
}
