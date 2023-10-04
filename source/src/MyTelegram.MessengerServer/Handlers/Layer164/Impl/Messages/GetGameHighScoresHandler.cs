using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetGameHighScoresHandler : RpcResultObjectHandler<RequestGetGameHighScores, IHighScores>,
    IGetGameHighScoresHandler
{
    protected override Task<IHighScores> HandleCoreAsync(IRequestInput input,
        RequestGetGameHighScores obj)
    {
        throw new NotImplementedException();
    }
}