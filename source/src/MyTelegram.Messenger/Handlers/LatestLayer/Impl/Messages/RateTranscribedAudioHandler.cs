// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Rate <a href="https://corefork.telegram.org/api/transcribe">transcribed voice message</a>
/// See <a href="https://corefork.telegram.org/method/messages.rateTranscribedAudio" />
///</summary>
internal sealed class RateTranscribedAudioHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRateTranscribedAudio, IBool>,
    Messages.IRateTranscribedAudioHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRateTranscribedAudio obj)
    {
        throw new NotImplementedException();
    }
}
