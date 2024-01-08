// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/SavedDialog" />
///</summary>
[JsonDerivedType(typeof(TSavedDialog), nameof(TSavedDialog))]
public interface ISavedDialog : IObject
{
    BitArray Flags { get; set; }
    bool Pinned { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
    int TopMessage { get; set; }
}
