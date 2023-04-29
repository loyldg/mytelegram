// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupParticipants : IObject
{
    int Count { get; set; }
    TVector<Schema.IGroupCallParticipant> Participants { get; set; }
    string NextOffset { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
    int Version { get; set; }
}
