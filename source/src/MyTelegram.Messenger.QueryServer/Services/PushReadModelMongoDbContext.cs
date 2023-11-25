using Microsoft.Extensions.Configuration;

namespace MyTelegram.Messenger.QueryServer.Services;

public class PushReadModelMongoDbContext : DefaultReadModelMongoDbContext
{
    public PushReadModelMongoDbContext(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string GetKeyOfDatabaseNameInConfiguration() => "App:QueryServerReadModelDatabaseName";
}