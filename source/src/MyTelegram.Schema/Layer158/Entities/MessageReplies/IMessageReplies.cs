// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageReplies : IObject
{
    BitArray Flags { get; set; }
    bool Comments { get; set; }
    int Replies { get; set; }
    int RepliesPts { get; set; }
    TVector<MyTelegram.Schema.IPeer>? RecentRepliers { get; set; }
    long? ChannelId { get; set; }
    int? MaxId { get; set; }
    int? ReadMaxId { get; set; }
}
