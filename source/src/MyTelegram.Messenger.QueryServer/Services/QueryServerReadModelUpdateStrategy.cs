namespace MyTelegram.Messenger.QueryServer.Services;

public class QueryServerReadModelUpdateStrategy : IReadModelUpdateStrategy
{
    public Task<bool> ShouldUpdateReadModelAsync<TReadModel>()
    {
        var type = typeof(TReadModel);
        switch (type.Name)
        {
            case nameof(MyTelegram.ReadModel.MongoDB.RpcResultReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.UpdatesReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsReadModel):
            case nameof(MyTelegram.ReadModel.MongoDB.PtsForAuthKeyIdReadModel):
                return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}