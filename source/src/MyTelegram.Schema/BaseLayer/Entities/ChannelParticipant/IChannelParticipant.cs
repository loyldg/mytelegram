// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Channel participant
/// See <a href="https://corefork.telegram.org/constructor/ChannelParticipant" />
///</summary>
[JsonDerivedType(typeof(TChannelParticipant), nameof(TChannelParticipant))]
[JsonDerivedType(typeof(TChannelParticipantSelf), nameof(TChannelParticipantSelf))]
[JsonDerivedType(typeof(TChannelParticipantCreator), nameof(TChannelParticipantCreator))]
[JsonDerivedType(typeof(TChannelParticipantAdmin), nameof(TChannelParticipantAdmin))]
[JsonDerivedType(typeof(TChannelParticipantBanned), nameof(TChannelParticipantBanned))]
[JsonDerivedType(typeof(TChannelParticipantLeft), nameof(TChannelParticipantLeft))]
public interface IChannelParticipant : IObject
{

}
