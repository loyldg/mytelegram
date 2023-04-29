// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class TranscribeAudioHandler : RpcResultObjectHandler<RequestTranscribeAudio, ITranscribedAudio>,
    Messages.ITranscribeAudioHandler
{
    protected override Task<ITranscribedAudio> HandleCoreAsync(IRequestInput input,
        RequestTranscribeAudio obj)
    {
        throw new NotImplementedException();
    }
}
