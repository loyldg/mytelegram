using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetInviteTextHandler : RpcResultObjectHandler<RequestGetInviteText, IInviteText>,
    IGetInviteTextHandler, IProcessedHandler
{
    protected override Task<IInviteText> HandleCoreAsync(IRequestInput input,
        RequestGetInviteText obj)
    {
        IInviteText r = new TInviteText { Message = @"{0} invite you to use telegram." };

        return Task.FromResult(r);
    }
}