namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer166;

public class PremiumPromoConverterLayer166 : IPremiumPromoConverterLayer166
{
    public virtual int Layer => Layers.Layer166;

    public int RequestLayer { get; set; }

    public virtual IPremiumPromo ToPremiumPromo()
    {
        return new TPremiumPromo
        {
            //Currency = "USD",
            //MonthlyAmount = 399,
            StatusText =
                "By subscribing to Telegram Premium you agree to the Telegram Terms of Service and Privacy Policy.",
            StatusEntities = new TVector<IMessageEntity>(),
            Users = new TVector<IUser>(),
            VideoSections = new TVector<string>(),
            Videos = new TVector<IDocument>(),
            PeriodOptions = new TVector<IPremiumSubscriptionOption>
            {
                new TPremiumSubscriptionOption
                {
                    Current = true,
                    Amount = 399,
                    Currency = "USD",
                    Months = 1,
                    StoreProduct = "org.telegram.telegramPremium.monthly",
                    BotUrl = string.Empty
                }
            }
        };
    }
}
