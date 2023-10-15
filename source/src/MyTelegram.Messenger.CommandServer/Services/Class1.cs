namespace MyTelegram.Messenger.CommandServer.Services;
//public class MyTelegramCommandServerReadModelUpdateManager : IReadModelUpdateManager
//{
//    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
//    {
//        var type = typeof(TReadModel);
//        switch (type.Name)
//        {
//            case nameof(MyTelegram.ReadModel.MongoDB.UserReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChannelReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChatReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChannelFullReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.PhotoReadModel):
//                return Task.FromResult(UpdateStrategy.All);
//        }

//        return Task.FromResult(UpdateStrategy.UpdateDatabase);
//    }
//}

public interface IReadModelUpdateManager
{
    Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>();
}

public class ReadModelUpdateManager : IReadModelUpdateManager
{
    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
    {
        return Task.FromResult(UpdateStrategy.UpdateDatabase);
    }
}

public enum UpdateStrategy
{
    None,
    All,
    UpdateDatabase,
    UpdateCache,
}
