using MyTelegram.Messenger.Services;
using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IDifferenceConverter : ILayeredConverter
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
        IList<IChat> chatListFromUpdates,
        IReadOnlyCollection<IEncryptedMessageReadModel>? encryptedMessageReadModels);

    //IChannelDifference ToChannelDifference(
    //    IList<IMessage> messages,
    //    IEnumerable<IChat> chatsOrChannels,
    //    IEnumerable<IUser> users,
    //    IList<IUpdate> updates,
    //    int pts,
    //    bool final);

    //IDifference ToDifference(IEnumerable<IMessage> messages,
    //    IEnumerable<IChat> chatsOrChannels,
    //    IEnumerable<IUser> users,
    //    IList<IUpdate> updates,
    //    IPtsReadModel? pts,
    //    int cachedPts,
    //    int limit);
}