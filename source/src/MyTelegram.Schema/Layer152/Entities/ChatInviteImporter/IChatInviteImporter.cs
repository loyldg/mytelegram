// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatInviteImporter : IObject
{
    BitArray Flags { get; set; }
    bool Requested { get; set; }
    long UserId { get; set; }
    int Date { get; set; }
    string? About { get; set; }
    long? ApprovedBy { get; set; }
}
