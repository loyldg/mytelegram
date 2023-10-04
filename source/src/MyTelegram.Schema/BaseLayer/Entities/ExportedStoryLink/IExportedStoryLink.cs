// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/ExportedStoryLink" />
///</summary>
public interface IExportedStoryLink : IObject
{
    ///<summary>
    /// &nbsp;
    ///</summary>
    string Link { get; set; }
}
