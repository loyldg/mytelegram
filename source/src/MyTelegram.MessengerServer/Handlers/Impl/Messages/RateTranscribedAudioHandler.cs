// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class RateTranscribedAudioHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRateTranscribedAudio, IBool>,
    Messages.IRateTranscribedAudioHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRateTranscribedAudio obj)
    {
        throw new NotImplementedException();
    }
}
