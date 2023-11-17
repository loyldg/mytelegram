// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns found messages
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
internal sealed class SearchHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearch, MyTelegram.Schema.Messages.IMessages>,
    Messages.ISearchHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;
    public SearchHandler(IMessageAppService messageAppService,
        IPeerHelper peerHelper,
        //IRpcResultProcessor rpcResultProcessor,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
        //_rpcResultProcessor = rpcResultProcessor;
        _accessHashHelper = accessHashHelper;
        _layeredService = layeredService;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearch obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        await _accessHashHelper.CheckAccessHashAsync(obj.FromId);
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);

        var ownerUid = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;

        var r = await _messageAppService.SearchAsync(new SearchInput
        {
            OwnerPeerId = ownerUid,
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

        //return _rpcResultProcessor.ToMessages(r, input.Layer);
        return _layeredService.GetConverter(input.Layer).ToMessages(r, input.Layer);
    }

    private static MessageType GetMessageType(IMessagesFilter? filter)
    {
        //Expression<Func<MessageBox, bool>> predicate = null;
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
            //if (MessageType != MessageType.Unknown)
            //{
            //    predicate = x => x.MessageType == MessageType;
            //}
        }

        return MessageType.Unknown;
        //return predicate;
    }
}
