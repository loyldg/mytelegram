// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Chat partner or group.
/// See <a href="https://corefork.telegram.org/constructor/Peer" />
///</summary>
[JsonDerivedType(typeof(TPeerUser), nameof(TPeerUser))]
[JsonDerivedType(typeof(TPeerChat), nameof(TPeerChat))]
[JsonDerivedType(typeof(TPeerChannel), nameof(TPeerChannel))]
public interface IPeer : IObject
{

}
