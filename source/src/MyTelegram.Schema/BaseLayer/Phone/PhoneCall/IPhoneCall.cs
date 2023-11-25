// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Phone call
/// See <a href="https://corefork.telegram.org/constructor/phone.PhoneCall" />
///</summary>
[JsonDerivedType(typeof(TPhoneCall), nameof(TPhoneCall))]
public interface IPhoneCall : IObject
{
    ///<summary>
    /// The VoIP phone call
    /// See <a href="https://corefork.telegram.org/type/PhoneCall" />
    ///</summary>
    MyTelegram.Schema.IPhoneCall PhoneCall { get; set; }

    ///<summary>
    /// VoIP phone call participants
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
