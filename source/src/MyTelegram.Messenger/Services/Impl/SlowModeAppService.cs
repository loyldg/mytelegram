using System.Buffers.Binary;
using Microsoft.Extensions.Caching.Distributed;

namespace MyTelegram.Messenger.Services.Impl;

/// <summary>
/// Slow mode state is stored in the distributed cache (not in the event store):
/// key = channel + user, value = the date of the last message sent by the user.
/// The last send date is stored instead of the next send date, so changing the slow mode delay takes effect immediately.
/// </summary>
public sealed class SlowModeAppService(
    IDistributedCache distributedCache,
    IChannelAppService channelAppService,
    ILogger<SlowModeAppService> logger) : ISlowModeAppService, ITransientDependency
{
    // The max slow mode delay is 1 hour, keep the cache item a little longer than that
    private static readonly TimeSpan CacheLifetime = TimeSpan.FromSeconds(3600 + 60);

    public async Task<bool> CheckSlowModeAsync(long channelId, long senderUserId, int messageCount, bool isGroupedMessage)
    {
        var slowModeSeconds = await GetSlowModeSecondsAsync(channelId, senderUserId);
        if (slowModeSeconds <= 0)
        {
            return false;
        }

        // Only one message (or one album) can be sent at a time in slow mode
        if (messageCount > 1 && !isGroupedMessage)
        {
            RpcErrors.RpcErrors400.SlowModeMultiMsgsDisabled.ThrowRpcError();
        }

        var lastSendDate = await GetLastSendDateAsync(channelId, senderUserId);
        if (lastSendDate > 0)
        {
            var waitSeconds = lastSendDate.Value + slowModeSeconds - DateTime.UtcNow.ToTimestamp();
            if (waitSeconds > 0)
            {
                RpcErrors.RpcErrors420.SlowModeWaitX.ThrowRpcError(waitSeconds);
            }
        }

        return true;
    }

    public async Task SaveLastSendDateAsync(long channelId, long senderUserId, int date)
    {
        var bytes = new byte[sizeof(int)];
        BinaryPrimitives.WriteInt32LittleEndian(bytes, date);
        try
        {
            await distributedCache.SetAsync(GetCacheKey(channelId, senderUserId), bytes,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheLifetime });
        }
        catch (Exception ex)
        {
            // Slow mode is best effort, never fail the sending because of the cache
            logger.LogWarning(ex, "Save slow mode last send date failed, channelId={ChannelId}, userId={UserId}", channelId, senderUserId);
        }
    }

    public async Task<int?> GetNextSendDateAsync(long channelId, long userId)
    {
        var slowModeSeconds = await GetSlowModeSecondsAsync(channelId, userId);
        if (slowModeSeconds <= 0)
        {
            return null;
        }

        var lastSendDate = await GetLastSendDateAsync(channelId, userId);
        if (lastSendDate is null or <= 0)
        {
            return null;
        }

        var nextSendDate = lastSendDate.Value + slowModeSeconds;

        return nextSendDate > DateTime.UtcNow.ToTimestamp() ? nextSendDate : null;
    }

    /// <summary>
    /// Returns 0 if slow mode is disabled or the user is not affected by slow mode (creator/admins)
    /// </summary>
    private async Task<int> GetSlowModeSecondsAsync(long channelId, long userId)
    {
        var channelReadModel = await channelAppService.GetAsync(channelId, false);
        if (channelReadModel == null! || channelReadModel.Broadcast || !channelReadModel.SlowModeEnabled)
        {
            return 0;
        }

        // Admins are not affected by slow mode
        if (channelReadModel.CreatorId == userId || channelReadModel.AdminList.Any(p => p.UserId == userId))
        {
            return 0;
        }

        var channelFullReadModel = await channelAppService.GetChannelFullAsync(channelId);

        return channelFullReadModel?.SlowModeSeconds ?? 0;
    }

    private async Task<int?> GetLastSendDateAsync(long channelId, long userId)
    {
        try
        {
            var bytes = await distributedCache.GetAsync(GetCacheKey(channelId, userId));
            if (bytes is not { Length: sizeof(int) })
            {
                return null;
            }

            return BinaryPrimitives.ReadInt32LittleEndian(bytes);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Get slow mode last send date failed, channelId={ChannelId}, userId={UserId}", channelId, userId);
            return null;
        }
    }

    private static string GetCacheKey(long channelId, long userId) =>
        MyCacheKey.With("slow_mode", channelId.ToString(), userId.ToString());
}
