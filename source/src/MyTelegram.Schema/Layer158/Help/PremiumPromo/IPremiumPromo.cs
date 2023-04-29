// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface IPremiumPromo : IObject
{
    string StatusText { get; set; }
    TVector<Schema.IMessageEntity> StatusEntities { get; set; }
    TVector<string> VideoSections { get; set; }
    TVector<Schema.IDocument> Videos { get; set; }
    TVector<Schema.IPremiumSubscriptionOption> PeriodOptions { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
