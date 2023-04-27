// ReSharper disable All

namespace MyTelegram.Schema;

public interface IReadParticipantDate : IObject
{
    long UserId { get; set; }
    int Date { get; set; }
}
