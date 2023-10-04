using MyTelegram.Handlers.Photos;
using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.IPhoto;

namespace MyTelegram.MessengerServer.Handlers.Impl.Photos;

public class GetUserPhotosHandler : RpcResultObjectHandler<RequestGetUserPhotos, IPhotos>,
    IGetUserPhotosHandler, IProcessedHandler
{
    protected override Task<IPhotos> HandleCoreAsync(IRequestInput input,
        RequestGetUserPhotos obj)
    {
        return Task.FromResult<IPhotos>(new TPhotos { Photos = new TVector<IPhoto>(), Users = new TVector<IUser>() });
    }
}