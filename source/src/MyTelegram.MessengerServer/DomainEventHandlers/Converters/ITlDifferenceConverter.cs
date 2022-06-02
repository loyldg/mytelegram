using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlDifferenceConverter
{
    IChannelDifference ToChannelDifference(GetMessageOutput output,
        bool isChannelMember,
        IList<IUpdate> updatesList,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false);

    IDifference ToDifference(GetMessageOutput output,
        IPtsReadModel? pts,
        int cachedPts,
        int limit,
        IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates);
}