namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Send an <a href="https://corefork.telegram.org/api/files#albums-grouped-media">album or grouped media</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_FORWARDS_RESTRICTED You can't forward messages from a protected chat.
/// 403 CHAT_SEND_MEDIA_FORBIDDEN You can't send media in this chat.
/// 403 CHAT_SEND_PHOTOS_FORBIDDEN You can't send photos in this chat.
/// 403 CHAT_SEND_VIDEOS_FORBIDDEN You can't send videos in this chat.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here»</a> for info on how to properly compute the entity offset/length.
/// 400 FILE_REFERENCE_%d_EXPIRED The file reference of the media file at index %d in the passed media array expired, it <a href="https://corefork.telegram.org/api/file_reference">must be refreshed</a>.
/// 400 FILE_REFERENCE_%d_INVALID The file reference of the media file at index %d in the passed media array is invalid.
/// 400 MEDIA_CAPTION_TOO_LONG The caption is too long.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MEDIA_INVALID Media invalid.
/// 400 MULTI_MEDIA_TOO_LONG Too many media files for album.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 QUICK_REPLIES_TOO_MUCH A maximum of <a href="https://corefork.telegram.org/api/config#quick-replies-limit">appConfig.<code>quick_replies_limit</code></a> shortcuts may be created, the limit was reached.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 RANDOM_ID_EMPTY Random ID empty.
/// 400 REPLY_MESSAGES_TOO_MUCH Each shortcut can contain a maximum of <a href="https://corefork.telegram.org/api/config#quick-reply-messages-limit">appConfig.<code>quick_reply_messages_limit</code></a> messages, the limit was reached.
/// 400 SCHEDULE_DATE_TOO_LATE You can't schedule a message this far in the future.
/// 400 SCHEDULE_TOO_MUCH There are too many scheduled messages.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// 420 SLOWMODE_WAIT_%d Slowmode is enabled in this chat: wait %d seconds before sending another message to this chat.
/// 400 TOPIC_CLOSED This topic was closed, you can't send messages to it anymore.
/// 400 TOPIC_DELETED The specified topic was deleted.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/messages.sendMultiMedia" />
///</summary>
internal sealed class SendMultiMediaHandler(
    IMessageAppService messageAppService,
    IMediaHelper mediaHelper,
    //IRequestCacheAppService requestCacheAppService,
    IPeerHelper peerHelper,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendMultiMedia, MyTelegram.Schema.IUpdates>
{
    //private readonly IRequestCacheAppService _requestCacheAppService;
    //_requestCacheAppService = requestCacheAppService;

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestSendMultiMedia obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.SendAs);

        var groupId = randomHelper.NextInt64();
        var groupItemCount = obj.MultiMedia.Count;
        var requestInfo = input.ToRequestInfo();
        int? replyToMsgId = null;
        int? topMsgId = null;
        if (obj.ReplyTo is TInputReplyToMessage replyToMessage)
        {
            replyToMsgId = replyToMessage.ReplyToMsgId;
            topMsgId = replyToMessage.TopMsgId;
        }

        var sendAs = peerHelper.GetPeer(obj.SendAs, input.UserId);
        var inputs = new List<SendMessageInput>();
        foreach (var inputSingleMedia in obj.MultiMedia)
        {
            var media = await mediaHelper.SaveMediaAsync(inputSingleMedia.Media);
            var sendMessageInput = new SendMessageInput(requestInfo,
                input.UserId,
                peerHelper.GetPeer(obj.Peer, input.UserId),
                inputSingleMedia.Message,
                inputSingleMedia.RandomId,
                clearDraft: obj.ClearDraft,
                entities: inputSingleMedia.Entities,
                media: media,
                //replyToMsgId: replyToMsgId,
                inputReplyTo: obj.ReplyTo,
                sendMessageType: SendMessageType.Media,
                messageType: mediaHelper.GeMessageType(media),
                groupId: groupId,
                groupItemCount: groupItemCount,
                topMsgId: topMsgId,
                sendAs: sendAs,
                effect: obj.Effect,
                inputQuickReplyShortcut: obj.QuickReplyShortcut,
                isSendGroupedMessage: true,
                silent: obj.Silent,
                scheduleDate: obj.ScheduleDate,
                invertMedia: obj.InvertMedia
            );
            inputs.Add(sendMessageInput);
            //await _messageAppService.SendMessageAsync(sendMessageInput);
            //_requestCacheAppService.AddRequest(input.ReqMsgId, input.AuthKeyId, input.RequestSessionId, input.SeqNumber);
        }

        await messageAppService.SendMessageAsync(inputs);

        return null!;
    }
}
