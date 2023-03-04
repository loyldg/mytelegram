// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPollAnswerVoters : IObject
{
    BitArray Flags { get; set; }
    bool Chosen { get; set; }
    bool Correct { get; set; }
    byte[] Option { get; set; }
    int Voters { get; set; }
}
