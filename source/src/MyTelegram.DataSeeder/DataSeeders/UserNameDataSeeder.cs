using EventFlow.Queries;
using MyTelegram.Domain.Aggregates.UserName;
using MyTelegram.Queries;

namespace MyTelegram.DataSeeder.DataSeeders;

public class UserNameDataSeeder(IQueryProcessor queryProcessor, ICommandBus commandBus, ILogger<UserNameDataSeeder> logger, IDataSeederHelper dataSeederHelper) : IDataSeeder, ITransientDependency
{
    public async Task SeedAsync()
    {
        var config = await dataSeederHelper.LoadDataSeederConfigAsync();
        if (config.IsUserNameIdMigrated)
        {
            return;
        }

        int pageSize = 1000;
        int skip = 0;
        var hasMoreData = true;

        while (hasMoreData)
        {
            var userNameReadModels = await queryProcessor.ProcessAsync(new GetAllUserNameQuery(skip, pageSize));
            logger.LogInformation("Loaded usernames, count: {Count}", userNameReadModels.Count);
            foreach (var userNameReadModel in userNameReadModels)
            {
                // 
                if (HasUpperCase(userNameReadModel.UserName))
                {
                    var command = new CreateUserNameCommand(UserNameId.Create(userNameReadModel.UserName.ToLower()),
                        new Peer(userNameReadModel.PeerType, userNameReadModel.PeerId), userNameReadModel.UserName,
                        userNameReadModel.Date);
                    await commandBus.PublishAsync(command);

                    var deleteOldUserNameCommand = new DeleteUserNameCommand(UserNameId.With(userNameReadModel.Id));
                    await commandBus.PublishAsync(deleteOldUserNameCommand);

                    logger.LogInformation("The userName.Id has been changed from {OldId} to {NewId}. PeerId: {PeerId}, userName: {UserName}", userNameReadModel.Id, UserNameId.Create(userNameReadModel.UserName.ToLower()), userNameReadModel.PeerId, userNameReadModel.UserName);
                }
            }

            hasMoreData = userNameReadModels.Count == pageSize;
        }

        config.IsUserNameIdMigrated = true;
        await dataSeederHelper.SaveDataSeederConfigAsync();
    }

    private static bool HasUpperCase(string str)
    {
        foreach (var t in str)
        {
            if (char.IsUpper(t))
            {
                return true;
            }
        }

        return false;
    }
}
