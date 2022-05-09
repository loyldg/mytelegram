// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IMessageStats : IObject
{
    MyTelegram.Schema.IStatsGraph ViewsGraph { get; set; }

}
