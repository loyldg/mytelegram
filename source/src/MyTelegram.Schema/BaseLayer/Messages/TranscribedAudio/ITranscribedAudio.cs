// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// <a href="https://corefork.telegram.org/api/transcribe">Transcribed text</a> from a voice message
/// See <a href="https://corefork.telegram.org/constructor/messages.TranscribedAudio" />
///</summary>
public interface ITranscribedAudio : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the transcription is partial because audio transcription is still in progress, if set the user may receive further <a href="https://corefork.telegram.org/constructor/updateTranscribedAudio">updateTranscribedAudio</a> updates with the updated transcription.
    ///</summary>
    bool Pending { get; set; }

    ///<summary>
    /// Transcription ID
    ///</summary>
    long TranscriptionId { get; set; }

    ///<summary>
    /// Transcripted text
    ///</summary>
    string Text { get; set; }
}
