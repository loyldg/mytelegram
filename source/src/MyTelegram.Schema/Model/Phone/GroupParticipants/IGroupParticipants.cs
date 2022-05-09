// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupParticipants : IObject
{
    int Count { get; set; }
    TVector<MyTelegram.Schema.IGroupCallParticipant> Participants { get; set; }
    string NextOffset { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
    int Version { get; set; }

}
