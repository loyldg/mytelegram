// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a group call
/// See <a href="https://corefork.telegram.org/constructor/InputGroupCall" />
///</summary>
[JsonDerivedType(typeof(TInputGroupCall), nameof(TInputGroupCall))]
public interface IInputGroupCall : IObject
{
    ///<summary>
    /// Group call ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Group call access hash
    ///</summary>
    long AccessHash { get; set; }
}
