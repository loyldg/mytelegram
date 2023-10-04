// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// HTTP link and embed info of channel message
/// See <a href="https://corefork.telegram.org/constructor/ExportedMessageLink" />
///</summary>
public interface IExportedMessageLink : IObject
{
    ///<summary>
    /// URL
    ///</summary>
    string Link { get; set; }

    ///<summary>
    /// Embed code
    ///</summary>
    string Html { get; set; }
}
