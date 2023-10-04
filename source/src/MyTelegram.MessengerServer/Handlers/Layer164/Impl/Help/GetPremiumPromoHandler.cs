// ReSharper disable All

namespace MyTelegram.Handlers.Help;

public class GetPremiumPromoHandler :
    RpcResultObjectHandler<Schema.Help.RequestGetPremiumPromo, Schema.Help.IPremiumPromo>,
    Help.IGetPremiumPromoHandler, IProcessedHandler
{
    protected override Task<Schema.Help.IPremiumPromo> HandleCoreAsync(IRequestInput input,
        Schema.Help.RequestGetPremiumPromo obj)
    {
        return Task.FromResult<IPremiumPromo>(new TPremiumPromo
        {
            //Currency = "USD",
            //MonthlyAmount = 399,
            StatusText =
                "By subscribing to Telegram Premium you agree to the Telegram Terms of Service and Privacy Policy.",
            StatusEntities = new TVector<IMessageEntity>(),
            Users = new TVector<IUser>(),
            VideoSections = new TVector<string>(),
            Videos = new TVector<IDocument>(),
            PeriodOptions = new TVector<IPremiumSubscriptionOption>()
            {
                new TPremiumSubscriptionOption()
                {
                    Amount = 399,
                    Currency = "USD",
                    Months = 1,
                    StoreProduct = "org.telegram.telegramPremium.monthly",
                    BotUrl = string.Empty
                }
            }
        });
    }
}