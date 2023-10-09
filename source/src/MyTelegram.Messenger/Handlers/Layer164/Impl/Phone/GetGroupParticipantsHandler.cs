// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get group call participants
/// See <a href="https://corefork.telegram.org/method/phone.getGroupParticipants" />
///</summary>
internal sealed class GetGroupParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupParticipants, MyTelegram.Schema.Phone.IGroupParticipants>,
    Phone.IGetGroupParticipantsHandler
{
    protected override Task<MyTelegram.Schema.Phone.IGroupParticipants> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupParticipants obj)
    {
        throw new NotImplementedException();
    }
}
