// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupCallStreamChannels : IObject
{
    TVector<MyTelegram.Schema.IGroupCallStreamChannel> Channels { get; set; }
}
