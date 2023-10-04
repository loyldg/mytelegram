// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object describes a photo.
/// See <a href="https://corefork.telegram.org/constructor/Photo" />
///</summary>
public interface IPhoto : IObject
{
    ///<summary>
    /// ID
    ///</summary>
    long Id { get; set; }
}
