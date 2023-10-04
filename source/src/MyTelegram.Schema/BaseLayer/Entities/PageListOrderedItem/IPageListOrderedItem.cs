// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an <a href="https://instantview.telegram.org/">instant view ordered list</a>
/// See <a href="https://corefork.telegram.org/constructor/PageListOrderedItem" />
///</summary>
public interface IPageListOrderedItem : IObject
{
    ///<summary>
    /// Number of element within ordered list
    ///</summary>
    string Num { get; set; }
}
