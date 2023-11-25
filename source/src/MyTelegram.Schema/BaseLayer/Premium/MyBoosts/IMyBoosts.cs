// ReSharper disable All

namespace MyTelegram.Schema.Premium;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/premium.MyBoosts" />
///</summary>
[JsonDerivedType(typeof(TMyBoosts), nameof(TMyBoosts))]
public interface IMyBoosts : IObject
{
    TVector<MyTelegram.Schema.IMyBoost> MyBoosts { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
