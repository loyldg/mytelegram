// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetCustomEmojiDocumentsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetCustomEmojiDocuments, TVector<MyTelegram.Schema.IDocument>>,
    Messages.IGetCustomEmojiDocumentsHandler
{
    protected override Task<TVector<MyTelegram.Schema.IDocument>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetCustomEmojiDocuments obj)
    {
        throw new NotImplementedException();
    }
}
