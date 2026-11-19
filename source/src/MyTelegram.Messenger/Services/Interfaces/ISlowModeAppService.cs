namespace MyTelegram.Messenger.Services.Interfaces;

public interface ISlowModeAppService
{
    /// <summary>
    /// Throws SLOWMODE_WAIT_X / SLOWMODE_MULTI_MSGS_DISABLED if the user can not send messages to the channel now.
    /// Returns true if slow mode applies to the sender (the send date should be saved after sending)
    /// </summary>
    Task<bool> CheckSlowModeAsync(long channelId, long senderUserId, int messageCount, bool isGroupedMessage);

    Task SaveLastSendDateAsync(long channelId, long senderUserId, int date);

    /// <summary>
    /// Returns the date when the user can send the next message, or null if the user can send messages now
    /// </summary>
    Task<int?> GetNextSendDateAsync(long channelId, long userId);
}
