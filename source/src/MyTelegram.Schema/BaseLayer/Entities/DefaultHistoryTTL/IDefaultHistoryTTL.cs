// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about the default value of the Time-To-Live setting, applied to all new chats.
/// See <a href="https://corefork.telegram.org/constructor/DefaultHistoryTTL" />
///</summary>
[JsonDerivedType(typeof(TDefaultHistoryTTL), nameof(TDefaultHistoryTTL))]
public interface IDefaultHistoryTTL : IObject
{
    ///<summary>
    /// Time-To-Live setting applied to all new chats.
    ///</summary>
    int Period { get; set; }
}
