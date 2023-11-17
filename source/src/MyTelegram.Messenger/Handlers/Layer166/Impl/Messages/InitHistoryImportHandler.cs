// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Import chat history from a foreign chat app into a specific Telegram chat, <a href="https://corefork.telegram.org/api/import">click here for more info about imported chats »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 IMPORT_FILE_INVALID The specified chat export file is invalid.
/// 400 IMPORT_FORMAT_UNRECOGNIZED The specified chat export file was exported from an unsupported chat app.
/// 406 PREVIOUS_CHAT_IMPORT_ACTIVE_WAIT_%dMIN Import for this chat is already in progress, wait %d minutes before starting a new one.
/// See <a href="https://corefork.telegram.org/method/messages.initHistoryImport" />
///</summary>
internal sealed class InitHistoryImportHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestInitHistoryImport, MyTelegram.Schema.Messages.IHistoryImport>,
    Messages.IInitHistoryImportHandler
{
    protected override Task<MyTelegram.Schema.Messages.IHistoryImport> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestInitHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}
