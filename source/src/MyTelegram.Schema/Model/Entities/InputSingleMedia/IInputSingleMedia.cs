// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputSingleMedia : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IInputMedia Media { get; set; }
    long RandomId { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }

}
