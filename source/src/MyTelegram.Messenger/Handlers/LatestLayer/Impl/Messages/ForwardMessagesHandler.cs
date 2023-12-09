// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Forwards messages by their IDs.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BROADCAST_PUBLIC_VOTERS_FORBIDDEN You can't forward polls with public voters.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 406 CHAT_FORWARDS_RESTRICTED You can't forward messages from a protected chat.
/// 403 CHAT_GUEST_SEND_FORBIDDEN You join the discussion group before commenting, see <a href="https://corefork.telegram.org/api/discussion#requiring-users-to-join-the-group">here »</a> for more info.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_RESTRICTED You can't send messages in this chat, you were restricted.
/// 403 CHAT_SEND_AUDIOS_FORBIDDEN You can't send audio messages in this chat.
/// 403 CHAT_SEND_GAME_FORBIDDEN You can't send a game to this chat.
/// 403 CHAT_SEND_GIFS_FORBIDDEN You can't send gifs in this chat.
/// 403 CHAT_SEND_MEDIA_FORBIDDEN You can't send media in this chat.
/// 403 CHAT_SEND_PHOTOS_FORBIDDEN You can't send photos in this chat.
/// 403 CHAT_SEND_PLAIN_FORBIDDEN You can't send non-media (text) messages in this chat.
/// 403 CHAT_SEND_POLL_FORBIDDEN You can't send polls in this chat.
/// 403 CHAT_SEND_STICKERS_FORBIDDEN You can't send stickers in this chat.
/// 403 CHAT_SEND_VIDEOS_FORBIDDEN You can't send videos in this chat.
/// 403 CHAT_SEND_VOICES_FORBIDDEN You can't send voice recordings in this chat.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 GROUPED_MEDIA_INVALID Invalid grouped media.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 QUIZ_ANSWER_MISSING You can forward a quiz while hiding the original author only after choosing an option in the quiz.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 RANDOM_ID_INVALID A provided random ID is invalid.
/// 400 SCHEDULE_DATE_TOO_LATE You can't schedule a message this far in the future.
/// 400 SCHEDULE_TOO_MUCH There are too many scheduled messages.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// 420 SLOWMODE_WAIT_%d Slowmode is enabled in this chat: wait %d seconds before sending another message to this chat.
/// 400 SLOWMODE_MULTI_MSGS_DISABLED Slowmode is enabled, you cannot forward multiple messages to this group.
/// 406 TOPIC_CLOSED This topic was closed, you can't send messages to it anymore.
/// 406 TOPIC_DELETED The specified topic was deleted.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// 400 USER_IS_BOT Bots can't send messages to other bots.
/// 400 YOU_BLOCKED_USER You blocked this user.
/// See <a href="https://corefork.telegram.org/method/messages.forwardMessages" />
///</summary>
internal sealed class ForwardMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestForwardMessages, MyTelegram.Schema.IUpdates>,
    Messages.IForwardMessagesHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public ForwardMessagesHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestForwardMessages obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.FromPeer);
        await _accessHashHelper.CheckAccessHashAsync(obj.ToPeer);
        await _accessHashHelper.CheckAccessHashAsync(obj.SendAs);

        var fromPeer = _peerHelper.GetPeer(obj.FromPeer, input.UserId);
        var toPeer = _peerHelper.GetPeer(obj.ToPeer, input.UserId);
        var firstId = obj.Id.First();
        var ownerPeerId = fromPeer.PeerType == PeerType.Channel ? fromPeer.PeerId : input.UserId;
        var command = new StartForwardMessageCommand(MessageId.Create(ownerPeerId, firstId),
            input.ToRequestInfo(),
            fromPeer,
            toPeer,
            obj.Id.ToList(),
            obj.RandomId.ToList(),
            false,
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}
