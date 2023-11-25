// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Page caption
/// See <a href="https://corefork.telegram.org/constructor/PageCaption" />
///</summary>
[JsonDerivedType(typeof(TPageCaption), nameof(TPageCaption))]
public interface IPageCaption : IObject
{
    ///<summary>
    /// Caption
    /// See <a href="https://corefork.telegram.org/type/RichText" />
    ///</summary>
    MyTelegram.Schema.IRichText Text { get; set; }

    ///<summary>
    /// Credits
    /// See <a href="https://corefork.telegram.org/type/RichText" />
    ///</summary>
    MyTelegram.Schema.IRichText Credit { get; set; }
}
