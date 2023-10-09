namespace MyTelegram.Messenger.Services.Impl;

//public interface IUserAppService
//{

//}

public class PhotoAppService : IPhotoAppService
{
    private readonly IQueryProcessor _queryProcessor;

    public PhotoAppService(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IUserReadModel? userReadModel, IContactReadModel? contactReadModel = null)
    {
        if (userReadModel == null)
        {
            return Task.FromResult<IReadOnlyCollection<IPhotoReadModel>>(Array.Empty<IPhotoReadModel>());
        }

        var photoIds = new List<long>();
        if (userReadModel.ProfilePhotoId.HasValue)
        {
            photoIds.Add(userReadModel.ProfilePhotoId.Value);
        }
        if (userReadModel.FallbackPhotoId.HasValue)
        {
            photoIds.Add(userReadModel.FallbackPhotoId.Value);
        }

        //if (contactReadModel?.PhotoId.HasValue ?? false)
        //{
        //    photoIds.Add(contactReadModel.PhotoId.Value);
        //}

        if (photoIds.Count == 0)
        {
            return Task.FromResult<IReadOnlyCollection<IPhotoReadModel>>(Array.Empty<IPhotoReadModel>());
        }

        return _queryProcessor.ProcessAsync(new GetPhotosByPhotoIdLisQuery(photoIds), default);
    }

    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IReadOnlyCollection<IUserReadModel> userReadModels, IReadOnlyCollection<IContactReadModel>? contactReadModels = null)
    {
        var photoIds = new List<long>();
        foreach (var userReadModel in userReadModels)
        {
            if (userReadModel.ProfilePhotoId.HasValue)
            {
                photoIds.Add(userReadModel.ProfilePhotoId.Value);
            }
            if (userReadModel.FallbackPhotoId.HasValue)
            {
                photoIds.Add(userReadModel.FallbackPhotoId.Value);
            }
        }

        //if (contactReadModels?.Count > 0)
        //{
        //    foreach (var contactReadModel in contactReadModels)
        //    {
        //        if (contactReadModel.PhotoId.HasValue)
        //        {
        //            photoIds.Add(contactReadModel.PhotoId.Value);
        //        }
        //    }
        //}

        if (photoIds.Count == 0)
        {
            return Task.FromResult<IReadOnlyCollection<IPhotoReadModel>>(Array.Empty<IPhotoReadModel>());
        }

        return _queryProcessor.ProcessAsync(new GetPhotosByPhotoIdLisQuery(photoIds.Distinct().ToList()), default);
    }

    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IReadOnlyCollection<IChannelReadModel> channelReadModels)
    {
        var photoIds = new List<long>();
        foreach (var channelReadModel in channelReadModels)
        {
            if (channelReadModel.PhotoId.HasValue)
            {
                photoIds.Add(channelReadModel.PhotoId.Value);
            }
        }

        if (photoIds.Count == 0)
        {
            return Task.FromResult<IReadOnlyCollection<IPhotoReadModel>>(Array.Empty<IPhotoReadModel>());
        }

        return _queryProcessor.ProcessAsync(new GetPhotosByPhotoIdLisQuery(photoIds.Distinct().ToList()), default);
    }

    public Task<IPhotoReadModel?> GetPhotoAsync(long? photoId)
    {
        if (!photoId.HasValue)
        {
            return Task.FromResult<IPhotoReadModel?>(null);
        }

        return _queryProcessor.ProcessAsync(new GetPhotoByIdQuery(photoId.Value), default);
    }

    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IList<long> photoIds)
    {
        if (photoIds.Count == 0)
        {
            return Task.FromResult<IReadOnlyCollection<IPhotoReadModel>>(Array.Empty<IPhotoReadModel>());
        }

        return _queryProcessor.ProcessAsync(new GetPhotosByPhotoIdLisQuery(photoIds.Distinct().ToList()), default);
    }
}