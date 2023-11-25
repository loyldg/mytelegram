// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an <a href="https://instantview.telegram.org/">instant view ordered list</a>
/// See <a href="https://corefork.telegram.org/constructor/PageListOrderedItem" />
///</summary>
[JsonDerivedType(typeof(TPageListOrderedItemText), nameof(TPageListOrderedItemText))]
[JsonDerivedType(typeof(TPageListOrderedItemBlocks), nameof(TPageListOrderedItemBlocks))]
public interface IPageListOrderedItem : IObject
{
    ///<summary>
    /// Number of element within ordered list
    ///</summary>
    string Num { get; set; }
}
