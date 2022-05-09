// ReSharper disable All

namespace MyTelegram.Schema.Updates;

public interface IChannelDifference : IObject
{
    BitArray Flags { get; set; }
    bool Final { get; set; }
    int? Timeout { get; set; }

}
