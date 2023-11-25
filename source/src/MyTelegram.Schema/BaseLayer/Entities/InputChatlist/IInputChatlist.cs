// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a folder
/// See <a href="https://corefork.telegram.org/constructor/InputChatlist" />
///</summary>
[JsonDerivedType(typeof(TInputChatlistDialogFilter), nameof(TInputChatlistDialogFilter))]
public interface IInputChatlist : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders">Folder</a> ID
    ///</summary>
    int FilterId { get; set; }
}
