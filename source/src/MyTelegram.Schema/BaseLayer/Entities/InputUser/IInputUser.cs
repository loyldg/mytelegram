// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a user for subsequent interaction.
/// See <a href="https://corefork.telegram.org/constructor/InputUser" />
///</summary>
[JsonDerivedType(typeof(TInputUserEmpty), nameof(TInputUserEmpty))]
[JsonDerivedType(typeof(TInputUserSelf), nameof(TInputUserSelf))]
[JsonDerivedType(typeof(TInputUser), nameof(TInputUser))]
[JsonDerivedType(typeof(TInputUserFromMessage), nameof(TInputUserFromMessage))]
public interface IInputUser : IObject
{

}
