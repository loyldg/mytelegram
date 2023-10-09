using MyTelegram.ReadModel.MongoDB;

namespace MyTelegram.Messenger.Services.Impl;

public class NullMongoDbIndexCreator : IMongoDbIndexesCreator
{
    public Task CreateAllIndexesAsync()
    {
        return Task.CompletedTask;
    }
}
