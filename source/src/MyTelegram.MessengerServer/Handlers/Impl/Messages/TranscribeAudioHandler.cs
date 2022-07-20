// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class TranscribeAudioHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranscribeAudio, MyTelegram.Schema.Messages.ITranscribedAudio>,
    Messages.ITranscribeAudioHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranscribedAudio> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestTranscribeAudio obj)
    {
        throw new NotImplementedException();
    }
}
