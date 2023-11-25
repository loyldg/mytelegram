// ReSharper disable All

namespace MyTelegram.Schema.Photos;

///<summary>
/// Photo with auxiliary data.
/// See <a href="https://corefork.telegram.org/constructor/photos.Photo" />
///</summary>
[JsonDerivedType(typeof(TPhoto), nameof(TPhoto))]
public interface IPhoto : IObject
{
    ///<summary>
    /// Photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto Photo { get; set; }

    ///<summary>
    /// Users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
