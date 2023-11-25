// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Why was the phone call discarded?
/// See <a href="https://corefork.telegram.org/constructor/PhoneCallDiscardReason" />
///</summary>
[JsonDerivedType(typeof(TPhoneCallDiscardReasonMissed), nameof(TPhoneCallDiscardReasonMissed))]
[JsonDerivedType(typeof(TPhoneCallDiscardReasonDisconnect), nameof(TPhoneCallDiscardReasonDisconnect))]
[JsonDerivedType(typeof(TPhoneCallDiscardReasonHangup), nameof(TPhoneCallDiscardReasonHangup))]
[JsonDerivedType(typeof(TPhoneCallDiscardReasonBusy), nameof(TPhoneCallDiscardReasonBusy))]
public interface IPhoneCallDiscardReason : IObject
{

}
