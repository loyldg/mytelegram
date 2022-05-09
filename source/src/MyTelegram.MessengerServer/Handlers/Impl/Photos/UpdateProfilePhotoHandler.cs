using MyTelegram.Handlers.Photos;
using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.Photos.IPhoto;

namespace MyTelegram.MessengerServer.Handlers.Impl.Photos;

public class UpdateProfilePhotoHandler : RpcResultObjectHandler<RequestUpdateProfilePhoto, IPhoto>,
    IUpdateProfilePhotoHandler
{
    protected override Task<IPhoto> HandleCoreAsync(IRequestInput input,
        RequestUpdateProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
