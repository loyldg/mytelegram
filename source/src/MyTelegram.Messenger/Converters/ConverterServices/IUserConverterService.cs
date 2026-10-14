namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IUserConverterService
{
    Task<ILayeredUser> GetUserAsync(IRequestWithAccessHashKeyId request, long userId, bool skipSetContactProperties = true,
        bool skipPrivacy = true);

    Task<List<ILayeredUser>> GetUserListAsync(IRequestWithAccessHashKeyId request, List<long> userIds, bool skipSetContactProperties = true,
        bool skipCheckPrivacy = true);

    Task<IUserFull> GetUserFullAsync(IRequestWithAccessHashKeyId request, long userId);

    Task<IUserFull> ToUserFullAsync(IRequestWithAccessHashKeyId request,
        IUserReadModel userReadModel,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels,
        IReadOnlyCollection<IContactReadModel>? contactReadModels,
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels);
    Task<ILayeredUser> ToUserAsync(IRequestWithAccessHashKeyId request, IUserReadModel userReadModel, IReadOnlyCollection<IPhotoReadModel>? photoReadModels = null,
        IContactReadModel? contactReadModel = null, IContactReadModel? targetUserContactReadModel = null, IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null);

    Task<List<ILayeredUser>> ToUserListAsync(IRequestWithAccessHashKeyId request, IReadOnlyCollection<IUserReadModel> userReadModels,
        IReadOnlyCollection<IPhotoReadModel> photoReadModels,
        IReadOnlyCollection<IContactReadModel> contactReadModels,
        IReadOnlyCollection<IPrivacyReadModel> privacyReadModels);
}