// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDefaultHistoryTTL : IObject
{
    int Period { get; set; }
}
