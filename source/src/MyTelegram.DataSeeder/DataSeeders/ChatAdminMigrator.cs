using EventFlow.Queries;
using MyTelegram.Domain.Aggregates.Channel;
using MyTelegram.Queries;

namespace MyTelegram.DataSeeder.DataSeeders;

public class ChatAdminMigrator(ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    ILogger<ChatAdminMigrator> logger, IDataSeederHelper dataSeederHelper) : IDataSeeder, ITransientDependency
{
    public async Task SeedAsync()
    {
        var config = await dataSeederHelper.LoadDataSeederConfigAsync();
        if (config.IsChatAdminMigrated)
        {
            logger.LogInformation("Chat admin data has been migrated — skipping.");
            return;
        }

        var hasMoreData = true;
        var skip = 0;
        var limit = 1000;
        while (hasMoreData)
        {
            var chatAdminReadModels = await queryProcessor.ProcessAsync(new GetChatAdminListQuery(skip, limit));
            foreach (var chatAdminReadModel in chatAdminReadModels)
            {
                var command = new EditChannelAdminCommand2(
                    ChannelMemberId.Create(chatAdminReadModel.PeerId, chatAdminReadModel.UserId),
                    RequestInfo.Empty with { ReqMsgId = Random.Shared.NextInt64() },
                    chatAdminReadModel.PeerId,
                    chatAdminReadModel.UserId,
                    chatAdminReadModel.AdminRights.GetFlags().ToInt32(),
                    chatAdminReadModel.Rank ?? string.Empty
                );
                await commandBus.PublishAsync(command);
            }

            skip += chatAdminReadModels.Count;
            hasMoreData = chatAdminReadModels.Count == limit;
        }

        config.IsChatAdminMigrated = true;
        logger.LogInformation("Chat admin data migration completed. Count: {Count}", skip);
        await dataSeederHelper.SaveDataSeederConfigAsync();
    }
}