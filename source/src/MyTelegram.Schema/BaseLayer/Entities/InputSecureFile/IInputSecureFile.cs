// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure <a href="https://corefork.telegram.org/passport">passport</a> file, for more info <a href="https://corefork.telegram.org/passport/encryption#inputsecurefile">see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/constructor/InputSecureFile" />
///</summary>
[JsonDerivedType(typeof(TInputSecureFileUploaded), nameof(TInputSecureFileUploaded))]
[JsonDerivedType(typeof(TInputSecureFile), nameof(TInputSecureFile))]
public interface IInputSecureFile : IObject
{
    ///<summary>
    /// Secure file ID
    ///</summary>
    long Id { get; set; }
}
