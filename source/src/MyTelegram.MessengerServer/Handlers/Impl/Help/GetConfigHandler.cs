using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetConfigHandler : RpcResultObjectHandler<RequestGetConfig, IConfig>,
    IGetConfigHandler, IProcessedHandler
{
    private readonly IDataCenterHelper _dataCenterHelper;

    //private readonly IOptions<MyTelegramOptions> _options;
    private readonly IObjectMapper _objectMapper;
    private readonly MyTelegramMessengerServerOptions _options;

    public GetConfigHandler(IOptions<MyTelegramMessengerServerOptions> optionsAccessor,
        IObjectMapper objectMapper,
        IDataCenterHelper dataCenterHelper)
    {
        _options = optionsAccessor.Value;
        _objectMapper = objectMapper;
        _dataCenterHelper = dataCenterHelper;
    }

    protected override Task<IConfig> HandleCoreAsync(IRequestInput input,
        RequestGetConfig obj)
    {
        //todo: desktop and app returns different config

        var dcOptions = _objectMapper.Map<List<DcOption>, List<TDcOption>>(_options.DcOptions);
        IConfig r = new TConfig {
            //Flags = new System.Collections.BitArray(new byte[] { 0 }),

            PhonecallsEnabled = true,
            DefaultP2pContacts = true,
            //PreloadFeaturedStickers = true,
            //IgnorePhoneEntities = false,
            RevokePmInbox = true,
            BlockedMode = false,
            PfsEnabled = true,
            Date = CurrentDate,
            //android 30m
            Expires = DateTime.UtcNow.AddMinutes(30).ToTimestamp(),

            //Date = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds(),
            //Expires =(int)((DateTimeOffset)DateTime.Now.AddMinutes(20)).ToUnixTimeSeconds(),

            TestMode = false,
            ThisDc = _options.ThisDcId,
            //DcOptions = new TVector<IDcOption>(),
            DcOptions = new TVector<IDcOption>(dcOptions),
            DcTxtDomainName = "apv2.stel.com",
            ChatSizeMax = _options.ChatSizeMax, //200
            MegagroupSizeMax = _options.MegagroupSizeMax, //200000
            ForwardedCountMax = _options.ForwardedCountMax, //100
            OnlineUpdatePeriodMs = 210000,
            OfflineBlurTimeoutMs = 5000,
            OfflineIdleTimeoutMs = 30000,
            OnlineCloudTimeoutMs = 300000,
            NotifyCloudDelayMs = 30000,
            NotifyDefaultDelayMs = 1500,
            PushChatPeriodMs = 60000,
            PushChatLimit = 2,
            SavedGifsLimit = 200,
            EditTimeLimit = _options.EditTimeLimit, //172800
            RevokeTimeLimit = 2147483647,
            RevokePmTimeLimit = 2147483647,
            RatingEDecay = 2419200,
            StickersRecentLimit = 200,
            StickersFavedLimit = 5,
            ChannelsReadMediaPeriod = 604800,
            //TmpSessions =
            PinnedDialogsCountMax = _options.PinnedDialogsCountMax, //5
            PinnedInfolderCountMax = 100,
            CallReceiveTimeoutMs = 20000,
            CallRingTimeoutMs = 90000,
            CallConnectTimeoutMs = 30000,
            CallPacketTimeoutMs = 10000,
            MeUrlPrefix = "https://test.me/",
            GifSearchUsername = "gif",
            VenueSearchUsername = "foursquare",
            ImgSearchUsername = "bing",
            StaticMapsProvider = "telegram,google:AIzaSyB0y3zA4LbA04ZPaHKsr_Xt5ZQWbMftj8I",
            CaptionLengthMax = _options.CaptionLengthMax, //1024
            MessageLengthMax = _options.MessageLengthMax, //4096
            WebfileDcId = _dataCenterHelper.GetMediaDcId(),
            SuggestedLangCode = "en",
            LangPackVersion = 0,
            BaseLangPackVersion = 0
        };

        return Task.FromResult(r);
    }
}
