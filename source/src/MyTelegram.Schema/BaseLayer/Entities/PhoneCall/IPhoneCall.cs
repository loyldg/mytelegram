// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone call
/// See <a href="https://corefork.telegram.org/constructor/PhoneCall" />
///</summary>
[JsonDerivedType(typeof(TPhoneCallEmpty), nameof(TPhoneCallEmpty))]
[JsonDerivedType(typeof(TPhoneCallWaiting), nameof(TPhoneCallWaiting))]
[JsonDerivedType(typeof(TPhoneCallRequested), nameof(TPhoneCallRequested))]
[JsonDerivedType(typeof(TPhoneCallAccepted), nameof(TPhoneCallAccepted))]
[JsonDerivedType(typeof(TPhoneCall), nameof(TPhoneCall))]
[JsonDerivedType(typeof(TPhoneCallDiscarded), nameof(TPhoneCallDiscarded))]
public interface IPhoneCall : IObject
{
    ///<summary>
    /// Call ID
    ///</summary>
    long Id { get; set; }
}
