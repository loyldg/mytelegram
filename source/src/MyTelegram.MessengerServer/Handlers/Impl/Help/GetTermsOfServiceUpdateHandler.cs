using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetTermsOfServiceUpdateHandler :
    RpcResultObjectHandler<RequestGetTermsOfServiceUpdate, ITermsOfServiceUpdate>,
    IGetTermsOfServiceUpdateHandler, IProcessedHandler
{
    protected override Task<ITermsOfServiceUpdate> HandleCoreAsync(IRequestInput input,
        RequestGetTermsOfServiceUpdate obj)
    {
        var userId = input.UserId;
        if (userId > 0)
        {
            var alreadyRegisteredTermsOfService = new TTermsOfServiceUpdateEmpty
            {
                Expires = (int)DateTimeOffset.UtcNow.AddDays(10).ToUnixTimeSeconds()
            };

            return Task.FromResult<ITermsOfServiceUpdate>(alreadyRegisteredTermsOfService);
        }

        var r = new TTermsOfServiceUpdate
        {
            Expires = (int)((DateTimeOffset)DateTime.UtcNow.AddDays(10)).ToUnixTimeSeconds(),
            //TermsOfService=new TTermsOfService()
            TermsOfService = new TTermsOfService
            {
                Entities = new TVector<IMessageEntity>(),
                //Popup = false,
                Id = new TDataJSON
                {
                    Data =
                        "{\"country\":\"US\",\"min_age\":false,\"terms_key\":\"TERMS_OF_SERVICE\",\"terms_lang\":\"en\",\"terms_version\":1,\"terms_hash\":\"7dca806cb8d387c07c778ce9ef6aac04\"}"
                },
                Text =
                    "By signing up for MyTelegram, you agree not to:\n\n- Use our service to send spam or scam users.\n- Promote violence on publicly viewable MyTelegram bots, groups or channels.\n- Post pornographic content on publicly viewable MyTelegram bots, groups or channels.\n\nWe reserve the right to update these Terms of Service later."
                //MinAgeConfirm = 0
            }
        };

        return Task.FromResult<ITermsOfServiceUpdate>(r);
    }
}