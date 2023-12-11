// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/help.PeerColorSet" />
///</summary>
[JsonDerivedType(typeof(TPeerColorSet), nameof(TPeerColorSet))]
[JsonDerivedType(typeof(TPeerColorProfileSet), nameof(TPeerColorProfileSet))]
public interface IPeerColorSet : IObject
{

}
