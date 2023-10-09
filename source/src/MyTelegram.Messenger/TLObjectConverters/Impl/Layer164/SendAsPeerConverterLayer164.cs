namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer164;

public class SendAsPeerConverterLayer164 : ISendAsPeerConverterLayer164
{
    public int Layer => Layers.Layer164;

    public ISendAsPeers ToSendAsPeers(long userId,
        long channelId,
        long channelCreatorId,
        IChat? channel,
        IUser? user)
    {
        if (channelCreatorId == userId)
        {
            return new TSendAsPeers
            {
                Chats = channel == null ? new TVector<IChat>() : new TVector<IChat>(channel),
                Users = new TVector<IUser>(),
                Peers = new TVector<ISendAsPeer>(new TSendAsPeer
                {
                    Peer = new TPeerChannel { ChannelId = channelId }
                })
            };
        }

        return new TSendAsPeers
        {
            Chats = new TVector<IChat>(),
            Users = user == null ? new TVector<IUser>() : new TVector<IUser>(user),
            Peers = new TVector<ISendAsPeer>(new TSendAsPeer
            {
                Peer = new TPeerUser { UserId = userId }
            })
        };
    }

    public int RequestLayer { get; set; }
}