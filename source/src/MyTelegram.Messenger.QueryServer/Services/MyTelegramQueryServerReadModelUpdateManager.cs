namespace MyTelegram.Messenger.QueryServer.Services;

public class MyTelegramQueryServerReadModelUpdateManager : IReadModelUpdateManager
{
    public Task<UpdateStrategy> GetReadModelUpdateStrategyAsync<TReadModel>()
    {
        var type = typeof(TReadModel);
        switch (type.Name)
        {
            case nameof(MyTelegram.ReadModel.MongoDB.UserReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChannelReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChatReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.ChannelFullReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PhotoReadModel):

                return Task.FromResult(UpdateStrategy.UpdateCache);

            case nameof(MyTelegram.ReadModel.MongoDB.RpcResultReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.UpdatesReadModel):

                return Task.FromResult(UpdateStrategy.UpdateDatabase);


            case nameof(MyTelegram.ReadModel.MongoDB.PtsReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsForAuthKeyIdReadModel):
                return Task.FromResult(UpdateStrategy.All);
        }

        return Task.FromResult(UpdateStrategy.None);
    }
}