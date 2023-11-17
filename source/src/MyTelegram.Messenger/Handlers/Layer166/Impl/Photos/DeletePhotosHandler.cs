// ReSharper disable All

namespace MyTelegram.Handlers.Photos;

///<summary>
/// Deletes profile photos. The method returns a list of successfully deleted photo IDs.
/// See <a href="https://corefork.telegram.org/method/photos.deletePhotos" />
///</summary>
internal sealed class DeletePhotosHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestDeletePhotos, TVector<long>>,
    Photos.IDeletePhotosHandler
{
    protected override Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestDeletePhotos obj)
    {
        throw new NotImplementedException();
    }
}
