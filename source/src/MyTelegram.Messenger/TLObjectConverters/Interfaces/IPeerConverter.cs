using MyTelegram.Schema.Phone;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPeerConverter : ILayeredConverter
{
    IJoinAsPeers ToJoinAsPeers(
        //IUserReadModel userReadModel,
        IUser user,
        IChat? chat,
        IChat? channel
        //IChannelReadModel? channelReadModel,
        //IChatReadModel? chatReadModel
        );
}