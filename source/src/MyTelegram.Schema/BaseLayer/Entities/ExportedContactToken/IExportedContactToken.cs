// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes a <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a>.
/// See <a href="https://corefork.telegram.org/constructor/ExportedContactToken" />
///</summary>
public interface IExportedContactToken : IObject
{
    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/links#temporary-profile-links">temporary profile link</a>.
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Its expiry date
    ///</summary>
    int Expires { get; set; }
}
