// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Exported authorization
/// See <a href="https://corefork.telegram.org/constructor/auth.ExportedAuthorization" />
///</summary>
[JsonDerivedType(typeof(TExportedAuthorization), nameof(TExportedAuthorization))]
public interface IExportedAuthorization : IObject
{
    ///<summary>
    /// current user identifier
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// authorizes key
    ///</summary>
    byte[] Bytes { get; set; }
}
