using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;
using TAuthorization = MyTelegram.Schema.Auth.TAuthorization;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class AuthorizationConverterLatest : IAuthorizationConverterLatest
{
    private readonly IObjectMapper _objectMapper;

    public AuthorizationConverterLatest(IObjectMapper objectMapper)
    {
        _objectMapper = objectMapper;
    }

    public virtual int Layer => Layers.LayerLatest;

    public int RequestLayer { get; set; }

    public virtual IAuthorization CreateAuthorization(IUser? user)
    {
        if (user == null)
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

        return new TAuthorization { User = user };
    }

    public virtual IAuthorization CreateSignUpAuthorization()
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

    public virtual Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel)
    {
        return ToAuthorization(deviceReadModel, -1);
    }

    public virtual IReadOnlyList<Schema.IAuthorization> ToAuthorizations(
        IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId)
    {
        var authList = new List<Schema.IAuthorization>();
        foreach (var deviceReadModel in deviceList)
        {
            authList.Add(ToAuthorization(deviceReadModel, selfPermAuthKeyId));
        }

        return authList;
    }

    public virtual IWebAuthorization ToWebAuthorization(IDeviceReadModel deviceReadModel)
    {
        return ToWebAuthorization(deviceReadModel, -1);
    }
    public virtual IReadOnlyList<IWebAuthorization> ToWebAuthorizations(
        IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId)
    {
        var authList = new List<IWebAuthorization>();
        foreach (var deviceReadModel in deviceList)
        {
            authList.Add(ToWebAuthorization(deviceReadModel, selfPermAuthKeyId));
        }

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

    private IWebAuthorization ToWebAuthorization(IDeviceReadModel deviceReadModel,
        long selfPermAuthKeyId)
    {
        var auth = _objectMapper.Map<IDeviceReadModel, TWebAuthorization>(deviceReadModel);
        //auth.AppName = deviceReadModel.LangPack;
        //auth.Country = "TestCountry";
        auth.Region = "test region";
        auth.Domain = "test domain";


        //auth.Current = selfPermAuthKeyId == deviceReadModel.PermAuthKeyId;
        // todo:use ip2region to query country by ip
        //
        return auth;
    }
}