using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlPeerConverter : ITlPeerConverter
{
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlUserConverter _userConverter;

    public TlPeerConverter(ITlChatConverter chatConverter,
        ITlUserConverter userConverter)
    {
        _chatConverter = chatConverter;
        _userConverter = userConverter;
    }

    public IJoinAsPeers ToJoinAsPeers(IUserReadModel userReadModel,
        IChannelReadModel? channelReadModel,
        IChatReadModel? chatReadModel)
    {
        var peerList = new List<IPeer>();
        IChat? chat = null;
        var peer = new TPeerUser { UserId = userReadModel.UserId };
        peerList.Add(peer);
        if (channelReadModel != null)
        {
            peerList.Add(new TPeerChannel { ChannelId = channelReadModel.ChannelId });
            chat = _chatConverter.ToChannel(channelReadModel, null, userReadModel.UserId, false);
        }

        if (chatReadModel != null)
        {
            peerList.Add(new TPeerChat { ChatId = chatReadModel.ChatId });
            chat = _chatConverter.ToChat(chatReadModel, userReadModel.UserId);
        }

        return new TJoinAsPeers
        {
            Chats = chat == null ? new TVector<IChat>() : new TVector<IChat>(chat),
            Peers = new TVector<IPeer>(peerList),
            Users = new TVector<IUser>(_userConverter.ToUser(userReadModel, userReadModel.UserId))
        };
    }
}
