// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInlineBotSwitchPM : IObject
{
    string Text { get; set; }
    string StartParam { get; set; }
}
