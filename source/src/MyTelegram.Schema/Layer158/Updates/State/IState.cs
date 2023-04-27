// ReSharper disable All

namespace MyTelegram.Schema.Updates;

public interface IState : IObject
{
    int Pts { get; set; }
    int Qts { get; set; }
    int Date { get; set; }
    int Seq { get; set; }
    int UnreadCount { get; set; }
}
