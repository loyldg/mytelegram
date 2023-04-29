using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class ResolveUsernameHandler : RpcResultObjectHandler<RequestResolveUsername, IResolvedPeer>,
    IResolveUsernameHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlUserConverter _userConverter;

    public ResolveUsernameHandler(IQueryProcessor queryProcessor,
        ITlChatConverter chatConverter,
        ITlUserConverter userConverter)
    {
        _queryProcessor = queryProcessor;
        _chatConverter = chatConverter;
        _userConverter = userConverter;
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
                            return new TResolvedPeer
                            {
                                Chats = new TVector<IChat>(),
                                Peer = new TPeerUser { UserId = userNameReadModel.PeerId },
                                Users = new TVector<IUser>(_userConverter.ToUser(userReadModel,
                                    input.UserId))
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
                                return new TResolvedPeer
                                {
                                    Chats = new TVector<IChat>(_chatConverter.ToChat(chatReadModel, input.UserId)),
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
                                return new TResolvedPeer
                                {
                                    Chats =
                                        new TVector<IChat>(_chatConverter.ToChannel(channelReadModel,
                                            null,
                                            input.UserId, false)),
                                    Peer = new TPeerChat { ChatId = userNameReadModel.PeerId },
                                    Users = new TVector<IUser>()
                                };
                            }
                        }
                        break;
                    //case PeerType.EncryptionChat:
                    //    break;
                    default:
                        //throw new ArgumentOutOfRangeException();
                        throw new NotSupportedException($"Not supported peer:{userNameReadModel.PeerType}");
                }
            }
        }

        throw new BadRequestException("USERNAME_NOT_OCCUPIED");
        //return new TResolvedPeer
        //{
        //    Chats = new TVector<IChat>(),
        //    Users = new TVector<IUser>(),
        //    Peer = new TPeerUser { UserId = 0 }
        //};
    }
}
