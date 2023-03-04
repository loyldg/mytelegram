// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWebViewResult : IObject
{
    long QueryId { get; set; }
    string Url { get; set; }
}
