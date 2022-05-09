// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageFwdHeader : IObject
{
    BitArray Flags { get; set; }
    bool Imported { get; set; }
    MyTelegram.Schema.IPeer? FromId { get; set; }
    string? FromName { get; set; }
    int Date { get; set; }
    int? ChannelPost { get; set; }
    string? PostAuthor { get; set; }
    MyTelegram.Schema.IPeer? SavedFromPeer { get; set; }
    int? SavedFromMsgId { get; set; }
    string? PsaType { get; set; }

}
