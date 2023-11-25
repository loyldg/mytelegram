// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Channel participant
/// See <a href="https://corefork.telegram.org/constructor/channels.ChannelParticipant" />
///</summary>
[JsonDerivedType(typeof(TChannelParticipant), nameof(TChannelParticipant))]
public interface IChannelParticipant : IObject
{
    ///<summary>
    /// The channel participant
    /// See <a href="https://corefork.telegram.org/type/ChannelParticipant" />
    ///</summary>
    MyTelegram.Schema.IChannelParticipant Participant { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
