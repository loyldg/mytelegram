// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class RateTranscribedAudioHandler : RpcResultObjectHandler<RequestRateTranscribedAudio, IBool>,
    Messages.IRateTranscribedAudioHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestRateTranscribedAudio obj)
    {
        throw new NotImplementedException();
    }
}
