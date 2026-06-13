namespace MyTelegram.DataSeeder.DataSeeders;

public class UserDataSeeder(
    ICommandBus commandBus,
    IEventStore eventStore,
    ILogger<UserDataSeeder> logger,
    IOptionsMonitor<MyTelegramDataSeederOptions> options,
    IDataSeederHelper dataSeederHelper,
    ISnapshotStore snapshotStore)
    : IUserDataSeeder, ITransientDependency
{
    public async Task SeedAsync()
    {
        var config = await dataSeederHelper.LoadDataSeederConfigAsync();

        if (config.IsUserCreated)
        {
            await UpdateServiceNotificationAccountBioAsync();

            return;
        }

        await CreateOfficialUserAsync();
        await CreateDefaultSupportUserAsync();
        await CreateAnonymousUserAsync();

        if (options.CurrentValue.CreateTestUsers)
        {
            var initUserId = MyTelegramConsts.UserIdInitId;
            var testUserCount = 30;
            for (var i = 1; i < testUserCount; i++)
            {
                await CreateUserIfNeededAsync(initUserId + i,
                    $"1{i}",
                    $"{i}",
                    $"{i}",
                    $"user{i}",
                    false);
            }
        }
    }

    public async Task<bool> CreateUserIfNeededAsync(long userId,
        string phoneNumber,
        string firstName,
        string? lastName,
        string? userName,
        bool bot)
    {
        var aggregateId = UserId.Create(userId);
        var u = new UserAggregate(aggregateId);
        await u.LoadAsync(eventStore, snapshotStore, CancellationToken.None);
        if (u.IsNew)
        {
            var accessHash = Random.Shared.NextInt64();
            var createUserCommand =
                new CreateUserCommand(aggregateId,
                    RequestInfo.Empty with { Date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() },
                    userId,
                    accessHash,
                    phoneNumber,
                    firstName,
                    lastName,
                    userName,
                    bot
                );
            await commandBus.PublishAsync(createUserCommand);

            if (userId != MyTelegramConsts.NotificationServiceUserId)
            {
                var command = new UpdateUserPremiumStatusCommand(u.Id, true);
                await commandBus.PublishAsync(command);
            }

            logger.LogInformation("User {UserName} created successfully", userName ?? firstName);

            return true;
        }

        return false;
    }

    private async Task CreateDefaultSupportUserAsync()
    {
        var userId = MyTelegramConsts.DefaultSupportUserId;
        var created = await CreateUserIfNeededAsync(userId,
            MyTelegramConsts.DefaultSupportUserId.ToString(),
            "MyTelegram Support",
            null,
            null,
            false);

        if (created)
        {
            var command = new SetSupportCommand(UserId.Create(userId), true);
            await commandBus.PublishAsync(command);

            var setVerifiedCommand = new SetVerifiedCommand(UserId.Create(userId), true);
            await commandBus.PublishAsync(setVerifiedCommand);
            logger.LogInformation("MyTelegram support user created successfully");
        }
    }

    private async Task CreateAnonymousUserAsync()
    {
        var userId = MyTelegramConsts.AnonymousUserId;
        var firstName = "Anonymous User";
        await CreateUserIfNeededAsync(userId, string.Empty, firstName, null, null, false);
    }

    private async Task CreateOfficialUserAsync()
    {
        var userId = MyTelegramConsts.NotificationServiceUserId;
        var created = await CreateUserIfNeededAsync(userId,
            "42777",
            "MyTelegram",
            null,
            null,
            false);

        if (created)
        {
            var command = new SetSupportCommand(UserId.Create(userId), true);
            await commandBus.PublishAsync(command);

            var setVerifiedCommand = new SetVerifiedCommand(UserId.Create(userId), true);
            await commandBus.PublishAsync(setVerifiedCommand);
            logger.LogInformation("MyTelegram notification user created successfully");
        }

        await UpdateServiceNotificationAccountBioAsync();
    }

    private async Task UpdateServiceNotificationAccountBioAsync()
    {
        var config = await dataSeederHelper.LoadDataSeederConfigAsync();
        if (!config.IsServiceNotificationAccountBioUpdated)
        {
            await UpdateUserBioAsync(MyTelegramConsts.NotificationServiceUserId, "This service is powered by the MyTelegram open-source project.\nLearn more: https://github.com/loyldg/mytelegram");
            config.IsServiceNotificationAccountBioUpdated = true;
            await dataSeederHelper.SaveDataSeederConfigAsync();
        }
    }

    private Task UpdateUserBioAsync(long userId, string bio)
    {
        var command = new UpdateAboutCommand(UserId.Create(userId), bio);
        return commandBus.PublishAsync(command);
    }
}