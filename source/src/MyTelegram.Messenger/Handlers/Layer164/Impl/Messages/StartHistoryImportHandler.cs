// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Complete the <a href="https://corefork.telegram.org/api/import">history import process</a>, importing all messages into the chat.<br>
/// To be called only after initializing the import with <a href="https://corefork.telegram.org/method/messages.initHistoryImport">messages.initHistoryImport</a> and uploading all files using <a href="https://corefork.telegram.org/method/messages.uploadImportedMedia">messages.uploadImportedMedia</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 IMPORT_ID_INVALID The specified import ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.startHistoryImport" />
///</summary>
internal sealed class StartHistoryImportHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestStartHistoryImport, IBool>,
    Messages.IStartHistoryImportHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestStartHistoryImport obj)
    {
        throw new NotImplementedException();
    }
}
