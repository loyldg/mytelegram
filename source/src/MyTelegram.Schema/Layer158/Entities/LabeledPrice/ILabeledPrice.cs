// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILabeledPrice : IObject
{
    string Label { get; set; }
    long Amount { get; set; }
}
