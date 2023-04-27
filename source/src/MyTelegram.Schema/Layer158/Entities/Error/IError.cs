// ReSharper disable All

namespace MyTelegram.Schema;

public interface IError : IObject
{
    int Code { get; set; }
    string Text { get; set; }
}
