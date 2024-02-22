// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Resolve a @username to get peer info
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONNECTION_LAYER_INVALID Layer invalid.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_OCCUPIED The provided username is not occupied.
/// See <a href="https://corefork.telegram.org/method/contacts.resolveUsername" />
///</summary>
internal sealed class ResolveUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolveUsername, MyTelegram.Schema.Contacts.IResolvedPeer>,
    Contacts.IResolveUsernameHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;

    public ResolveUsernameHandler(IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredChatService = layeredChatService;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<IResolvedPeer> HandleCoreAsync(IRequestInput input,
        RequestResolveUsername obj)
    {
        //Console.WriteLine($"RequestResolveUsername:{obj.Username}");
        if (!string.IsNullOrEmpty(obj.Username))
        {
            var userNameReadModel = await _queryProcessor
                .ProcessAsync(new GetUserNameByNameQuery(obj.Username), default)
         ;
            if (userNameReadModel != null)
            {
                switch (userNameReadModel.PeerType)
                {
                    case PeerType.User:
                        var userReadModel = await _queryProcessor
                            .ProcessAsync(new GetUserByIdQuery(userNameReadModel.PeerId), default)
                     ;
                        if (userReadModel != null)
                        {
                            var contactReadModel =
                                await _queryProcessor.ProcessAsync(
                                    new GetContactQuery(input.UserId, userReadModel.UserId), default);
                            var photos = await _photoAppService.GetPhotosAsync(userReadModel, contactReadModel);
                            var privacies = await _privacyAppService.GetPrivacyListAsync(userReadModel!.UserId);

                            return new TResolvedPeer
                            {
                                Chats = new TVector<IChat>(),
                                Peer = new TPeerUser { UserId = userNameReadModel.PeerId },
                                Users = new TVector<IUser>(_layeredUserService.GetConverter(input.Layer).ToUser(
                                    input.UserId,
                                    userReadModel,
                                    photos,
                                    contactReadModel,
                                    privacies))
                            };
                        }

                        break;
                    case PeerType.Chat:
                        {
                            var chatReadModel = await _queryProcessor
                                .ProcessAsync(new GetChatByChatIdQuery(userNameReadModel.PeerId), default)
                         ;
                            if (chatReadModel != null)
                            {
                                var photoReadModel = await _photoAppService.GetPhotoAsync(chatReadModel.PhotoId);
                                return new TResolvedPeer
                                {
                                    Chats = new TVector<IChat>(_layeredChatService.GetConverter(input.Layer).ToChat(input.UserId, chatReadModel, photoReadModel)),
                                    Peer = new TPeerChat { ChatId = userNameReadModel.PeerId },
                                    Users = new TVector<IUser>()
                                };
                            }
                        }
                        break;
                    case PeerType.Channel:
                        {
                            var channelReadModel = await _queryProcessor
                                .ProcessAsync(new GetChannelByIdQuery(userNameReadModel.PeerId), default)
                         ;
                            if (channelReadModel != null)
                            {
                                var photoReadModel = await _photoAppService.GetPhotoAsync(channelReadModel.PhotoId);

                                return new TResolvedPeer
                                {
                                    Chats =
                                        new TVector<IChat>(_layeredChatService.GetConverter(input.Layer).ToChannel(
                                            input.UserId,
                                            channelReadModel,
                                            photoReadModel,
                                            null, false)),
                                    Peer = new TPeerChat { ChatId = userNameReadModel.PeerId },
                                    Users = new TVector<IUser>()
                                };
                            }
                        }
                        break;
                    //case PeerType.EncryptionChat:
                    //    break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        RpcErrors.RpcErrors400.UsernameNotOccupied.ThrowRpcError();
        return null!;
        //throw new BadRequestException("USERNAME_NOT_OCCUPIED");
        //return new TResolvedPeer
        //{
        //    Chats = new TVector<IChat>(),
        //    Users = new TVector<IUser>(),
        //    Peer = new TPeerUser { UserId = 0 }
        //};
    }
}
