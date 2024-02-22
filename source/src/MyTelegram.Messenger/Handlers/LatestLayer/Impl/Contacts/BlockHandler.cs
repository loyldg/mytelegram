// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Adds the user to the blacklist.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/contacts.block" />
///</summary>
internal sealed class BlockHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestBlock, IBool>,
    Contacts.IBlockHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestBlock obj)
    {
        throw new NotImplementedException();
    }
}
