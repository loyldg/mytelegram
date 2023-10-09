namespace MyTelegram.Messenger.Services.Impl;

public class DataSeeder : IDataSeeder
{
    private readonly ICommandBus _commandBus;

    private readonly IEventStore _eventStore;
    private readonly IRandomHelper _randomHelper;
    private readonly ISnapshotStore _snapshotStore;

    public DataSeeder(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IEventStore eventStore,
        ISnapshotStore snapshotStore
    )
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _eventStore = eventStore;
        _snapshotStore = snapshotStore;
    }

    public async Task SeedAsync()
    {
        await CreateOfficialUserAsync();
        //await CreateBotFatherUserAsync();
        var initUid = MyTelegramServerDomainConsts.UserIdInitId;
        var testUserCount = 30;
        for (var i = 1; i < testUserCount; i++)
        {
            await CreateUserIfNeedAsync(initUid + i,
                $"1{i}",
                $"{i}",
                $"{i}",
                $"user{i}",
                false);
        }
    }

    private async Task CreateOfficialUserAsync()
    {
        var userId = MyTelegramServerDomainConsts.OfficialUserId;
        var created = await CreateUserIfNeedAsync(userId,
            "42777",
            "MyTelegram",
            null,
            null,
            false);

        if (created)
        {
            var command = new SetSupportCommand(UserId.Create(userId), true);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            var setVerifiedCommand = new SetVerifiedCommand(UserId.Create(userId), true);
            await _commandBus.PublishAsync(setVerifiedCommand, CancellationToken.None);
        }
    }

    private async Task<bool> CreateUserIfNeedAsync(long userId,
        string phoneNumber,
        string firstName,
        string? lastName,
        string? userName,
        bool bot)
    {
        var aggregateId = UserId.Create(userId);
        var u = new UserAggregate(aggregateId);
        await u.LoadAsync(_eventStore, _snapshotStore, CancellationToken.None);
        if (u.IsNew)
        {
            var accessHash = _randomHelper.NextLong();
            var createUserCommand =
                new CreateUserCommand(aggregateId,
                    new RequestInfo(0, 0, 0, 0, Guid.Empty, 0, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()),
                    userId,
                    accessHash,
                    phoneNumber,
                    firstName,
                    lastName,
                    userName,
                    bot);
            await _commandBus.PublishAsync(createUserCommand, CancellationToken.None);

            return true;
        }

        return false;
    }
}
