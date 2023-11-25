// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Hash of an uploaded file, to be checked for validity after download
/// See <a href="https://corefork.telegram.org/constructor/FileHash" />
///</summary>
[JsonDerivedType(typeof(TFileHash), nameof(TFileHash))]
public interface IFileHash : IObject
{
    ///<summary>
    /// Offset from where to start computing SHA-256 hash
    ///</summary>
    long Offset { get; set; }

    ///<summary>
    /// Length
    ///</summary>
    int Limit { get; set; }

    ///<summary>
    /// SHA-256 Hash of file chunk, to be checked for validity after download
    ///</summary>
    byte[] Hash { get; set; }
}
