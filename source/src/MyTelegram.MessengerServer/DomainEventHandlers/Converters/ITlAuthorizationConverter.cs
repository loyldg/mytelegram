using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlAuthorizationConverter
{
    IAuthorization CreateAuthorization(SignInSuccessEvent aggregateEvent);
    IAuthorization CreateAuthorizationFromUser(IUserReadModel? user);
    IAuthorization CreateAuthorizationFromUser(UserCreatedEvent userCreatedEvent);
    IAuthorization CreateSignUpAuthorization();
    Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel);

    IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId);
}
