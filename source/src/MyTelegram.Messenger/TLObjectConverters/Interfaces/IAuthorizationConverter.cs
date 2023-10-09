using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IAuthorizationConverter : ILayeredConverter
{
    IAuthorization CreateAuthorization(IUser? user);
    //IAuthorization CreateAuthorization(SignInSuccessEvent aggregateEvent);
    //IAuthorization CreateAuthorizationFromUser(IUserReadModel? user);
    //IAuthorization CreateAuthorizationFromUser(UserCreatedEvent userCreatedEvent);
    IAuthorization CreateSignUpAuthorization();
    Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel);
    Schema.IWebAuthorization ToWebAuthorization(IDeviceReadModel deviceReadModel);

    //IAuthorization ToAuthorization(UserCloudPasswordRemovedEvent aggregateEvent);

    IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId);

    IReadOnlyList<Schema.IWebAuthorization> ToWebAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId);
}