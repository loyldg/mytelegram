// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Location of a certain size of a picture
/// See <a href="https://corefork.telegram.org/constructor/PhotoSize" />
///</summary>
public interface IPhotoSize : IObject
{
    ///<summary>
    /// Always <code>j</code>
    ///</summary>
    string Type { get; set; }
}
