// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPollResults : IObject
{
    BitArray Flags { get; set; }
    bool Min { get; set; }
    TVector<MyTelegram.Schema.IPollAnswerVoters>? Results { get; set; }
    int? TotalVoters { get; set; }
    TVector<long>? RecentVoters { get; set; }
    string? Solution { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? SolutionEntities { get; set; }

}
