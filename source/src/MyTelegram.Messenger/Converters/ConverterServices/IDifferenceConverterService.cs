using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IDifferenceConverterService
{
    Task<IChannelDifference> ToChannelDifferenceAsync(
        IRequestWithAccessHashKeyId request,
        GetMessageOutput output,
        bool isChannelMember,
        IList<IUpdate> updatesList,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false,
        int timeoutSeconds = 30,
        int layer = 0
    );

    Task<IDifference> ToDifferenceAsync(
        IRequestWithAccessHashKeyId request,
        GetMessageOutput output,
        IPtsReadModel? pts,
        int cachedPts,
        int limit,
        IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates,
        IReadOnlyCollection<IEncryptedMessageReadModel>? encryptedMessageReadModels,
        int layer = 0);
}