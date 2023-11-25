// ReSharper disable All

namespace MyTelegram.Schema.Photos;

///<summary>
/// Object contains list of photos with auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/photos.Photos" />
///</summary>
[JsonDerivedType(typeof(TPhotos), nameof(TPhotos))]
[JsonDerivedType(typeof(TPhotosSlice), nameof(TPhotosSlice))]
public interface IPhotos : IObject
{
    ///<summary>
    /// List of photos
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    TVector<MyTelegram.Schema.IPhoto> Photos { get; set; }

    ///<summary>
    /// List of mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
