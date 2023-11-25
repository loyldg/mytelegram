// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Rich text
/// See <a href="https://corefork.telegram.org/constructor/RichText" />
///</summary>
[JsonDerivedType(typeof(TTextEmpty), nameof(TTextEmpty))]
[JsonDerivedType(typeof(TTextPlain), nameof(TTextPlain))]
[JsonDerivedType(typeof(TTextBold), nameof(TTextBold))]
[JsonDerivedType(typeof(TTextItalic), nameof(TTextItalic))]
[JsonDerivedType(typeof(TTextUnderline), nameof(TTextUnderline))]
[JsonDerivedType(typeof(TTextStrike), nameof(TTextStrike))]
[JsonDerivedType(typeof(TTextFixed), nameof(TTextFixed))]
[JsonDerivedType(typeof(TTextUrl), nameof(TTextUrl))]
[JsonDerivedType(typeof(TTextEmail), nameof(TTextEmail))]
[JsonDerivedType(typeof(TTextConcat), nameof(TTextConcat))]
[JsonDerivedType(typeof(TTextSubscript), nameof(TTextSubscript))]
[JsonDerivedType(typeof(TTextSuperscript), nameof(TTextSuperscript))]
[JsonDerivedType(typeof(TTextMarked), nameof(TTextMarked))]
[JsonDerivedType(typeof(TTextPhone), nameof(TTextPhone))]
[JsonDerivedType(typeof(TTextImage), nameof(TTextImage))]
[JsonDerivedType(typeof(TTextAnchor), nameof(TTextAnchor))]
public interface IRichText : IObject
{

}
