// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about when a certain participant has read a message
/// See <a href="https://corefork.telegram.org/constructor/ReadParticipantDate" />
///</summary>
[JsonDerivedType(typeof(TReadParticipantDate), nameof(TReadParticipantDate))]
public interface IReadParticipantDate : IObject
{
    ///<summary>
    /// User ID
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// When the user read the message
    ///</summary>
    int Date { get; set; }
}
