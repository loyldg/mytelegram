// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// <a href="https://corefork.telegram.org/api/transcribe">Transcribe voice message</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 400 TRANSCRIPTION_FAILED Audio transcription failed.
/// See <a href="https://corefork.telegram.org/method/messages.transcribeAudio" />
///</summary>
internal sealed class TranscribeAudioHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranscribeAudio, MyTelegram.Schema.Messages.ITranscribedAudio>,
    Messages.ITranscribeAudioHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranscribedAudio> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTranscribeAudio obj)
    {
        throw new NotImplementedException();
    }
}
