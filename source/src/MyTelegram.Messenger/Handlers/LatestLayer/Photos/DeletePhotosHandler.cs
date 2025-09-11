using MyTelegram.Domain.Aggregates.Photo;
using MyTelegram.Domain.Commands.Photo;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;

///<summary>
/// Deletes profile photos. The method returns a list of successfully deleted photo IDs.
/// See <a href="https://corefork.telegram.org/method/photos.deletePhotos" />
///</summary>
internal sealed class DeletePhotosHandler(IQueryProcessor queryProcessor,ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestDeletePhotos, TVector<long>>
{
    protected override async Task<TVector<long>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestDeletePhotos obj)
    {
        var photoIds = new Dictionary<long, long>();

        foreach (var inputPhoto in obj.Id)
        {
            switch (inputPhoto)
            {
                case TInputPhoto inputPhoto1:
                    photoIds.TryAdd(inputPhoto1.Id, inputPhoto1.AccessHash);
                    break;
            }
        }

        var deletedIds = new List<long>();
        var photoReadModels =
            await queryProcessor.ProcessAsync(new GetPhotoListQuery(input.UserId, photoIds.Keys.ToList()));
        foreach (var photoReadModel in photoReadModels)
        {
            if (photoIds.TryGetValue(photoReadModel.PhotoId, out var accessHash))
            {
                if (accessHash == photoReadModel.AccessHash)
                {
                    deletedIds.Add(photoReadModel.PhotoId);
                }
            }
        }

        foreach (var deletedId in deletedIds)
        {
            var command = new DeletePhotoCommand(PhotoId.Create(deletedId), input.UserId);
            await commandBus.PublishAsync(command);
        }

        return [.. deletedIds];
    }
}
