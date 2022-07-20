// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ITranscribedAudio : IObject
{
    BitArray Flags { get; set; }
    bool Pending { get; set; }
    long TranscriptionId { get; set; }
    string Text { get; set; }

}
