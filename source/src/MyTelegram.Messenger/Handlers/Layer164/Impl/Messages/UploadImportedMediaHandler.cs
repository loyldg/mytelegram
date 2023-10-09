// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Upload a media file associated with an <a href="https://corefork.telegram.org/api/import">imported chat, click here for more info »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/messages.uploadImportedMedia" />
///</summary>
internal sealed class UploadImportedMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUploadImportedMedia, MyTelegram.Schema.IMessageMedia>,
    Messages.IUploadImportedMediaHandler
{
    protected override Task<MyTelegram.Schema.IMessageMedia> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUploadImportedMedia obj)
    {
        throw new NotImplementedException();
    }
}
