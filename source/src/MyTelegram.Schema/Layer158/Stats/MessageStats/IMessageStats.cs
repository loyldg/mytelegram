// ReSharper disable All

namespace MyTelegram.Schema.Stats;

public interface IMessageStats : IObject
{
    Schema.IStatsGraph ViewsGraph { get; set; }
}
