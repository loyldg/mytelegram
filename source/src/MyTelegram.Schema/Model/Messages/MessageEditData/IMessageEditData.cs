// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IMessageEditData : IObject
{
    BitArray Flags { get; set; }
    bool Caption { get; set; }

}
