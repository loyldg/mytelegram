namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Rate <a href="https://corefork.telegram.org/api/transcribe">transcribed voice message</a>
/// See <a href="https://corefork.telegram.org/method/messages.rateTranscribedAudio" />
///</summary>
internal sealed class RateTranscribedAudioHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRateTranscribedAudio, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRateTranscribedAudio obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
