namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface ISendAsPeerConverter : ILayeredConverter
{
    ISendAsPeers ToSendAsPeers(long userId, long channelId, long channelCreatorId, IChat? channel, IUser? user);
}
