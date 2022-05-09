// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPaymentCharge : IObject
{
    string Id { get; set; }
    string ProviderChargeId { get; set; }

}
