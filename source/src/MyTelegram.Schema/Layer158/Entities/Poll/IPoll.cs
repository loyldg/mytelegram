// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPoll : IObject
{
    long Id { get; set; }
    BitArray Flags { get; set; }
    bool Closed { get; set; }
    bool PublicVoters { get; set; }
    bool MultipleChoice { get; set; }
    bool Quiz { get; set; }
    string Question { get; set; }
    TVector<Schema.IPollAnswer> Answers { get; set; }
    int? ClosePeriod { get; set; }
    int? CloseDate { get; set; }
}
