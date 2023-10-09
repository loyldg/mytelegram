// ReSharper disable All

namespace MyTelegram.Handlers.Updates;

///<summary>
/// Returns the difference between the current state of updates of a certain channel and transmitted.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PERSISTENT_TIMESTAMP_EMPTY Persistent timestamp empty.
/// 400 PERSISTENT_TIMESTAMP_INVALID Persistent timestamp invalid.
/// 500 PERSISTENT_TIMESTAMP_OUTDATED Channel internal replication issues, try again later (treat this like an RPC_CALL_FAIL).
/// 400 PINNED_DIALOGS_TOO_MUCH Too many pinned dialogs.
/// 400 RANGES_INVALID Invalid range provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/updates.getChannelDifference" />
///</summary>
internal sealed class GetChannelDifferenceHandler : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetChannelDifference, MyTelegram.Schema.Updates.IChannelDifference>,
    Updates.IGetChannelDifferenceHandler
{
    protected override Task<MyTelegram.Schema.Updates.IChannelDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.RequestGetChannelDifference obj)
    {
        throw new NotImplementedException();
    }
}
