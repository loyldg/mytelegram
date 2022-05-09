// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IBankCardData : IObject
{
    string Title { get; set; }
    TVector<MyTelegram.Schema.IBankCardOpenUrl> OpenUrls { get; set; }

}
