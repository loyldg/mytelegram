// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Location of a certain size of a picture
/// See <a href="https://corefork.telegram.org/constructor/PhotoSize" />
///</summary>
[JsonDerivedType(typeof(TPhotoSizeEmpty), nameof(TPhotoSizeEmpty))]
[JsonDerivedType(typeof(TPhotoSize), nameof(TPhotoSize))]
[JsonDerivedType(typeof(TPhotoCachedSize), nameof(TPhotoCachedSize))]
[JsonDerivedType(typeof(TPhotoStrippedSize), nameof(TPhotoStrippedSize))]
[JsonDerivedType(typeof(TPhotoSizeProgressive), nameof(TPhotoSizeProgressive))]
[JsonDerivedType(typeof(TPhotoPathSize), nameof(TPhotoPathSize))]
public interface IPhotoSize : IObject
{
    ///<summary>
    /// Always <code>j</code>
    ///</summary>
    string Type { get; set; }
}
