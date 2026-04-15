namespace MyTelegram.Messenger.Handlers.LayerN.Messages;

internal sealed class ForwardMessagesHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Messages.LayerN.RequestForwardMessages,
        MyTelegram.Schema.Messages.RequestForwardMessages> dataConverter)
    : ForwardRequestToNewHandler<
        MyTelegram.Schema.Messages.LayerN.RequestForwardMessages,
        MyTelegram.Schema.Messages.RequestForwardMessages
    >(handlerHelper, dataConverter)
{
}