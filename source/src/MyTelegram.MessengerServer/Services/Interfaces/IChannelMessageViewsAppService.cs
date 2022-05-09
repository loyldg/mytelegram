namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IChannelMessageViewsAppService
{
    Task<IList<IMessageViews>> GetMessageViewsAsync(long selfUserId,
        long authKeyId,
        long channelId,
        List<int> messageIdList);
}