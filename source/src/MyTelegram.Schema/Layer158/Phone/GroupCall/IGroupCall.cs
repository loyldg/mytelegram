// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupCall : IObject
{
    Schema.IGroupCall Call { get; set; }
    TVector<Schema.IGroupCallParticipant> Participants { get; set; }
    string ParticipantsNextOffset { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
