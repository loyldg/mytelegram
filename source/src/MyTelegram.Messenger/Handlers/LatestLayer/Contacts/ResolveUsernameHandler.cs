namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Resolve a @username to get peer info
/// Possible errors
/// Code Type Description
/// 400 CONNECTION_LAYER_INVALID Layer invalid.
/// 400 STARREF_EXPIRED The specified referral link is invalid.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_OCCUPIED The provided username is not occupied.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.resolveUsername"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ResolveUsernameHandler(IQueryProcessor queryProcessor, //ILayeredService<IChatConverter> layeredChatService,
 //ILayeredService<IUserConverter> layeredUserService,
IChatConverterService chatConverterService, IUserConverterService userConverterService, IChannelAppService channelAppService, IPhotoAppService photoAppService) : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolveUsername, MyTelegram.Schema.Contacts.IResolvedPeer>
{
    protected override async Task<IResolvedPeer> HandleCoreAsync(IRequestInput input, RequestResolveUsername obj)
    {
        if (!string.IsNullOrEmpty(obj.Username))
        {
            var userNameReadModel = await queryProcessor.ProcessAsync(new GetUserNameByNameQuery(obj.Username), default);
            if (userNameReadModel != null)
            {
                switch (userNameReadModel.PeerType)
                {
                    case PeerType.User:
                        var user = await userConverterService.GetUserAsync(input, userNameReadModel.PeerId, false, false, input.Layer);
                        return new TResolvedPeer
                        {
                            Chats = [],
                            Peer = new TPeerUser
                            {
                                UserId = userNameReadModel.PeerId
                            },
                            Users = new TVector<IUser>(user)
                        };
                    case PeerType.Channel:
                    {
                        var channelReadModel = await channelAppService.GetAsync(userNameReadModel.PeerId);
                        if (channelReadModel != null)
                        {
                            var photoReadModel = await photoAppService.GetAsync(channelReadModel.PhotoId);
                            var channelMemberReadModel = await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(channelReadModel.ChannelId, input.UserId));
                            return new TResolvedPeer
                            {
                                Chats = new TVector<IChat>(chatConverterService.ToChannel(input, channelReadModel, photoReadModel, channelMemberReadModel, channelMemberReadModel?.Left ?? true, input.Layer)),
                                Peer = new TPeerChannel
                                {
                                    ChannelId = userNameReadModel.PeerId
                                },
                                Users = []
                            };
                        }
                    }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        RpcErrors.RpcErrors400.UsernameNotOccupied.ThrowRpcError();
        return null !;
    }
}