namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Updates;

///<summary>
/// Get new <a href="https://corefork.telegram.org/api/updates">updates</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 DATE_EMPTY Date empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PERSISTENT_TIMESTAMP_EMPTY Persistent timestamp empty.
/// 400 PERSISTENT_TIMESTAMP_INVALID Persistent timestamp invalid.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/updates.getDifference" />
///</summary>
internal sealed class GetDifferenceHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Updates.LayerN.RequestGetDifference,
        MyTelegram.Schema.Updates.RequestGetDifference> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Updates.LayerN.RequestGetDifference,
            MyTelegram.Schema.Updates.RequestGetDifference
        >(handlerHelper, dataConverter)
{
}