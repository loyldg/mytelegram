namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Channels;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Channels.LayerN.RequestGetMessages,
        MyTelegram.Schema.Channels.RequestGetMessages> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Channels.LayerN.RequestGetMessages,
            MyTelegram.Schema.Channels.RequestGetMessages
        >(handlerHelper, dataConverter)
{
}