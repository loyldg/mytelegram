using IMessageViews = MyTelegram.Schema.IMessageViews;

namespace MyTelegram.Messenger.Services.Interfaces;

public interface IChannelMessageViewsAppService
{
    Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
        long authKeyId,
        long channelId,
        int messageId);
    void IncrementViews(long selfUserId, long channelId, int messageId);

    Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long authKeyId,
        long channelId,
        List<int> messageIdList);

    void SaveViewsFilters();
    void LoadViewsFilters();
    void RotateDaily();
}