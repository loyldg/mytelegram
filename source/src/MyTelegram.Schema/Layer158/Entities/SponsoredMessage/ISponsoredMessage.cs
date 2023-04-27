// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISponsoredMessage : IObject
{
    BitArray Flags { get; set; }
    bool Recommended { get; set; }
    bool ShowPeerPhoto { get; set; }
    byte[] RandomId { get; set; }
    MyTelegram.Schema.IPeer? FromId { get; set; }
    MyTelegram.Schema.IChatInvite? ChatInvite { get; set; }
    string? ChatInviteHash { get; set; }
    int? ChannelPost { get; set; }
    string? StartParam { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    string? SponsorInfo { get; set; }
    string? AdditionalInfo { get; set; }
}
