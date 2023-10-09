// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Delete contacts by phone number
/// See <a href="https://corefork.telegram.org/method/contacts.deleteByPhones" />
///</summary>
internal sealed class DeleteByPhonesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestDeleteByPhones, IBool>,
    Contacts.IDeleteByPhonesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestDeleteByPhones obj)
    {
        throw new NotImplementedException();
    }
}
