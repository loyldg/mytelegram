namespace MyTelegram.Messenger.Services.Impl;

public class PhotoAppService(IQueryProcessor queryProcessor, IReadModelCacheHelper<IPhotoReadModel> photoReadModelCacheHelper) : ReadModelWithCacheAppService<IPhotoReadModel>(photoReadModelCacheHelper), IPhotoAppService, ITransientDependency
{
    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IUserReadModel? userReadModel, IContactReadModel? contactReadModel = null)
    {
        IReadOnlyCollection<IPhotoReadModel> photos = [];
        if (userReadModel == null)
        {
            return Task.FromResult(photos);
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

        if (userReadModel.PersonalPhotoId.HasValue)
        {
            photoIds.Add(userReadModel.PersonalPhotoId.Value);
        }

        if (contactReadModel?.PhotoId.HasValue ?? false)
        {
            photoIds.Add(contactReadModel.PhotoId.Value);
        }

        return GetListAsync(photoIds);
    }

    public Task<IReadOnlyCollection<IPhotoReadModel>> GetPhotosAsync(IReadOnlyCollection<IUserReadModel> userReadModels,
        IReadOnlyCollection<IContactReadModel>? contactReadModels = null,
        IReadOnlyCollection<IChannelReadModel>? channelReadModels = null
        )
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

            if (userReadModel.PersonalPhotoId.HasValue)
            {
                photoIds.Add(userReadModel.PersonalPhotoId.Value);
            }
        }

        if (contactReadModels?.Count > 0)
        {
            foreach (var contactReadModel in contactReadModels)
            {
                if (contactReadModel.PhotoId.HasValue)
                {
                    photoIds.Add(contactReadModel.PhotoId.Value);
                }
            }
        }

        foreach (var channelReadModel in channelReadModels ?? [])
        {
            if (channelReadModel.PhotoId.HasValue)
            {
                photoIds.Add(channelReadModel.PhotoId.Value);
            }
        }

        return GetListAsync(photoIds.Distinct().ToList());
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

        return GetListAsync(photoIds);
    }

    protected override Task<IPhotoReadModel?> GetReadModelAsync(long id)
    {
        return queryProcessor.ProcessAsync(new GetPhotoByIdQuery(id));
    }

    protected override string GetReadModelId(IPhotoReadModel readModel) => readModel.Id;

    protected override long GetReadModelKey(IPhotoReadModel readModel) => readModel.PhotoId;
    protected override Task<IPhotoReadModel?> CreateNonExistsReadModelAsync(long id)
    {
        return Task.FromResult<IPhotoReadModel?>(null);
    }

    protected override Task<IReadOnlyCollection<IPhotoReadModel>> GetReadModelListAsync(List<long> ids)
    {
        return queryProcessor.ProcessAsync(new GetPhotosByPhotoIdLisQuery(ids));
    }
}