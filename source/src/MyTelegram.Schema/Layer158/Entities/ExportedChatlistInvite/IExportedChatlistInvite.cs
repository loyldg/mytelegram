// ReSharper disable All

namespace MyTelegram.Schema;

public interface IExportedChatlistInvite : IObject
{
    BitArray Flags { get; set; }
    string Title { get; set; }
    string Url { get; set; }
    TVector<MyTelegram.Schema.IPeer> Peers { get; set; }
}
