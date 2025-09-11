namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Search for messages.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FROM_PEER_INVALID The specified from_id is invalid.
/// 400 INPUT_FILTER_INVALID The specified filter is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PEER_ID_NOT_SUPPORTED The provided peer ID is not supported.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.search" />
///</summary>
internal sealed class SearchHandler(
    IMessageAppService messageAppService,
    IPeerHelper peerHelper,
    IAccessHashHelper accessHashHelper,
    IGetHistoryConverterService getHistoryConverterService
    )
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearch, IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearch obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        await accessHashHelper.CheckAccessHashAsync(input, obj.FromId);
        var userId = input.UserId;
        var peer = peerHelper.GetPeer(obj.Peer, userId);

        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;

        var getMessageOutput = await messageAppService.SearchAsync(new SearchInput
        {
            OwnerPeerId = ownerPeerId,
            SelfUserId = userId,
            Limit = obj.Limit,
            Q = obj.Q,
            OffsetId = obj.OffsetId,
            AddOffset = obj.AddOffset,
            Peer = peer,
            MaxDate = obj.MaxDate,
            MaxId = obj.MaxId,
            MinDate = obj.MinDate,
            MinId = obj.MinId,
            MessageType = GetMessageType(obj.Filter)
        });
        return getHistoryConverterService.ToMessages(input, getMessageOutput, input.Layer);
    }

    private static MessageType GetMessageType(IMessagesFilter? filter)
    {
        if (filter != null)
        {
            var messageType = MessageType.Unknown;
            switch (filter)
            {
                case TInputMessagesFilterChatPhotos:
                    messageType = MessageType.Photo;
                    break;

                case TInputMessagesFilterContacts:
                    messageType = MessageType.Contacts;
                    break;

                case TInputMessagesFilterDocument:
                    messageType = MessageType.Document;
                    break;

                case TInputMessagesFilterEmpty:
                    break;

                case TInputMessagesFilterGeo:
                    messageType = MessageType.Geo;
                    break;

                case TInputMessagesFilterGif:
                    messageType = MessageType.Photo;
                    break;

                case TInputMessagesFilterMusic:
                    messageType = MessageType.Music;
                    break;

                case TInputMessagesFilterMyMentions:
                    break;

                case TInputMessagesFilterPhoneCalls:
                    messageType = MessageType.PhoneCall;
                    break;

                case TInputMessagesFilterPhotos:
                    messageType = MessageType.Photo;
                    break;

                case TInputMessagesFilterPhotoVideo:
                    messageType = MessageType.Video;
                    break;

                case TInputMessagesFilterPinned:
                    messageType = MessageType.Pinned;
                    break;

                case TInputMessagesFilterRoundVideo:
                    messageType = MessageType.Video;

                    break;

                case TInputMessagesFilterRoundVoice:
                    messageType = MessageType.Voice;

                    break;

                case TInputMessagesFilterUrl:
                    messageType = MessageType.Url;
                    break;

                case TInputMessagesFilterVideo:
                    messageType = MessageType.Video;

                    break;

                case TInputMessagesFilterVoice:
                    messageType = MessageType.Voice;

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(filter));
            }

            return messageType;
        }

        return MessageType.Unknown;
    }
}
