// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Get localized name for support user
/// See <a href="https://corefork.telegram.org/constructor/help.SupportName" />
///</summary>
[JsonDerivedType(typeof(TSupportName), nameof(TSupportName))]
public interface ISupportName : IObject
{
    ///<summary>
    /// Localized name
    ///</summary>
    string Name { get; set; }
}
