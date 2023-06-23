using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;
using TAuthorization = MyTelegram.Schema.Auth.TAuthorization;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlAuthorizationConverter : ITlAuthorizationConverter
{
    private readonly IObjectMapper _objectMapper;
    private readonly ITlUserConverter _userConverter;

    public TlAuthorizationConverter(
        IObjectMapper objectMapper,
        ITlUserConverter userConverter)
    {
        _objectMapper = objectMapper;
        _userConverter = userConverter;
    }

    public IAuthorization CreateAuthorization(SignInSuccessEvent aggregateEvent)
    {
        var tUser = _objectMapper.Map<SignInSuccessEvent, TUser>(aggregateEvent);
        tUser.Phone = aggregateEvent.PhoneNumber;
        tUser.Photo = new TUserProfilePhotoEmpty();
        tUser.Id = aggregateEvent.UserId;
        tUser.Status = new TUserStatusOnline { Expires = DateTime.UtcNow.AddMinutes(5).ToTimestamp() };
        tUser.Self = true;
        var r = new TAuthorization { User = tUser };

        return r;
    }

    public IAuthorization CreateAuthorizationFromUser(IUserReadModel? user)
    {
        if (user == null)
            return new TAuthorizationSignUpRequired
            {
                TermsOfService = new TTermsOfService
                {
                    Entities = new TVector<IMessageEntity>(),
                    Id = new TDataJSON
                    {
                        Data =
                            "{\"country\":\"US\",\"min_age\":false,\"terms_key\":\"TERMS_OF_SERVICE\",\"terms_lang\":\"en\",\"terms_version\":1,\"terms_hash\":\"7dca806cb8d387c07c778ce9ef6aac04\"}"
                    },
                    Text =
                        "By signing up for MyTelegram, you agree not to:\n\n- Use our service to send spam or scam users.\n- Promote violence on publicly viewable Telegram bots, groups or channels.\n- Post pornographic content on publicly viewable MyTelegram bots, groups or channels.\n\nWe reserve the right to update these Terms of Service later."
                }
            };

        var tUser = _userConverter.ToUser(user, user.UserId);
        var r = new TAuthorization { User = tUser };

        return r;
    }

    public IAuthorization CreateAuthorizationFromUser(UserCreatedEvent userCreatedEvent)
    {
        var tUser = _userConverter.ToUser(userCreatedEvent);
        return new TAuthorization { User = tUser };
        // todo:map user created event to user read model
        //return CreateAuthorizationFromUser(_objectMapper.Map<UserCreatedEvent, UserReadModel>(userCreatedEvent));
    }

    public IAuthorization CreateSignUpAuthorization()
    {
        return new TAuthorizationSignUpRequired
        {
            TermsOfService = new TTermsOfService
            {
                Entities = new TVector<IMessageEntity>(),
                Id = new TDataJSON
                {
                    Data =
                        "{\"country\":\"US\",\"min_age\":false,\"terms_key\":\"TERMS_OF_SERVICE\",\"terms_lang\":\"en\",\"terms_version\":1,\"terms_hash\":\"7dca806cb8d387c07c778ce9ef6aac04\"}"
                },
                Text =
                    "By signing up for MyTelegram, you agree not to:\n\n- Use our service to send spam or scam users.\n- Promote violence on publicly viewable Telegram bots, groups or channels.\n- Post pornographic content on publicly viewable MyTelegram bots, groups or channels.\n\nWe reserve the right to update these Terms of Service later."
            }
        };
    }

    public Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel)
    {
        return ToAuthorization(deviceReadModel, -1);
    }

    public IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId)
    {
        var authList = new List<Schema.IAuthorization>();
        foreach (var deviceReadModel in deviceList) authList.Add(ToAuthorization(deviceReadModel, selfPermAuthKeyId));

        return authList;
    }

    private Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel,
        long selfPermAuthKeyId)
    {
        var auth = _objectMapper.Map<IDeviceReadModel, Schema.TAuthorization>(deviceReadModel);
        auth.AppName = deviceReadModel.LangPack;
        auth.Country = "TestCountry";
        auth.Region = string.Empty;

        auth.Current = selfPermAuthKeyId == deviceReadModel.PermAuthKeyId;
        // todo:use ip2region to query country by ip
        // 
        return auth;
    }
}