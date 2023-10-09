//namespace MyTelegram.Messenger.Services.Impl;

//public class MyTelegramReadModelCacheStrategy : IReadModelCacheStrategy
//{
//    public Task<bool> ShouldCacheReadModelAsync<TReadModel>()
//    {
//        var type = typeof(TReadModel);
//        switch (type.Name)
//        {
//            case nameof(MyTelegram.ReadModel.MongoDB.UserReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChannelReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChatReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.ChannelFullReadModel):
//            case nameof(MyTelegram.ReadModel.MongoDB.PhotoReadModel):
//                return Task.FromResult(true);
//        }

//        return Task.FromResult(false);
//    }
//}