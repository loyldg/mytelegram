// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Location of remote file
/// See <a href="https://corefork.telegram.org/constructor/InputWebFileLocation" />
///</summary>
[JsonDerivedType(typeof(TInputWebFileLocation), nameof(TInputWebFileLocation))]
[JsonDerivedType(typeof(TInputWebFileGeoPointLocation), nameof(TInputWebFileGeoPointLocation))]
[JsonDerivedType(typeof(TInputWebFileAudioAlbumThumbLocation), nameof(TInputWebFileAudioAlbumThumbLocation))]
public interface IInputWebFileLocation : IObject
{

}
