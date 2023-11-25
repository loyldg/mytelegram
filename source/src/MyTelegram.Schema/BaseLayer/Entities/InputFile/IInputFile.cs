// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a file uploaded by the client.
/// See <a href="https://corefork.telegram.org/constructor/InputFile" />
///</summary>
[JsonDerivedType(typeof(TInputFile), nameof(TInputFile))]
[JsonDerivedType(typeof(TInputFileBig), nameof(TInputFileBig))]
public interface IInputFile : IObject
{
    ///<summary>
    /// Random file id, created by the client
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Number of parts saved
    ///</summary>
    int Parts { get; set; }

    ///<summary>
    /// Full file name
    ///</summary>
    string Name { get; set; }
}
