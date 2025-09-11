namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get a document by its SHA256 hash, mainly used for gifs
/// <para>Possible errors</para>
/// Code Type Description
/// 400 SHA256_HASH_INVALID The provided SHA256 hash is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDocumentByHash" />
///</summary>
internal sealed class GetDocumentByHashHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDocumentByHash, MyTelegram.Schema.IDocument>
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDocumentByHash obj)
    {
        throw new NotImplementedException();
    }
}
