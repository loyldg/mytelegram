// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IGroupCall : IObject
{
    MyTelegram.Schema.IGroupCall Call { get; set; }
    TVector<MyTelegram.Schema.IGroupCallParticipant> Participants { get; set; }
    string ParticipantsNextOffset { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
