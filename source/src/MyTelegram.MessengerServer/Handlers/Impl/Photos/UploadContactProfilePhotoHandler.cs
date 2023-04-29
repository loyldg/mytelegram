// ReSharper disable All

using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.Photos.IPhoto;

namespace MyTelegram.Handlers.Photos;

internal sealed class UploadContactProfilePhotoHandler :
    RpcResultObjectHandler<RequestUploadContactProfilePhoto, IPhoto>,
    Photos.IUploadContactProfilePhotoHandler
{
    protected override Task<IPhoto> HandleCoreAsync(IRequestInput input,
        RequestUploadContactProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
