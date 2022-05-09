using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetInlineGameHighScoresHandler : RpcResultObjectHandler<RequestGetInlineGameHighScores, IHighScores>,
    IGetInlineGameHighScoresHandler
{
    protected override Task<IHighScores> HandleCoreAsync(IRequestInput input,
        RequestGetInlineGameHighScores obj)
    {
        throw new NotImplementedException();
    }
}
