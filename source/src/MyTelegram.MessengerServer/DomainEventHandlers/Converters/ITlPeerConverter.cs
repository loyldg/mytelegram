using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlPeerConverter
{
    IJoinAsPeers ToJoinAsPeers(IUserReadModel userReadModel,
        IChannelReadModel? channelReadModel,
        IChatReadModel? chatReadModel);
}
