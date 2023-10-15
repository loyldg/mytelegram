using System.Threading.Channels;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace MyTelegram.Messenger.QueryServer.Services;

public interface ICommandExecutor<out TAggregate, in TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    Task ProcessCommandAsync();

    void Enqueue(ICommand<TAggregate, TIdentity, TExecutionResult> command);
}

public class CommandExecutor<TAggregate, TIdentity, TExecutionResult> : ICommandExecutor<TAggregate, TIdentity, TExecutionResult>
    where TAggregate : IAggregateRoot<TIdentity>
    where TIdentity : IIdentity
    where TExecutionResult : IExecutionResult
{
    private readonly Channel<ICommand<TAggregate, TIdentity, TExecutionResult>> _commands = Channel.CreateUnbounded<ICommand<TAggregate, TIdentity, TExecutionResult>>();
    private readonly ICommandBus _commandBus;

    public CommandExecutor(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    public Task ProcessCommandAsync()
    {
        Task.Run(async () =>
        {
            while (await _commands.Reader.WaitToReadAsync())
            {
                while (_commands.Reader.TryRead(out var command))
                {
                    await _commandBus.PublishAsync(command, default);
                }
            }
        });

        return Task.CompletedTask;
    }

    public void Enqueue(
        ICommand<TAggregate, TIdentity, TExecutionResult> command)
    {
        _commands.Writer.TryWrite(command);
        //Console.WriteLine($"Total count:{_commands.Reader.Count}");
    }
}

//public class CachedReadModelLoader
//{
//    private readonly IQueryProcessor _queryProcessor;

//    public CachedReadModelLoader(IQueryProcessor queryProcessor)
//    {
//        _queryProcessor = queryProcessor;
//    }

//    public Task LoadAllCachedReadModelAsync<TReadModel>()
//    {

//    }


//}

public class MyTelegramQueryServerReadModelUpdateManager : IReadModelUpdateManager
{
    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
    {
        var type = typeof(TReadModel);
        switch (type.Name)
        {
            case nameof(MyTelegram.ReadModel.MongoDB.UserReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChannelReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChatReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChannelFullReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PhotoReadModel):

                return Task.FromResult(UpdateStrategy.UpdateCache);

            case nameof(MyTelegram.ReadModel.MongoDB.RpcResultReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.UpdatesReadModel):

                return Task.FromResult(UpdateStrategy.UpdateDatabase);


            case nameof(MyTelegram.ReadModel.MongoDB.PtsReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsForAuthKeyIdReadModel):
                return Task.FromResult(UpdateStrategy.All);
        }

        return Task.FromResult(UpdateStrategy.None);
    }
}

public class QueryServerReadModelUpdateStrategy : IReadModelUpdateStrategy
{
    public Task<bool> ShouldUpdateReadModelAsync<TReadModel>()
    {
        var type = typeof(TReadModel);
        switch (type.Name)
        {
            case nameof(MyTelegram.ReadModel.MongoDB.RpcResultReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.UpdatesReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsForAuthKeyIdReadModel):
                return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}

//public class QueryServerPtsHelper : IPtsHelper
//{
//    private readonly ConcurrentDictionary<long, PtsCacheItem> _ownerToPtsDict = new();
//    private readonly int _syncToReadModelIntervalCount = 1000;
//    private readonly ICommandBus _commandBus;
//    private readonly ILogger<QueryServerPtsHelper> _logger;
//    public QueryServerPtsHelper(ICommandBus commandBus, ILogger<QueryServerPtsHelper> logger)
//    {
//        _commandBus = commandBus;
//        _logger = logger;
//    }

//    public int GetCachedPts(long ownerId)
//    {
//        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
//        {
//            return cacheItem.Pts;
//        }

//        return 0;
//    }

//    //public async Task AddPtsAsync(long ownerId,int currentPts){}

//    public Task IncrementPtsAsync(long ownerId, int currentPts, int ptsCount = 1, long permAuthKeyId = 0)
//    {
//        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
//        {
//            if (ptsCount == 1)
//            {
//                cacheItem.IncrementPts();
//            }
//            else
//            {
//                cacheItem.AddPts(ptsCount);
//            }

//            if (cacheItem.Pts < currentPts)
//            {
//                cacheItem.AddPts(currentPts - cacheItem.Pts);
//            }

//            //if (cacheItem.Pts + 1 < currentPts)
//            //{
//            //    cacheItem.AddPts(currentPts - cacheItem.Pts);
//            //}
//            //else
//            //{
//            //    cacheItem.IncrementPts();
//            //}
//        }
//        else
//        {
//            cacheItem = new PtsCacheItem(ownerId, currentPts);
//            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
//        }

//        if (cacheItem.Pts % _syncToReadModelIntervalCount == 0)
//        {
//            //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, currentPts);
//            //await _commandBus.PublishAsync(command, CancellationToken.None);
//        }

//        //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, permAuthKeyId, cacheItem.Pts, 0);
//        //_commandBus.PublishAsync(command, default);

//        _logger.LogInformation("##### [query server] pts updated {UserId} {Pts}", ownerId, cacheItem.Pts);

//        return Task.CompletedTask;
//    }

//    public Task<int> IncrementQtsAsync(long ownerId, int currentQts, int qtsCount = 1, long permAuthKeyId = 0)
//    {
//        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
//        {
//            if (qtsCount == 1)
//            {
//                cacheItem.IncrementQts();
//            }
//            else
//            {
//                cacheItem.AddQts(qtsCount);
//            }

//            if (cacheItem.Qts < currentQts)
//            {
//                cacheItem.AddQts(currentQts - cacheItem.Qts);
//            }
//        }
//        else
//        {
//            cacheItem = new PtsCacheItem(ownerId, currentQts);
//            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
//        }

//        if (cacheItem.Qts % _syncToReadModelIntervalCount == 0)
//        {
//            //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, currentPts);
//            //await _commandBus.PublishAsync(command, CancellationToken.None);
//        }

//         _logger.LogInformation("##### Qts updated {UserId} {Qts}", ownerId, cacheItem.Qts);

//        return Task.FromResult(cacheItem.Qts);
//    }


//    public Task SyncCachedPtsToReadModelAsync(long ownerId)
//    {
//        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
//        {
//            //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, cacheItem.Pts);
//            //await _commandBus.PublishAsync(command, CancellationToken.None);
//        }

//        return Task.CompletedTask;
//    }
//}