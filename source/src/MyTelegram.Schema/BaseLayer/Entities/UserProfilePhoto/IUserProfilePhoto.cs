// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on the user's profile photo.
/// See <a href="https://corefork.telegram.org/constructor/UserProfilePhoto" />
///</summary>
[JsonDerivedType(typeof(TUserProfilePhotoEmpty), nameof(TUserProfilePhotoEmpty))]
[JsonDerivedType(typeof(TUserProfilePhoto), nameof(TUserProfilePhoto))]
public interface IUserProfilePhoto : IObject
{

}
