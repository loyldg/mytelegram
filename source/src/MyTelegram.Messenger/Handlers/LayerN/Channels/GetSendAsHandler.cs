namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Channels;

///<summary>
/// Obtains a list of peers that can be used to send messages in a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getSendAs" />
///</summary>
internal sealed class GetSendAsHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Channels.LayerN.RequestGetSendAs,
        MyTelegram.Schema.Channels.RequestGetSendAs> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Channels.LayerN.RequestGetSendAs,
            MyTelegram.Schema.Channels.RequestGetSendAs
        >(handlerHelper, dataConverter), IDistinctObjectHandler
{
}