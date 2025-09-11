namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Import chat history from a foreign chat app into a specific Telegram chat, <a href="https://corefork.telegram.org/api/import">click here for more info about imported chats »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 IMPORT_FILE_INVALID The specified chat export file is invalid.
/// 400 IMPORT_FORMAT_DATE_INVALID The date specified in the import file is invalid.
/// 400 IMPORT_FORMAT_UNRECOGNIZED The specified chat export file was exported from an unsupported chat app.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 406 PREVIOUS_CHAT_IMPORT_ACTIVE_WAIT_%dMIN Import for this chat is already in progress, wait %d minutes before starting a new one.
/// See <a href="https://corefork.telegram.org/method/messages.initHistoryImport" />
///</summary>
internal sealed class InitHistoryImportHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestInitHistoryImport, MyTelegram.Schema.Messages.IHistoryImport>
{
    protected override Task<MyTelegram.Schema.Messages.IHistoryImport> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestInitHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}
