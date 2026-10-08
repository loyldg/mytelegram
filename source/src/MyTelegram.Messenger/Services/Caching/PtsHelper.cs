using GetPtsByPermAuthKeyIdQuery = MyTelegram.Queries.GetPtsByPermAuthKeyIdQuery;

namespace MyTelegram.Messenger.Services.Caching;

public class PtsHelper(IQueryProcessor queryProcessor) : IPtsHelper, ISingletonDependency
{
    private readonly ConcurrentDictionary<long, PtsCacheItem> _ownerToPtsDict = new();
    private readonly ConcurrentDictionary<long, PtsCacheItem> _permAuthKeyIdToPtsDict = new();

    public int GetCachedPts(long ownerId)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            return cacheItem.Pts;
        }

        return MyTelegramConsts.PtsIdBase;
    }

    public async Task<PtsCacheItem> GetPtsForUserAsync(long userId)
    {
        if (!_ownerToPtsDict.TryGetValue(userId, out var ptsCacheItem))
        {
            var ptsReadModel = await queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(userId));
            if (ptsReadModel != null)
            {
                ptsCacheItem = new PtsCacheItem(ptsReadModel.PeerId, ptsReadModel.Pts, ptsReadModel.Qts,
                    ptsReadModel.Date);
            }
            else
            {
                ptsCacheItem = new PtsCacheItem(userId, date: DateTime.UtcNow.ToTimestamp());
            }

            _ownerToPtsDict.TryAdd(userId, ptsCacheItem);
        }

        return ptsCacheItem;
    }

    public async Task<PtsCacheItem> GetPtsForAuthKeyIdAsync(long userId, long permAuthKeyId)
    {
        if (!_permAuthKeyIdToPtsDict.TryGetValue(permAuthKeyId, out var ptsCacheItem))
        {
            var ptsForAuthKeyIdReadModel =
                await queryProcessor.ProcessAsync(new GetPtsByPermAuthKeyIdQuery(userId, permAuthKeyId));
            if (ptsForAuthKeyIdReadModel != null)
            {
                ptsCacheItem = new PtsCacheItem(ptsForAuthKeyIdReadModel.PeerId, ptsForAuthKeyIdReadModel.Pts,
                    ptsForAuthKeyIdReadModel.Qts);
            }
            else
            {
                ptsCacheItem = new PtsCacheItem(userId);
            }
            _permAuthKeyIdToPtsDict.TryAdd(permAuthKeyId, ptsCacheItem);
        }

        return ptsCacheItem;
    }

    public async Task<bool> UpdatePtsForAuthKeyIdAsync(long userId, long permAuthKeyId, int pts, bool forceUpdate)
    {
        if (!_permAuthKeyIdToPtsDict.TryGetValue(permAuthKeyId, out var ptsCacheItem))
        {
            var ptsForAuthKeyIdReadModel =
                await queryProcessor.ProcessAsync(new GetPtsByPermAuthKeyIdQuery(userId, permAuthKeyId));
            if (ptsForAuthKeyIdReadModel != null)
            {
                ptsCacheItem = new PtsCacheItem(ptsForAuthKeyIdReadModel.PeerId, ptsForAuthKeyIdReadModel.Pts,
                    ptsForAuthKeyIdReadModel.Qts);
            }
            else
            {
                ptsCacheItem = new PtsCacheItem(userId);
            }

            _permAuthKeyIdToPtsDict.TryAdd(permAuthKeyId, ptsCacheItem);
        }

        if (ptsCacheItem.Pts + 1 == pts || forceUpdate)
        {
            var ptsCount = pts - ptsCacheItem.Pts;
            if (ptsCount > 0)
            {
                ptsCacheItem.AddPts(ptsCount);
                return true;
            }
        }
        else
        {
            // Console.WriteLine($"Current pts is:{ptsCacheItem.Pts} newPts:{pts}");
        }

        return false;
    }

    public Task<int> IncrementPtsAsync(long ownerId, int currentPts, int ptsCount = 1, long permAuthKeyId = 0, int newUnreadCount = 0)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            if (ptsCount == 1)
            {
                cacheItem.IncrementPts();
            }
            else
            {
                cacheItem.AddPts(ptsCount);
            }

            if (cacheItem.Pts < currentPts)
            {
                cacheItem.AddPts(currentPts - cacheItem.Pts);
            }
        }
        else
        {
            cacheItem = new PtsCacheItem(ownerId, currentPts);
            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
        }

        if (newUnreadCount != 0)
        {
            cacheItem.AddUnreadCount(newUnreadCount);
        }

        return Task.FromResult(cacheItem.Pts);
    }

    public Task<int> IncrementQtsAsync(long ownerId, int currentQts, int qtsCount = 1, long permAuthKeyId = 0)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            if (qtsCount == 1)
            {
                cacheItem.IncrementQts();
            }
            else
            {
                cacheItem.AddQts(qtsCount);
            }

            if (cacheItem.Qts < currentQts)
            {
                cacheItem.AddQts(currentQts - cacheItem.Qts);
            }
        }
        else
        {
            cacheItem = new PtsCacheItem(ownerId, currentQts);
            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
        }

        return Task.FromResult(cacheItem.Qts);
    }

    public Task SyncCachedPtsToReadModelAsync(long ownerId)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out _))
        {
            //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, cacheItem.Pts);
            //await _commandBus.PublishAsync(command, CancellationToken.None);
        }

        return Task.CompletedTask;
    }
}