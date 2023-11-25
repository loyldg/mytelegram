// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Item in block list
/// See <a href="https://corefork.telegram.org/constructor/PageListItem" />
///</summary>
[JsonDerivedType(typeof(TPageListItemText), nameof(TPageListItemText))]
[JsonDerivedType(typeof(TPageListItemBlocks), nameof(TPageListItemBlocks))]
public interface IPageListItem : IObject
{

}
