// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Send an <a href="https://corefork.telegram.org/api/files#albums-grouped-media">album or grouped media</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_FORWARDS_RESTRICTED You can't forward messages from a protected chat.
/// 403 CHAT_SEND_MEDIA_FORBIDDEN You can't send media in this chat.
/// 403 CHAT_SEND_PHOTOS_FORBIDDEN You can't send photos in this chat.
/// 403 CHAT_SEND_VIDEOS_FORBIDDEN You can't send videos in this chat.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 MEDIA_CAPTION_TOO_LONG The caption is too long.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MEDIA_INVALID Media invalid.
/// 400 MULTI_MEDIA_TOO_LONG Too many media files for album.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 RANDOM_ID_EMPTY Random ID empty.
/// 400 SCHEDULE_DATE_TOO_LATE You can't schedule a message this far in the future.
/// 400 SCHEDULE_TOO_MUCH There are too many scheduled messages.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// 420 SLOWMODE_WAIT_%d Slowmode is enabled in this chat: wait %d seconds before sending another message to this chat.
/// 400 TOPIC_DELETED The specified topic was deleted.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/messages.sendMultiMedia" />
///</summary>
internal sealed class SendMultiMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendMultiMedia, MyTelegram.Schema.IUpdates>,
    Messages.ISendMultiMediaHandler
{
    private readonly IMediaHelper _mediaHelper;

    private readonly IMessageAppService _messageAppService;

    //private readonly IRequestCacheAppService _requestCacheAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;

    public SendMultiMediaHandler(IMessageAppService messageAppService,
    IMediaHelper mediaHelper,
    //IRequestCacheAppService requestCacheAppService,
    IPeerHelper peerHelper,
    IRandomHelper randomHelper,
    IAccessHashHelper accessHashHelper)
    {
        _messageAppService = messageAppService;
        _mediaHelper = mediaHelper;
        //_requestCacheAppService = requestCacheAppService;
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendMultiMedia obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        await _accessHashHelper.CheckAccessHashAsync(obj.SendAs);

        var groupId = _randomHelper.NextLong();
        var groupItemCount = obj.MultiMedia.Count;
        var requestInfo = input.ToRequestInfo();
        int? replyToMsgId = null;
        int? topMsgId = null;
        if (obj.ReplyTo is TInputReplyToMessage replyToMessage)
        {
            replyToMsgId = replyToMessage.ReplyToMsgId;
            topMsgId = replyToMessage.TopMsgId;
        }

        foreach (var inputSingleMedia in obj.MultiMedia)
        {
            var media = await _mediaHelper.SaveMediaAsync(inputSingleMedia.Media);
            var sendMessageInput = new SendMessageInput(requestInfo,
                input.UserId,
                _peerHelper.GetPeer(obj.Peer, input.UserId),
                inputSingleMedia.Message,
                inputSingleMedia.RandomId,
                clearDraft: obj.ClearDraft,
                entities: inputSingleMedia.Entities,
                media: media.ToBytes(),
                replyToMsgId: replyToMsgId,
                sendMessageType: SendMessageType.Media,
                messageType: _mediaHelper.GeMessageType(media),
                groupId: groupId,
                groupItemCount: groupItemCount,
                topMsgId: topMsgId
            );
            await _messageAppService.SendMessageAsync(sendMessageInput);
            //_requestCacheAppService.AddRequest(input.ReqMsgId, input.AuthKeyId, input.RequestSessionId, input.SeqNumber);
        }

        return null!;
    }
}
