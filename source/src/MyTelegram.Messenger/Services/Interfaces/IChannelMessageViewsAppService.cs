using IMessageViews = MyTelegram.Schema.IMessageViews;

namespace MyTelegram.Messenger.Services.Interfaces;

public interface IChannelMessageViewsAppService
{
    Task IncrementViewsIfNotIncrementedAsync(long selfUserId,
        long channelId,
        int messageId);
    Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long channelId,
        List<int> messageIdList);
}