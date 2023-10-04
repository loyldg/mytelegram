// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Secure Telegram Passport value
/// See <a href="https://corefork.telegram.org/constructor/SecureValue" />
///</summary>
public interface ISecureValue : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Secure <a href="https://corefork.telegram.org/passport">passport</a> value type
    /// See <a href="https://corefork.telegram.org/type/SecureValueType" />
    ///</summary>
    MyTelegram.Schema.ISecureValueType Type { get; set; }

    ///<summary>
    /// Encrypted <a href="https://corefork.telegram.org/passport">Telegram Passport</a> element data
    /// See <a href="https://corefork.telegram.org/type/SecureData" />
    ///</summary>
    MyTelegram.Schema.ISecureData? Data { get; set; }

    ///<summary>
    /// Encrypted <a href="https://corefork.telegram.org/passport">passport</a> file with the front side of the document
    /// See <a href="https://corefork.telegram.org/type/SecureFile" />
    ///</summary>
    MyTelegram.Schema.ISecureFile? FrontSide { get; set; }

    ///<summary>
    /// Encrypted <a href="https://corefork.telegram.org/passport">passport</a> file with the reverse side of the document
    /// See <a href="https://corefork.telegram.org/type/SecureFile" />
    ///</summary>
    MyTelegram.Schema.ISecureFile? ReverseSide { get; set; }

    ///<summary>
    /// Encrypted <a href="https://corefork.telegram.org/passport">passport</a> file with a selfie of the user holding the document
    /// See <a href="https://corefork.telegram.org/type/SecureFile" />
    ///</summary>
    MyTelegram.Schema.ISecureFile? Selfie { get; set; }

    ///<summary>
    /// Array of encrypted <a href="https://corefork.telegram.org/passport">passport</a> files with translated versions of the provided documents
    /// See <a href="https://corefork.telegram.org/type/SecureFile" />
    ///</summary>
    TVector<MyTelegram.Schema.ISecureFile>? Translation { get; set; }

    ///<summary>
    /// Array of encrypted <a href="https://corefork.telegram.org/passport">passport</a> files with photos the of the documents
    /// See <a href="https://corefork.telegram.org/type/SecureFile" />
    ///</summary>
    TVector<MyTelegram.Schema.ISecureFile>? Files { get; set; }

    ///<summary>
    /// Plaintext verified <a href="https://corefork.telegram.org/passport">passport</a> data
    /// See <a href="https://corefork.telegram.org/type/SecurePlainData" />
    ///</summary>
    MyTelegram.Schema.ISecurePlainData? PlainData { get; set; }

    ///<summary>
    /// Data hash
    ///</summary>
    byte[] Hash { get; set; }
}
