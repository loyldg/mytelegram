namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get a document by its SHA256 hash, mainly used for gifs
/// Possible errors
/// Code Type Description
/// 400 SHA256_HASH_INVALID The provided SHA256 hash is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getDocumentByHash"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetDocumentByHashHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDocumentByHash, MyTelegram.Schema.IDocument>
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetDocumentByHash obj)
    {
        throw new NotImplementedException();
    }
}