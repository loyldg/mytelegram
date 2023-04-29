// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupCallStreamChannels : IObject
{
    TVector<Schema.IGroupCallStreamChannel> Channels { get; set; }
}
