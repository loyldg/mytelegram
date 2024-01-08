// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/stories#story-links">story deep link</a>
/// See <a href="https://corefork.telegram.org/constructor/ExportedStoryLink" />
///</summary>
[JsonDerivedType(typeof(TExportedStoryLink), nameof(TExportedStoryLink))]
public interface IExportedStoryLink : IObject
{
    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/stories#story-links">story deep link</a>.
    ///</summary>
    string Link { get; set; }
}
