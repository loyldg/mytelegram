// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Fetch <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickers »</a>.Returns a list of <a href="https://corefork.telegram.org/constructor/document">documents</a> with the animated custom emoji in TGS format, and a <a href="https://corefork.telegram.org/constructor/documentAttributeCustomEmoji">documentAttributeCustomEmoji</a> attribute with the original emoji and info about the emoji stickerset this custom emoji belongs to.
/// See <a href="https://corefork.telegram.org/method/messages.getCustomEmojiDocuments" />
///</summary>
internal sealed class GetCustomEmojiDocumentsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetCustomEmojiDocuments, TVector<MyTelegram.Schema.IDocument>>,
    Messages.IGetCustomEmojiDocumentsHandler
{
    protected override Task<TVector<MyTelegram.Schema.IDocument>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetCustomEmojiDocuments obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IDocument>>(new TVector<IDocument>());
        //throw new NotImplementedException();
    }
}
