// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/OutboxReadDate" />
///</summary>
[JsonDerivedType(typeof(TOutboxReadDate), nameof(TOutboxReadDate))]
public interface IOutboxReadDate : IObject
{
    int Date { get; set; }
}
