using MyTelegram.Handlers.Photos;
using MyTelegram.Schema.Photos;

namespace MyTelegram.MessengerServer.Handlers.Impl.Photos;

public class DeletePhotosHandler : RpcResultObjectHandler<RequestDeletePhotos, TVector<long>>,
    IDeletePhotosHandler
{
    protected override Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        RequestDeletePhotos obj)
    {
        throw new NotImplementedException();
    }
}
