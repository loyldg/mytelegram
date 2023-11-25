// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Filter for fetching channel participants
/// See <a href="https://corefork.telegram.org/constructor/ChannelParticipantsFilter" />
///</summary>
[JsonDerivedType(typeof(TChannelParticipantsRecent), nameof(TChannelParticipantsRecent))]
[JsonDerivedType(typeof(TChannelParticipantsAdmins), nameof(TChannelParticipantsAdmins))]
[JsonDerivedType(typeof(TChannelParticipantsKicked), nameof(TChannelParticipantsKicked))]
[JsonDerivedType(typeof(TChannelParticipantsBots), nameof(TChannelParticipantsBots))]
[JsonDerivedType(typeof(TChannelParticipantsBanned), nameof(TChannelParticipantsBanned))]
[JsonDerivedType(typeof(TChannelParticipantsSearch), nameof(TChannelParticipantsSearch))]
[JsonDerivedType(typeof(TChannelParticipantsContacts), nameof(TChannelParticipantsContacts))]
[JsonDerivedType(typeof(TChannelParticipantsMentions), nameof(TChannelParticipantsMentions))]
public interface IChannelParticipantsFilter : IObject
{

}
