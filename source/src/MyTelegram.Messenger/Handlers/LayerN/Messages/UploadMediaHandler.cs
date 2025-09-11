namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Messages;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class UploadMediaHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Messages.LayerN.RequestUploadMedia,
        MyTelegram.Schema.Messages.RequestUploadMedia> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Messages.LayerN.RequestUploadMedia,
            MyTelegram.Schema.Messages.RequestUploadMedia
        >(handlerHelper, dataConverter)
{
}