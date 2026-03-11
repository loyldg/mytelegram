namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Complete the <a href="https://corefork.telegram.org/api/import">history import process</a>, importing all messages into the chat.<br/>
/// To be called only after initializing the import with <a href="https://corefork.telegram.org/method/messages.initHistoryImport">messages.initHistoryImport</a> and uploading all files using <a href="https://corefork.telegram.org/method/messages.uploadImportedMedia">messages.uploadImportedMedia</a>.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 IMPORT_ID_INVALID The specified import ID is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.startHistoryImport"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class StartHistoryImportHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestStartHistoryImport, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestStartHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}