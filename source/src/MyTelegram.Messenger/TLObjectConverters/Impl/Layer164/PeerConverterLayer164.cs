using MyTelegram.Schema.Phone;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer164;

public class PeerConverterLayer164 : IPeerConverterLayer164
{
    public virtual int Layer => Layers.Layer164;

    public int RequestLayer { get; set; }

    public IJoinAsPeers ToJoinAsPeers(
        //IUserReadModel userReadModel,
        //IChannelReadModel? channelReadModel,
        //IChatReadModel? chatReadModel
        IUser user,
        IChat? chat,
        IChat? channel
    )
    {
        var peerList = new List<IPeer>();
        //IChat? chat = null;
        var peer = new TPeerUser { UserId = user.Id };
        peerList.Add(peer);
        if (channel != null)
        {
            peerList.Add(new TPeerChannel { ChannelId = channel.Id });
        }

        if (chat != null)
        {
            peerList.Add(new TPeerChat { ChatId = chat.Id });
        }

        //if (channelReadModel != null)
        //{
        //    peerList.Add(new TPeerChannel { ChannelId = channelReadModel.ChannelId });
        //    chat = GetChatConverter().ToChannel(channelReadModel, null, userReadModel.UserId, false);
        //}

        //if (chatReadModel != null)
        //{
        //    peerList.Add(new TPeerChat { ChatId = chatReadModel.ChatId });
        //    chat = GetChatConverter().ToChat(chatReadModel, userReadModel.UserId);
        //}

        return new TJoinAsPeers
        {
            Chats = chat == null ? new TVector<IChat>() : new TVector<IChat>(chat),
            Peers = new TVector<IPeer>(peerList),
            Users = new TVector<IUser>(user)
        };
    }
}