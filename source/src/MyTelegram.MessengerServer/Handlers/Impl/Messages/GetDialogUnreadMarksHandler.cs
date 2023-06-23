using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetDialogUnreadMarksHandler : RpcResultObjectHandler<RequestGetDialogUnreadMarks, TVector<IDialogPeer>>,
    IGetDialogUnreadMarksHandler
{
    protected override Task<TVector<IDialogPeer>> HandleCoreAsync(IRequestInput input,
        RequestGetDialogUnreadMarks obj)
    {
        throw new NotImplementedException();
    }
}