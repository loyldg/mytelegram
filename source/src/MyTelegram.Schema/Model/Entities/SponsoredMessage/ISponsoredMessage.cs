// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISponsoredMessage : IObject
{
    BitArray Flags { get; set; }
    byte[] RandomId { get; set; }
    MyTelegram.Schema.IPeer FromId { get; set; }
    string? StartParam { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }

}
