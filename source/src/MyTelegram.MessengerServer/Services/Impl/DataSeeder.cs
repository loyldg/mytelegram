using MyTelegram.Domain.Commands.User;

namespace MyTelegram.MessengerServer.Services.Impl;

public class DataSeeder : IDataSeeder //, ISingletonDependency
{
    private readonly ICommandBus _commandBus;

    private readonly IEventStore _eventStore;
    private readonly IRandomHelper _randomHelper;
    private readonly ISnapshotStore _snapshotStore;

    //private readonly IIdGenerator _idGenerator;

    public DataSeeder(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IEventStore eventStore,
        ISnapshotStore snapshotStore
    //IIdGenerator idGenerator
    )
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _eventStore = eventStore;
        _snapshotStore = snapshotStore;
        //_idGenerator = idGenerator;
    }

    public async Task SeedAsync()
    {
        await CreateOfficialUserAsync().ConfigureAwait(false);

        var initUid = MyTelegramServerDomainConsts.UserIdInitId;
        var testUserCount = 30;
        for (var i = 1; i < testUserCount; i++)
            //var userId = await _idGenerator.NextLongIdAsync(IdType.UserId);
            //Console.WriteLine($"############ Start create user:{initUid + i}");
            await CreateUserIfNeedAsync(initUid + i,
                $"1{i}",
                $"{i}",
                $"{i}",
                false).ConfigureAwait(false);
        //await CreateUserIfNeedAsync(1, "11", "1", "1", false);
        //await CreateUserIfNeedAsync(2, "12", "2", "2", false);
        //await CreateUserIfNeedAsync(3, "13", "3", "3", false);
        //await CreateUserIfNeedAsync(4, "14", "4", "4", false);
        //await CreateUserIfNeedAsync(5, "15", "5", "5", false);
        //await CreateUserIfNeedAsync(6, "16", "6", "6", false);
        //await CreateUserIfNeedAsync(7, "17", "7", "7", false);
        //await CreateUserIfNeedAsync(8, "18", "8", "8", false);
        //await CreateUserIfNeedAsync(9, "19", "9", "9", false);
        //await CreateUserIfNeedAsync(10, "110", "10", "10", false);
    }

    private async Task CreateOfficialUserAsync()
    {
        var userId = MyTelegramServerDomainConsts.OfficialUserId;
        var created = await CreateUserIfNeedAsync(userId,
            "42777",
            "Telegram",
            null,
            false).ConfigureAwait(false);

        if (created)
        {
            var command = new SetSupportCommand(UserId.Create(userId), true);
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

            var setVerifiedCommand = new SetVerifiedCommand(UserId.Create(userId), true);
            await _commandBus.PublishAsync(setVerifiedCommand, CancellationToken.None).ConfigureAwait(false);
        }
    }

    private async Task<bool> CreateUserIfNeedAsync(long userId,
        string phoneNumber,
        string firstName,
        string? lastName,
        bool bot)
    {
        var aggregateId = UserId.Create(userId);
        var u = new UserAggregate(aggregateId);
        await u.LoadAsync(_eventStore, _snapshotStore, CancellationToken.None).ConfigureAwait(false);
        if (u.IsNew)
        {
            var accessHash = _randomHelper.NextLong();
            var createUserCommand =
                new CreateUserCommand(aggregateId,
                    new RequestInfo(0, 0, 0, 0, Guid.Empty),
                    userId,
                    accessHash,
                    phoneNumber,
                    firstName,
                    lastName,
                    bot);
            await _commandBus.PublishAsync(createUserCommand, CancellationToken.None).ConfigureAwait(false);
            return true;
        }

        return false;
    }
}
