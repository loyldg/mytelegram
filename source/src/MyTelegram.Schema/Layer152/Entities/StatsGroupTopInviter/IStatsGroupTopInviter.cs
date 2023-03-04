// ReSharper disable All

namespace MyTelegram.Schema;

public interface IStatsGroupTopInviter : IObject
{
    long UserId { get; set; }
    int Invitations { get; set; }
}
