using MyTelegram.Domain.Aggregates.Photo;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;
/// <summary>
/// Deletes profile photos. The method returns a list of successfully deleted photo IDs.
/// <para><c>See <a href="https://corefork.telegram.org/method/photos.deletePhotos"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeletePhotosHandler(IQueryProcessor queryProcessor, ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestDeletePhotos, TVector<long>>
{
    protected override async Task<TVector<long>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Photos.RequestDeletePhotos obj)
    {
        var photoIds = new List<long>();
        foreach (var inputPhoto in obj.Id)
        {
            switch (inputPhoto)
            {
                case TInputPhoto inputPhoto1:
                    photoIds.Add(inputPhoto1.Id);
                    break;
            }
        }

        var deletedIds = new List<long>();
        var photoReadModels = await queryProcessor.ProcessAsync(new GetPhotoListQuery(input.UserId, photoIds));

        foreach (var photoReadModel in photoReadModels)
        {
            var command = new DeletePhotoCommand(PhotoId.Create(photoReadModel.PhotoId), input.UserId);
            await commandBus.PublishAsync(command);
            deletedIds.Add(photoReadModel.PhotoId);
        }

        return [.. deletedIds];
    }
}