namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbIndexesCreator
{
    Task CreateAllIndexesAsync();
}