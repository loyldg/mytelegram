// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Stop getting notifications about <a href="https://corefork.telegram.org/api/threads">thread replies</a> of a certain user in <code>@replies</code>
/// See <a href="https://corefork.telegram.org/method/contacts.blockFromReplies" />
///</summary>
internal sealed class BlockFromRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestBlockFromReplies, MyTelegram.Schema.IUpdates>,
    Contacts.IBlockFromRepliesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestBlockFromReplies obj)
    {
        throw new NotImplementedException();
    }
}
