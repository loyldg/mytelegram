namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Messages;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Messages.LayerN.RequestGetMessages,
        MyTelegram.Schema.Messages.RequestGetMessages> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Messages.LayerN.RequestGetMessages,
            MyTelegram.Schema.Messages.RequestGetMessages
        >(handlerHelper, dataConverter)
{
}