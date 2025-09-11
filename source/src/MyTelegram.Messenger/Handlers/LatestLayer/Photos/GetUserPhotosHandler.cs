using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.IPhoto;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;

///<summary>
/// Returns the list of user photos.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MAX_ID_INVALID The provided max ID is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.getUserPhotos" />
///</summary>
internal sealed class GetUserPhotosHandler(
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    ILayeredService<IPhotoConverter> photoLayeredService,
    IPrivacyAppService privacyAppService,
    IAccessHashHelper accessHashHelper,
    IPeerHelper peerHelper) : RpcResultObjectHandler<Schema.Photos.RequestGetUserPhotos, Schema.Photos.IPhotos>
{
    protected override async Task<Schema.Photos.IPhotos> HandleCoreAsync(IRequestInput input,
        Schema.Photos.RequestGetUserPhotos obj)
    {
        var peer = peerHelper.GetPeer(obj.UserId, input.UserId);
        await accessHashHelper.CheckAccessHashAsync(input, obj.UserId);
        bool shouldReturnEmptyProfilePhotos = false;
        if (peer.PeerId != input.UserId)
        {
            await privacyAppService.ApplyPrivacyAsync(input.UserId, peer.PeerId, _ =>
                {
                    shouldReturnEmptyProfilePhotos = true;
                },
                 [PrivacyType.ProfilePhoto]);
        }

        if (!shouldReturnEmptyProfilePhotos)
        {
            var photoReadModels = await queryProcessor.ProcessAsync(new GetUserProfilePhotosQuery(peer.PeerId));
            if (photoReadModels.Count > 0)
            {
                var photos = photoReadModels.Select(p => photoLayeredService.GetConverter(input.Layer).ToPhoto(p)).ToList();
                if (photos.Count > 1)
                {
                    // If photos.Count>1, set the user's main photo in the first position
                    var userReadModel = await userAppService.GetAsync(peer.PeerId);
                    if (userReadModel != null)
                    {
                        long? photoId = userReadModel.ProfilePhotoId;
                        if (photoId == null && userReadModel.UserId == input.UserId)
                        {
                            photoId = userReadModel.PersonalPhotoId;
                        }

                        if (photoId != null)
                        {
                            var photo = photos.FirstOrDefault(p => p.Id == photoId.Value);
                            if (photo != null)
                            {
                                photos.RemoveAll(p => p.Id == photo.Id);
                                photos.Insert(0, photo);
                            }
                        }
                    }
                }

                return new TPhotos
                {
                    Photos = [.. photos],
                    Users = []
                };
            }
        }

        return new TPhotos
        {
            Photos = [],
            Users = []
        };
    }
}