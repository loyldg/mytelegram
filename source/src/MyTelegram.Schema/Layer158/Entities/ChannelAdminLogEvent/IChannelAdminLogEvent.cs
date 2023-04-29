// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChannelAdminLogEvent : IObject
{
    long Id { get; set; }
    int Date { get; set; }
    long UserId { get; set; }
    Schema.IChannelAdminLogEventAction Action { get; set; }
}
