using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetTypingHandler : RpcResultObjectHandler<RequestSetTyping, IBool>,
    ISetTypingHandler, IProcessedHandler
{
    private readonly IObjectMessageSender _messageSender;
    private readonly IPeerHelper _peerHelper;

    public SetTypingHandler(IPeerHelper peerHelper,
        IObjectMessageSender messageSender)
    {
        _peerHelper = peerHelper;
        _messageSender = messageSender;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetTyping obj)
    {
        var userId = input.UserId;
        var peer = _peerHelper.GetPeer(obj.Peer, userId);
        IUpdate? update = null;
        switch (peer.PeerType)
        {
            case PeerType.Unknown:
                break;
            case PeerType.Self:
                break;
            case PeerType.User:
                update = new TUpdateUserTyping { Action = obj.Action, UserId = userId };

                break;
            case PeerType.Chat:
                update = new TUpdateChatUserTyping {
                    Action = obj.Action, ChatId = peer.PeerId, FromId = new TPeerUser { UserId = userId }
                    //UserId = session.UserId
                };
                break;
            case PeerType.Channel:
                update = new TUpdateChannelUserTyping {
                    Action = obj.Action,
                    ChannelId = peer.PeerId,
                    TopMsgId = obj.TopMsgId,
                    //UserId = session.UserId,
                    FromId = new TPeerUser { UserId = userId }
                };

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (update == null)
        {
            return new TBoolTrue();
        }

        var updateShort = new TUpdateShort { Date = CurrentDate, Update = update };

        // push message to peer
        // await LocalEventBus.PublishAsync(new PushMessageToPeerDomainEvent(peer,updateShort,input.SeqNumber+1));
        //await SendMessageToPeerAsync(peer, updateShort, input.SeqNumber + 1, input.AuthKeyId);
        //await _messageSender.SendMessageToPeerAsync(peer, updateShort, input.SeqNumber + 1, input.AuthKeyId);
        await _messageSender.PushMessageToPeerAsync(peer, updateShort);
        return new TBoolTrue();
    }
}
