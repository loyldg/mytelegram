// ReSharper disable All

namespace MyTelegram.Handlers.Photos;

internal sealed class UploadContactProfilePhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto, MyTelegram.Schema.Photos.IPhoto>,
    Photos.IUploadContactProfilePhotoHandler
{
    protected override Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
