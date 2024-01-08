namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class ConfigConverterLatest : IConfigConverterLatest
{
    public ConfigConverterLatest(IObjectMapper objectMapper)
    {
        ObjectMapper = objectMapper;
    }

    public virtual int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }
    protected IObjectMapper ObjectMapper { get; }

    public virtual IConfig ToConfig(List<DcOption> dcOptions,
        int thisDcId,
        int mediaDcId)
    {
        var options = ObjectMapper.Map<List<DcOption>, List<TDcOption>>(dcOptions);
        var date = DateTime.UtcNow.ToTimestamp();
        var config = new TConfig
        {
            //Flags = new System.Collections.BitArray(new byte[] { 0 }),

            //PhonecallsEnabled = true,
            DefaultP2pContacts = true,
            //PreloadFeaturedStickers = true,
            //IgnorePhoneEntities = false,
            RevokePmInbox = true,
            BlockedMode = false,
            //PfsEnabled = true,
            Date = date,
            //android 30m
            Expires = DateTime.UtcNow.AddMinutes(30).ToTimestamp(),

            //Date = (int)((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds(),
            //Expires =(int)((DateTimeOffset)DateTime.Now.AddMinutes(20)).ToUnixTimeSeconds(),

            TestMode = false,
            ThisDc = thisDcId,
            ForceTryIpv6 = true,
            //DcOptions = new TVector<IDcOption>(),
            DcOptions = new TVector<IDcOption>(options),
            DcTxtDomainName = "apv2.stel.com",
            ChatSizeMax = 200, // _options.ChatSizeMax, //200
            MegagroupSizeMax = 200000, // _options.MegagroupSizeMax, //200000
            ForwardedCountMax = 100, // _options.ForwardedCountMax, //100
            OnlineUpdatePeriodMs = 210000,
            OfflineBlurTimeoutMs = 5000,
            OfflineIdleTimeoutMs = 30000,
            OnlineCloudTimeoutMs = 300000,
            NotifyCloudDelayMs = 30000,
            NotifyDefaultDelayMs = 1500,
            PushChatPeriodMs = 60000,
            PushChatLimit = 2,
            //SavedGifsLimit = 200,
            EditTimeLimit = 172800, // _options.EditTimeLimit, //172800
            RevokeTimeLimit = 2147483647,
            RevokePmTimeLimit = 2147483647,
            RatingEDecay = 2419200,
            StickersRecentLimit = 200,
            //StickersFavedLimit = 5,
            ChannelsReadMediaPeriod = 604800,
            //TmpSessions =
            //PinnedDialogsCountMax = 5, // _options.PinnedDialogsCountMax, //5
            //PinnedInfolderCountMax = 100,
            CallReceiveTimeoutMs = 20000,
            CallRingTimeoutMs = 90000,
            CallConnectTimeoutMs = 30000,
            CallPacketTimeoutMs = 10000,
            MeUrlPrefix = "https://t.mytelegram.top/",
            //AutoupdateUrlPrefix = "",
            GifSearchUsername = "gif",
            VenueSearchUsername = "foursquare",
            ImgSearchUsername = "bing",
            StaticMapsProvider = "telegram,google:AIzaSyB0y3zA4LbA04ZPaHKsr_Xt5ZQWbMftj8I",
            CaptionLengthMax = 1024, // _options.CaptionLengthMax, //1024
            MessageLengthMax = 4096, // _options.MessageLengthMax, //4096
            WebfileDcId = mediaDcId, // _dataCenterHelper.GetMediaDcId(),
            SuggestedLangCode = "en",
            LangPackVersion = 0,
            BaseLangPackVersion = 0,
            ReactionsDefault = new TReactionEmoji
            {
                Emoticon = "👍"
            }
        };

        return config;
    }
}