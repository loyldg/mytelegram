//using EventFlow.MongoDB.EventStore;
//using Microsoft.Extensions.Hosting;
//using MyTelegram.Common;
//using MyTelegram.Messenger.Services.Impl;

//namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

//public class CheckRabbitMqStatusBackgroundService : BackgroundService
//{
//    private readonly IRabbitMqPersistentConnection _connection;
//    private readonly ILogger<CheckRabbitMqStatusBackgroundService> _logger;
//    private readonly IEventBus _eventBus;
//    private long _count = 100;
//    private readonly IMongoDbEventSequenceStore _eventSequenceStore;
//    private readonly IIdGenerator _idGenerator;
//    private readonly IHiLoHighValueGenerator _hiLoHighValueGenerator;
//    private readonly RedisIdGenerator _redisIdGenerator;

//    public CheckRabbitMqStatusBackgroundService(IRabbitMqPersistentConnection connection,
//        ILogger<CheckRabbitMqStatusBackgroundService> logger, IEventBus eventBus,
//        IMongoDbEventSequenceStore eventSequenceStore, IIdGenerator idGenerator, IHiLoHighValueGenerator hiLoHighValueGenerator, RedisIdGenerator redisIdGenerator)
//    {
//        _connection = connection;
//        _logger = logger;
//        _eventBus = eventBus;
//        _eventSequenceStore = eventSequenceStore;
//        _idGenerator = idGenerator;
//        _hiLoHighValueGenerator = hiLoHighValueGenerator;
//        _redisIdGenerator = redisIdGenerator;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        var count = 0;

//        //await _redisIdGenerator.NextLongIdAsync(IdType.BotUpdateId, 1001);

//        //var sw = Stopwatch.StartNew();
//        //var n = 1000;
//        //while (true)
//        //{
//        //    await _redisIdGenerator.NextLongIdAsync(IdType.BotUpdateId, 1001);
//        //    count++;

//        //    if (count == n)
//        //    {
//        //        break;
//        //    }
//        //}
//        //sw.Stop();
//        //_logger.LogInformation("Redis Generate {n} {timespan}", n, sw.Elapsed);


//        //var count = 0;

//        //var m = _eventSequenceStore;
//        //m.GetNextSequence("test-aa");

//        //var sw = Stopwatch.StartNew();
//        //var n = 1000;
//        //while (true)
//        //{
//        //    m.GetNextSequence("test-aa");
//        //    count++;

//        //    if (count == n)
//        //    {
//        //        break;
//        //    }
//        //}
//        //sw.Stop();

//        //_logger.LogInformation("Generate {n} {timespan}", n, sw.Elapsed);

//        //count = 0;
//        //n = 100000;
//        //sw.Restart();
//        //while (true)
//        //{
//        //    await _idGenerator.NextLongIdAsync(IdType.BotUpdateId, 1001, 1, stoppingToken);

//        //    count++;
//        //    if (count == n)
//        //    {
//        //        break;
//        //    }
//        //}
//        //sw.Stop();

//        //_logger.LogInformation("Generate 2 {n} {timespan}", n, sw.Elapsed);

//        //count = 0;
//        //n = 10000;
//        //sw.Restart();
//        //while (true)
//        //{
//        //    await _hiLoHighValueGenerator.GetNewHighValueAsync(IdType.BotUpdateId, 1001, stoppingToken);

//        //    count++;
//        //    if (count == n)
//        //    {
//        //        break;
//        //    }
//        //}
//        //sw.Stop();

//        //_logger.LogInformation("Generate 3 {n} {timespan}", n, sw.Elapsed);

//        //while (!stoppingToken.IsCancellationRequested)
//        //{
//        //    await Task.Delay(TimeSpan.FromSeconds(20));

//        //    _logger.LogInformation("RabbitMQ status:{Status} {EventBus}", _connection.IsConnected,_eventBus);

//        //    if (_count % 100 == 0)
//        //    {
//        //        _count++;
//        //    }

//        //    if (_count >= long.MaxValue - 10)
//        //    {
//        //        await _eventBus.PublishAsync(new AppCodeCreatedIntegrationEvent(1, "1", "2222", 1));
//        //    }

//        //}
//    }
//}
