// ReSharper disable All

namespace MyTelegram.Schema;

public interface IExportedChatInvite : IObject
{
    BitArray Flags { get; set; }
    bool Revoked { get; set; }
    bool Permanent { get; set; }
    string Link { get; set; }
    long AdminId { get; set; }
    int Date { get; set; }
    int? StartDate { get; set; }
    int? ExpireDate { get; set; }
    int? UsageLimit { get; set; }
    int? Usage { get; set; }

}
