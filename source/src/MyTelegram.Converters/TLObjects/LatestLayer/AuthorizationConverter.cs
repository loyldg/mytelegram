using MyTelegram.Schema.Auth;
using MyTelegram.Schema.Help;

namespace MyTelegram.Converters.TLObjects.LatestLayer;

internal sealed class AuthorizationConverter(IObjectMapper objectMapper) : IAuthorizationConverter, ITransientDependency
{
    
    public int Layer => Layers.LayerLatest;

    public MyTelegram.Schema.Auth.IAuthorization CreateAuthorization(IUser? user, bool setupPasswordRequired = false)
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

        return new MyTelegram.Schema.Auth.TAuthorization
        {
            User = user,
            SetupPasswordRequired = setupPasswordRequired,
            OtherwiseReloginDays = setupPasswordRequired ? 1 : null
        };
    }

    public MyTelegram.Schema.Auth.IAuthorization CreateSignUpAuthorization()
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

    public Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel, long selfPermAuthKeyId = -1)
    {
        var authorization = objectMapper.Map<IDeviceReadModel, Schema.TAuthorization>(deviceReadModel);
        authorization.AppName = deviceReadModel.LangPack;
        authorization.Country = "Test Country";
        authorization.Region = string.Empty;

        authorization.Current = selfPermAuthKeyId == deviceReadModel.PermAuthKeyId;

        return authorization;
    }

    public IWebAuthorization ToWebAuthorization(IDeviceReadModel deviceReadModel, long selfPermAuthKeyId = -1)
    {
        var authorization = objectMapper.Map<IDeviceReadModel, TWebAuthorization>(deviceReadModel);
        authorization.Region = "Test region";
        authorization.Domain = "Test domain";

        return authorization;
    }

    public IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceReadModels,
        long selfPermAuthKeyId = -1)
    {
        return deviceReadModels.Select(p => ToAuthorization(p, selfPermAuthKeyId)).ToList();
    }

    public IReadOnlyList<IWebAuthorization> ToWebAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceReadModels,
        long selfPermAuthKeyId = -1)
    {
        return deviceReadModels.Select(p => ToWebAuthorization(p, selfPermAuthKeyId)).ToList();
    }
}