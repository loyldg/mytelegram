// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInlineBotWebView : IObject
{
    string Text { get; set; }
    string Url { get; set; }
}
