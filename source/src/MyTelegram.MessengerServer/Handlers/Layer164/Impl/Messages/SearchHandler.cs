using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using RequestSearch = MyTelegram.Schema.Messages.RequestSearch;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SearchHandler : RpcResultObjectHandler<RequestSearch, IMessages>,
    ISearchHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IPeerHelper _peerHelper;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public SearchHandler(IMessageAppService messageAppService,
        IPeerHelper peerHelper,
        IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _peerHelper = peerHelper;
        _rpcResultProcessor = rpcResultProcessor;
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
            //if (messageType != MessageType.Unknown)
            //{
            //    predicate = x => x.MessageType == messageType;
            //}
        }

        return MessageType.Unknown;
        //return predicate;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestSearch obj)
    {
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);

        var ownerPeerId = peer.PeerType == PeerType.Channel ? peer.PeerId : userId;

        var r = await _messageAppService.SearchAsync(
            new SearchInput(GetMessageType(obj.Filter),
                ownerPeerId,
                peer,
                obj.Q,
                userId)
            {
                Limit = obj.Limit,
                OffsetId = obj.OffsetId,
                AddOffset = obj.AddOffset,
                MaxDate = obj.MaxDate,
                MaxId = obj.MaxId,
                MinDate = obj.MinDate,
                MinId = obj.MinId
            });

        return _rpcResultProcessor.ToMessages(r);
    }
}