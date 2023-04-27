// ReSharper disable All

namespace MyTelegram.Schema;

public interface IRestrictionReason : IObject
{
    string Platform { get; set; }
    string Reason { get; set; }
    string Text { get; set; }
}
