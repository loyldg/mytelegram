namespace MyTelegram.Schema
{
    public partial class TAccountDaysTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Days

            return length;
        }
    }
    public partial class TAiComposeTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += Slug.GetTLLength(); // Slug
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(1)) { length += 8; } // EmojiId
            if (flags.IsBitSet(4)) { length += Prompt.GetTLLength(); } // Prompt
            if (flags.IsBitSet(2)) { length += 4; } // InstallsCount
            if (flags.IsBitSet(3)) { length += 8; } // AuthorId
            if (flags.IsBitSet(5)) { length += ExampleEnglish.GetLength(); } // ExampleEnglish

            return length;
        }
    }
    public partial class TAiComposeToneDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetTLLength(); // Tone
            length += 8; // EmojiId
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TAiComposeToneExample
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += From.GetLength(); // From
            length += To.GetLength(); // To

            return length;
        }
    }
    public partial class TAttachMenuBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // BotId
            length += ShortName.GetTLLength(); // ShortName
            if (flags.IsBitSet(3)) { length += PeerTypes.GetLength(); } // PeerTypes
            length += Icons.GetLength(); // Icons

            return length;
        }
    }
    public partial class TAttachMenuBotIcon
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Name.GetTLLength(); // Name
            length += Icon.GetLength(); // Icon
            if (flags.IsBitSet(0)) { length += Colors.GetLength(); } // Colors

            return length;
        }
    }
    public partial class TAttachMenuBotIconColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name
            length += 4; // Color

            return length;
        }
    }
    public partial class TAttachMenuBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Bots.GetLength(); // Bots
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TAttachMenuBotsBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TAttachMenuBotsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAttachMenuPeerTypeBotPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAttachMenuPeerTypeBroadcast
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAttachMenuPeerTypeChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAttachMenuPeerTypePM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAttachMenuPeerTypeSameBotPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAuctionBidLevel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pos
            length += 8; // Amount
            length += 4; // Date

            return length;
        }
    }
    public partial class TAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash
            length += DeviceModel.GetTLLength(); // DeviceModel
            length += Platform.GetTLLength(); // Platform
            length += SystemVersion.GetTLLength(); // SystemVersion
            length += 4; // ApiId
            length += AppName.GetTLLength(); // AppName
            length += AppVersion.GetTLLength(); // AppVersion
            length += 4; // DateCreated
            length += 4; // DateActive
            length += Ip.GetTLLength(); // Ip
            length += Country.GetTLLength(); // Country
            length += Region.GetTLLength(); // Region

            return length;
        }
    }
    public partial class TAutoDownloadSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // PhotoSizeMax
            length += 8; // VideoSizeMax
            length += 8; // FileSizeMax
            length += 4; // VideoUploadMaxbitrate
            length += 4; // SmallQueueActiveOperationsMax
            length += 4; // LargeQueueActiveOperationsMax

            return length;
        }
    }
    public partial class TAutoSaveException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class TAutoSaveSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += 8; } // VideoMaxSize

            return length;
        }
    }
    public partial class TAvailableEffect
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Emoticon.GetTLLength(); // Emoticon
            if (flags.IsBitSet(0)) { length += 8; } // StaticIconId
            length += 8; // EffectStickerId
            if (flags.IsBitSet(1)) { length += 8; } // EffectAnimationId

            return length;
        }
    }
    public partial class TAvailableReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Reaction.GetTLLength(); // Reaction
            length += Title.GetTLLength(); // Title
            length += StaticIcon.GetLength(); // StaticIcon
            length += AppearAnimation.GetLength(); // AppearAnimation
            length += SelectAnimation.GetLength(); // SelectAnimation
            length += ActivateAnimation.GetLength(); // ActivateAnimation
            length += EffectAnimation.GetLength(); // EffectAnimation
            if (flags.IsBitSet(1)) { length += AroundAnimation.GetLength(); } // AroundAnimation
            if (flags.IsBitSet(1)) { length += CenterIcon.GetLength(); } // CenterIcon

            return length;
        }
    }
    public partial class TBadMsgNotification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BadMsgId
            length += 4; // BadMsgSeqno
            length += 4; // ErrorCode

            return length;
        }
    }
    public partial class TBadServerSalt
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BadMsgId
            length += 4; // BadMsgSeqno
            length += 4; // ErrorCode
            length += 8; // NewServerSalt

            return length;
        }
    }
    public partial class TBankCardOpenUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += Name.GetTLLength(); // Name

            return length;
        }
    }
    public partial class TBaseThemeArctic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBaseThemeClassic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBaseThemeDay
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBaseThemeNight
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBaseThemeTinted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBindAuthKeyInner
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Nonce
            length += 8; // TempAuthKeyId
            length += 8; // PermAuthKeyId
            length += 8; // TempSessionId
            length += 4; // ExpiresAt

            return length;
        }
    }
    public partial class TBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Day
            length += 4; // Month
            if (flags.IsBitSet(0)) { length += 4; } // Year

            return length;
        }
    }
    public partial class TBoolFalse
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBoolTrue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBoost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            if (flags.IsBitSet(0)) { length += 8; } // UserId
            if (flags.IsBitSet(2)) { length += 4; } // GiveawayMsgId
            length += 4; // Date
            length += 4; // Expires
            if (flags.IsBitSet(4)) { length += UsedGiftSlug.GetTLLength(); } // UsedGiftSlug
            if (flags.IsBitSet(5)) { length += 4; } // Multiplier
            if (flags.IsBitSet(6)) { length += 8; } // Stars

            return length;
        }
    }
    public partial class TBotApp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += ShortName.GetTLLength(); // ShortName
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            length += Photo.GetLength(); // Photo
            if (flags.IsBitSet(0)) { length += Document.GetLength(); } // Document
            length += 8; // Hash

            return length;
        }
    }
    public partial class TBotAppNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotAppSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += PlaceholderPath.GetTLLength(); } // PlaceholderPath
            if (flags.IsBitSet(1)) { length += 4; } // BackgroundColor
            if (flags.IsBitSet(2)) { length += 4; } // BackgroundDarkColor
            if (flags.IsBitSet(3)) { length += 4; } // HeaderColor
            if (flags.IsBitSet(4)) { length += 4; } // HeaderDarkColor

            return length;
        }
    }
    public partial class TBotBusinessConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += 8; // UserId
            length += 4; // DcId
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += Rights.GetLength(); } // Rights

            return length;
        }
    }
    public partial class TBotCommand
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Command.GetTLLength(); // Command
            length += Description.GetTLLength(); // Description

            return length;
        }
    }
    public partial class TBotCommandScopeChatAdmins
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotCommandScopeChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotCommandScopeDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotCommandScopePeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TBotCommandScopePeerAdmins
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TBotCommandScopePeerUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class TBotCommandScopeUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // UserId
            if (flags.IsBitSet(1)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(4)) { length += DescriptionPhoto.GetLength(); } // DescriptionPhoto
            if (flags.IsBitSet(5)) { length += DescriptionDocument.GetLength(); } // DescriptionDocument
            if (flags.IsBitSet(2)) { length += Commands.GetLength(); } // Commands
            if (flags.IsBitSet(3)) { length += MenuButton.GetLength(); } // MenuButton
            if (flags.IsBitSet(7)) { length += PrivacyPolicyUrl.GetTLLength(); } // PrivacyPolicyUrl
            if (flags.IsBitSet(8)) { length += AppSettings.GetLength(); } // AppSettings
            if (flags.IsBitSet(9)) { length += VerifierSettings.GetLength(); } // VerifierSettings

            return length;
        }
    }
    public partial class TBotInlineMediaResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Type.GetTLLength(); // Type
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(1)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(2)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(3)) { length += Description.GetTLLength(); } // Description
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TBotInlineMessageMediaAuto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageMediaContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Vcard.GetTLLength(); // Vcard
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageMediaGeo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Geo.GetLength(); // Geo
            if (flags.IsBitSet(0)) { length += 4; } // Heading
            if (flags.IsBitSet(1)) { length += 4; } // Period
            if (flags.IsBitSet(3)) { length += 4; } // ProximityNotificationRadius
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageMediaInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageMediaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Geo.GetLength(); // Geo
            length += Title.GetTLLength(); // Title
            length += Address.GetTLLength(); // Address
            length += Provider.GetTLLength(); // Provider
            length += VenueId.GetTLLength(); // VenueId
            length += VenueType.GetTLLength(); // VenueType
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageMediaWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineMessageRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            length += RichMessage.GetLength(); // RichMessage

            return length;
        }
    }
    public partial class TBotInlineMessageText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TBotInlineResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Type.GetTLLength(); // Type
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(3)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(4)) { length += Thumb.GetLength(); } // Thumb
            if (flags.IsBitSet(5)) { length += Content.GetLength(); } // Content
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TBotMenuButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TBotMenuButtonCommands
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotMenuButtonDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotPreviewMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class TBotVerification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId
            length += 8; // Icon
            length += Description.GetTLLength(); // Description

            return length;
        }
    }
    public partial class TBotVerifierSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Icon
            length += Company.GetTLLength(); // Company
            if (flags.IsBitSet(0)) { length += CustomDescription.GetTLLength(); } // CustomDescription

            return length;
        }
    }
    public partial class TBusinessAwayMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ShortcutId
            length += Schedule.GetLength(); // Schedule
            length += Recipients.GetLength(); // Recipients

            return length;
        }
    }
    public partial class TBusinessAwayMessageScheduleAlways
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBusinessAwayMessageScheduleCustom
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // StartDate
            length += 4; // EndDate

            return length;
        }
    }
    public partial class TBusinessAwayMessageScheduleOutsideWorkHours
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBusinessBotRecipients
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += Users.GetLength(); } // Users
            if (flags.IsBitSet(6)) { length += ExcludeUsers.GetLength(); } // ExcludeUsers

            return length;
        }
    }
    public partial class TBusinessBotRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Link.GetTLLength(); // Link
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(0)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            length += 4; // Views

            return length;
        }
    }
    public partial class TBusinessGreetingMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Recipients.GetLength(); // Recipients
            length += 4; // NoActivityDays

            return length;
        }
    }
    public partial class TBusinessIntro
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Sticker.GetLength(); } // Sticker

            return length;
        }
    }
    public partial class TBusinessLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += GeoPoint.GetLength(); } // GeoPoint
            length += Address.GetTLLength(); // Address

            return length;
        }
    }
    public partial class TBusinessRecipients
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += Users.GetLength(); } // Users

            return length;
        }
    }
    public partial class TBusinessWeeklyOpen
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // StartMinute
            length += 4; // EndMinute

            return length;
        }
    }
    public partial class TBusinessWorkHours
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += TimezoneId.GetTLLength(); // TimezoneId
            length += WeeklyOpen.GetLength(); // WeeklyOpen

            return length;
        }
    }
    public partial class TButtonTypeDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TButtonTypeRequestGeoLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TButtonTypeRequestPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Flags
            length += 4; // ButtonId
            length += PeerType.GetLength(); // PeerType
            length += 4; // MaxQuantity

            return length;
        }
    }
    public partial class TButtonTypeRequestPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TButtonTypeRequestPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Quiz

            return length;
        }
    }
    public partial class TButtonTypeSimpleWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TCdnConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PublicKeys.GetLength(); // PublicKeys

            return length;
        }
    }
    public partial class TCdnPublicKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId
            length += PublicKey.GetTLLength(); // PublicKey

            return length;
        }
    }
    public partial class TChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 8; // Id
            if (flags.IsBitSet(13)) { length += 8; } // AccessHash
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(6)) { length += Username.GetTLLength(); } // Username
            length += Photo.GetLength(); // Photo
            length += 4; // Date
            if (flags.IsBitSet(9)) { length += RestrictionReason.GetLength(); } // RestrictionReason
            if (flags.IsBitSet(14)) { length += AdminRights.GetLength(); } // AdminRights
            if (flags.IsBitSet(15)) { length += BannedRights.GetLength(); } // BannedRights
            if (flags.IsBitSet(18)) { length += DefaultBannedRights.GetLength(); } // DefaultBannedRights
            if (flags.IsBitSet(17)) { length += 4; } // ParticipantsCount
            if (flags2.IsBitSet(0)) { length += Usernames.GetLength(); } // Usernames
            if (flags2.IsBitSet(4)) { length += StoriesMaxId.GetLength(); } // StoriesMaxId
            if (flags2.IsBitSet(7)) { length += Color.GetLength(); } // Color
            if (flags2.IsBitSet(8)) { length += ProfileColor.GetLength(); } // ProfileColor
            if (flags2.IsBitSet(9)) { length += EmojiStatus.GetLength(); } // EmojiStatus
            if (flags2.IsBitSet(10)) { length += 4; } // Level
            if (flags2.IsBitSet(11)) { length += 4; } // SubscriptionUntilDate
            if (flags2.IsBitSet(13)) { length += 8; } // BotVerificationIcon
            if (flags2.IsBitSet(14)) { length += 8; } // SendPaidMessagesStars
            if (flags2.IsBitSet(18)) { length += 8; } // LinkedMonoforumId
            if (flags2.IsBitSet(20)) { length += 8; } // LinkedCommunityId

            return length;
        }
    }
    public partial class TChannelAdminLogEvent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Date
            length += 8; // UserId
            length += Action.GetLength(); // Action

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeAbout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetTLLength(); // PrevValue
            length += NewValue.GetTLLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeAvailableReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeEmojiStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevStickerset.GetLength(); // PrevStickerset
            length += NewStickerset.GetLength(); // NewStickerset

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // PrevValue
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeLinkedChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PrevValue
            length += 8; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangePeerColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevPhoto.GetLength(); // PrevPhoto
            length += NewPhoto.GetLength(); // NewPhoto

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeProfilePeerColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevStickerset.GetLength(); // PrevStickerset
            length += NewStickerset.GetLength(); // NewStickerset

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetTLLength(); // PrevValue
            length += NewValue.GetTLLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetTLLength(); // PrevValue
            length += NewValue.GetTLLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeUsernames
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionChangeWallpaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevValue.GetLength(); // PrevValue
            length += NewValue.GetLength(); // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionCreateTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Topic.GetLength(); // Topic

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionDefaultBannedRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevBannedRights.GetLength(); // PrevBannedRights
            length += NewBannedRights.GetLength(); // NewBannedRights

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionDeleteMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionDeleteTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Topic.GetLength(); // Topic

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionDiscardGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionEditMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevMessage.GetLength(); // PrevMessage
            length += NewMessage.GetLength(); // NewMessage

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionEditTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevTopic.GetLength(); // PrevTopic
            length += NewTopic.GetLength(); // NewTopic

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionExportedInviteDelete
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invite.GetLength(); // Invite

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionExportedInviteEdit
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevInvite.GetLength(); // PrevInvite
            length += NewInvite.GetLength(); // NewInvite

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionExportedInviteRevoke
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invite.GetLength(); // Invite

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantEditRank
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += PrevRank.GetTLLength(); // PrevRank
            length += NewRank.GetTLLength(); // NewRank

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantJoin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantJoinByInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Invite.GetLength(); // Invite

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantJoinByRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invite.GetLength(); // Invite
            length += 8; // ApprovedBy

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantLeave
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantMute
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantSubExtend
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevParticipant.GetLength(); // PrevParticipant
            length += NewParticipant.GetLength(); // NewParticipant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantToggleAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevParticipant.GetLength(); // PrevParticipant
            length += NewParticipant.GetLength(); // NewParticipant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantToggleBan
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PrevParticipant.GetLength(); // PrevParticipant
            length += NewParticipant.GetLength(); // NewParticipant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantUnmute
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionParticipantVolume
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionPinTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += PrevTopic.GetLength(); } // PrevTopic
            if (flags.IsBitSet(1)) { length += NewTopic.GetLength(); } // NewTopic

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionSendMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionStartGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionStopPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleAntiSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleAutotranslation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleForum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleGroupCallSetting
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // JoinMuted

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleNoForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionTogglePreHistoryHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleSignatureProfiles
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleSignatures
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionToggleSlowMode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // PrevValue
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TChannelAdminLogEventActionUpdatePinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TChannelAdminLogEventsFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TChannelForbidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(16)) { length += 4; } // UntilDate

            return length;
        }
    }
    public partial class TChannelFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 8; // Id
            length += About.GetTLLength(); // About
            if (flags.IsBitSet(0)) { length += 4; } // ParticipantsCount
            if (flags.IsBitSet(1)) { length += 4; } // AdminsCount
            if (flags.IsBitSet(2)) { length += 4; } // KickedCount
            if (flags.IsBitSet(2)) { length += 4; } // BannedCount
            if (flags.IsBitSet(13)) { length += 4; } // OnlineCount
            length += 4; // ReadInboxMaxId
            length += 4; // ReadOutboxMaxId
            length += 4; // UnreadCount
            length += ChatPhoto.GetLength(); // ChatPhoto
            length += NotifySettings.GetLength(); // NotifySettings
            if (flags.IsBitSet(23)) { length += ExportedInvite.GetLength(); } // ExportedInvite
            length += BotInfo.GetLength(); // BotInfo
            if (flags.IsBitSet(4)) { length += 8; } // MigratedFromChatId
            if (flags.IsBitSet(4)) { length += 4; } // MigratedFromMaxId
            if (flags.IsBitSet(5)) { length += 4; } // PinnedMsgId
            if (flags.IsBitSet(8)) { length += Stickerset.GetLength(); } // Stickerset
            if (flags.IsBitSet(9)) { length += 4; } // AvailableMinId
            if (flags.IsBitSet(11)) { length += 4; } // FolderId
            if (flags.IsBitSet(14)) { length += 8; } // LinkedChatId
            if (flags.IsBitSet(15)) { length += Location.GetLength(); } // Location
            if (flags.IsBitSet(17)) { length += 4; } // SlowmodeSeconds
            if (flags.IsBitSet(18)) { length += 4; } // SlowmodeNextSendDate
            if (flags.IsBitSet(12)) { length += 4; } // StatsDc
            length += 4; // Pts
            if (flags.IsBitSet(21)) { length += Call.GetLength(); } // Call
            if (flags.IsBitSet(24)) { length += 4; } // TtlPeriod
            if (flags.IsBitSet(25)) { length += PendingSuggestions.GetLength(); } // PendingSuggestions
            if (flags.IsBitSet(26)) { length += GroupcallDefaultJoinAs.GetLength(); } // GroupcallDefaultJoinAs
            if (flags.IsBitSet(27)) { length += ThemeEmoticon.GetTLLength(); } // ThemeEmoticon
            if (flags.IsBitSet(28)) { length += 4; } // RequestsPending
            if (flags.IsBitSet(28)) { length += RecentRequesters.GetLength(); } // RecentRequesters
            if (flags.IsBitSet(29)) { length += DefaultSendAs.GetLength(); } // DefaultSendAs
            if (flags.IsBitSet(30)) { length += AvailableReactions.GetLength(); } // AvailableReactions
            if (flags2.IsBitSet(13)) { length += 4; } // ReactionsLimit
            if (flags2.IsBitSet(4)) { length += Stories.GetLength(); } // Stories
            if (flags2.IsBitSet(7)) { length += Wallpaper.GetLength(); } // Wallpaper
            if (flags2.IsBitSet(8)) { length += 4; } // BoostsApplied
            if (flags2.IsBitSet(9)) { length += 4; } // BoostsUnrestrict
            if (flags2.IsBitSet(10)) { length += Emojiset.GetLength(); } // Emojiset
            if (flags2.IsBitSet(17)) { length += BotVerification.GetLength(); } // BotVerification
            if (flags2.IsBitSet(18)) { length += 4; } // StargiftsCount
            if (flags2.IsBitSet(21)) { length += 8; } // SendPaidMessagesStars
            if (flags2.IsBitSet(22)) { length += MainTab.GetLength(); } // MainTab
            if (flags2.IsBitSet(23)) { length += 8; } // GuardBotId

            return length;
        }
    }
    public partial class TChannelLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GeoPoint.GetLength(); // GeoPoint
            length += Address.GetTLLength(); // Address

            return length;
        }
    }
    public partial class TChannelLocationEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelMessagesFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Ranges.GetLength(); // Ranges

            return length;
        }
    }
    public partial class TChannelMessagesFilterEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 4; // Date
            if (flags.IsBitSet(0)) { length += 4; } // SubscriptionUntilDate
            if (flags.IsBitSet(2)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChannelParticipantAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            if (flags.IsBitSet(1)) { length += 8; } // InviterId
            length += 8; // PromotedBy
            length += 4; // Date
            length += AdminRights.GetLength(); // AdminRights
            if (flags.IsBitSet(2)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChannelParticipantBanned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // KickedBy
            length += 4; // Date
            length += BannedRights.GetLength(); // BannedRights
            if (flags.IsBitSet(2)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChannelParticipantCreator
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += AdminRights.GetLength(); // AdminRights
            if (flags.IsBitSet(0)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChannelParticipantLeft
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TChannelParticipantsAdmins
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelParticipantsBanned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q

            return length;
        }
    }
    public partial class TChannelParticipantsBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelParticipantsContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q

            return length;
        }
    }
    public partial class TChannelParticipantSelf
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 8; // InviterId
            length += 4; // Date
            if (flags.IsBitSet(1)) { length += 4; } // SubscriptionUntilDate
            if (flags.IsBitSet(2)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChannelParticipantsKicked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q

            return length;
        }
    }
    public partial class TChannelParticipantsMentions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Q.GetTLLength(); } // Q
            if (flags.IsBitSet(1)) { length += 4; } // TopMsgId

            return length;
        }
    }
    public partial class TChannelParticipantsRecent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChannelParticipantsSearch
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q

            return length;
        }
    }
    public partial class TChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Title.GetTLLength(); // Title
            length += Photo.GetLength(); // Photo
            length += 4; // ParticipantsCount
            length += 4; // Date
            length += 4; // Version
            if (flags.IsBitSet(6)) { length += MigratedTo.GetLength(); } // MigratedTo
            if (flags.IsBitSet(14)) { length += AdminRights.GetLength(); } // AdminRights
            if (flags.IsBitSet(18)) { length += DefaultBannedRights.GetLength(); } // DefaultBannedRights

            return length;
        }
    }
    public partial class TChatAdminRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TChatAdminWithInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // AdminId
            length += 4; // InvitesCount
            length += 4; // RevokedInvitesCount

            return length;
        }
    }
    public partial class TChatBannedRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // UntilDate

            return length;
        }
    }
    public partial class TChatEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TChatForbidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TChatFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += About.GetTLLength(); // About
            length += Participants.GetLength(); // Participants
            if (flags.IsBitSet(2)) { length += ChatPhoto.GetLength(); } // ChatPhoto
            length += NotifySettings.GetLength(); // NotifySettings
            if (flags.IsBitSet(13)) { length += ExportedInvite.GetLength(); } // ExportedInvite
            if (flags.IsBitSet(3)) { length += BotInfo.GetLength(); } // BotInfo
            if (flags.IsBitSet(6)) { length += 4; } // PinnedMsgId
            if (flags.IsBitSet(11)) { length += 4; } // FolderId
            if (flags.IsBitSet(12)) { length += Call.GetLength(); } // Call
            if (flags.IsBitSet(14)) { length += 4; } // TtlPeriod
            if (flags.IsBitSet(15)) { length += GroupcallDefaultJoinAs.GetLength(); } // GroupcallDefaultJoinAs
            if (flags.IsBitSet(16)) { length += ThemeEmoticon.GetTLLength(); } // ThemeEmoticon
            if (flags.IsBitSet(17)) { length += 4; } // RequestsPending
            if (flags.IsBitSet(17)) { length += RecentRequesters.GetLength(); } // RecentRequesters
            if (flags.IsBitSet(18)) { length += AvailableReactions.GetLength(); } // AvailableReactions
            if (flags.IsBitSet(20)) { length += 4; } // ReactionsLimit

            return length;
        }
    }
    public partial class TChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(5)) { length += About.GetTLLength(); } // About
            length += Photo.GetLength(); // Photo
            length += 4; // ParticipantsCount
            if (flags.IsBitSet(4)) { length += Participants.GetLength(); } // Participants
            length += 4; // Color
            if (flags.IsBitSet(10)) { length += SubscriptionPricing.GetLength(); } // SubscriptionPricing
            if (flags.IsBitSet(12)) { length += 8; } // SubscriptionFormId
            if (flags.IsBitSet(13)) { length += BotVerification.GetLength(); } // BotVerification

            return length;
        }
    }
    public partial class TChatInviteAlready
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chat.GetLength(); // Chat

            return length;
        }
    }
    public partial class TChatInviteExported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Link.GetTLLength(); // Link
            length += 8; // AdminId
            length += 4; // Date
            if (flags.IsBitSet(4)) { length += 4; } // StartDate
            if (flags.IsBitSet(1)) { length += 4; } // ExpireDate
            if (flags.IsBitSet(2)) { length += 4; } // UsageLimit
            if (flags.IsBitSet(3)) { length += 4; } // Usage
            if (flags.IsBitSet(7)) { length += 4; } // Requested
            if (flags.IsBitSet(10)) { length += 4; } // SubscriptionExpired
            if (flags.IsBitSet(8)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(9)) { length += SubscriptionPricing.GetLength(); } // SubscriptionPricing

            return length;
        }
    }
    public partial class TChatInviteImporter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += About.GetTLLength(); } // About
            if (flags.IsBitSet(1)) { length += 8; } // ApprovedBy

            return length;
        }
    }
    public partial class TChatInvitePeek
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chat.GetLength(); // Chat
            length += 4; // Expires

            return length;
        }
    }
    public partial class TChatInvitePublicJoinRequests
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChatOnlines
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Onlines

            return length;
        }
    }
    public partial class TChatParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 8; // InviterId
            length += 4; // Date
            if (flags.IsBitSet(0)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChatParticipantAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 8; // InviterId
            length += 4; // Date
            if (flags.IsBitSet(0)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChatParticipantCreator
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class TChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += Participants.GetLength(); // Participants
            length += 4; // Version

            return length;
        }
    }
    public partial class TChatParticipantsForbidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChatId
            if (flags.IsBitSet(0)) { length += SelfParticipant.GetLength(); } // SelfParticipant

            return length;
        }
    }
    public partial class TChatPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // PhotoId
            if (flags.IsBitSet(1)) { length += StrippedThumb.GetTLLength(); } // StrippedThumb
            length += 4; // DcId

            return length;
        }
    }
    public partial class TChatPhotoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChatReactionsAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TChatReactionsNone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TChatReactionsSome
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Reactions.GetLength(); // Reactions

            return length;
        }
    }
    public partial class TChatTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TChatThemeUniqueGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift
            length += ThemeSettings.GetLength(); // ThemeSettings

            return length;
        }
    }
    public partial class TClientDHInnerData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 8; // RetryId
            length += GB.GetTLLength(); // GB

            return length;
        }
    }
    public partial class TCodeSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(6)) { length += LogoutTokens.GetLength(); } // LogoutTokens
            if (flags.IsBitSet(8)) { length += Token.GetTLLength(); } // Token
            if (flags.IsBitSet(8)) { length += 4; } // AppSandbox

            return length;
        }
    }
    public partial class TCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 8; // Id
            if (flags.IsBitSet(13)) { length += 8; } // AccessHash
            length += Title.GetTLLength(); // Title
            length += Photo.GetLength(); // Photo
            length += 4; // Date
            if (flags.IsBitSet(14)) { length += AdminRights.GetLength(); } // AdminRights
            if (flags.IsBitSet(18)) { length += DefaultBannedRights.GetLength(); } // DefaultBannedRights

            return length;
        }
    }
    public partial class TCommunityForbidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            if (flags.IsBitSet(13)) { length += 8; } // AccessHash
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TCommunityFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += About.GetTLLength(); // About
            length += ChatPhoto.GetLength(); // ChatPhoto
            length += LinkedPeers.GetLength(); // LinkedPeers
            if (flags.IsBitSet(1)) { length += 4; } // AdminsCount
            if (flags.IsBitSet(2)) { length += 4; } // KickedCount
            if (flags.IsBitSet(0)) { length += 4; } // PeerLinkRequestsPending

            return length;
        }
    }
    public partial class TCommunityPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Visible
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TCommunityPeerRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // RequestedBy
            length += 4; // Date

            return length;
        }
    }
    public partial class TConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Date
            length += 4; // Expires
            length += 4; // TestMode
            length += 4; // ThisDc
            length += DcOptions.GetLength(); // DcOptions
            length += DcTxtDomainName.GetTLLength(); // DcTxtDomainName
            length += 4; // ChatSizeMax
            length += 4; // MegagroupSizeMax
            length += 4; // ForwardedCountMax
            length += 4; // OnlineUpdatePeriodMs
            length += 4; // OfflineBlurTimeoutMs
            length += 4; // OfflineIdleTimeoutMs
            length += 4; // OnlineCloudTimeoutMs
            length += 4; // NotifyCloudDelayMs
            length += 4; // NotifyDefaultDelayMs
            length += 4; // PushChatPeriodMs
            length += 4; // PushChatLimit
            length += 4; // EditTimeLimit
            length += 4; // RevokeTimeLimit
            length += 4; // RevokePmTimeLimit
            length += 4; // RatingEDecay
            length += 4; // StickersRecentLimit
            length += 4; // ChannelsReadMediaPeriod
            if (flags.IsBitSet(0)) { length += 4; } // TmpSessions
            length += 4; // CallReceiveTimeoutMs
            length += 4; // CallRingTimeoutMs
            length += 4; // CallConnectTimeoutMs
            length += 4; // CallPacketTimeoutMs
            length += MeUrlPrefix.GetTLLength(); // MeUrlPrefix
            if (flags.IsBitSet(7)) { length += AutoupdateUrlPrefix.GetTLLength(); } // AutoupdateUrlPrefix
            if (flags.IsBitSet(9)) { length += GifSearchUsername.GetTLLength(); } // GifSearchUsername
            if (flags.IsBitSet(10)) { length += VenueSearchUsername.GetTLLength(); } // VenueSearchUsername
            if (flags.IsBitSet(11)) { length += ImgSearchUsername.GetTLLength(); } // ImgSearchUsername
            if (flags.IsBitSet(12)) { length += StaticMapsProvider.GetTLLength(); } // StaticMapsProvider
            length += 4; // CaptionLengthMax
            length += 4; // MessageLengthMax
            length += 4; // WebfileDcId
            if (flags.IsBitSet(2)) { length += SuggestedLangCode.GetTLLength(); } // SuggestedLangCode
            if (flags.IsBitSet(2)) { length += 4; } // LangPackVersion
            if (flags.IsBitSet(2)) { length += 4; } // BaseLangPackVersion
            if (flags.IsBitSet(15)) { length += ReactionsDefault.GetLength(); } // ReactionsDefault
            if (flags.IsBitSet(16)) { length += AutologinToken.GetTLLength(); } // AutologinToken

            return length;
        }
    }
    public partial class TConnectedBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // BotId
            length += Recipients.GetLength(); // Recipients
            length += Rights.GetLength(); // Rights
            if (flags.IsBitSet(0)) { length += Device.GetTLLength(); } // Device
            if (flags.IsBitSet(1)) { length += 4; } // Date
            if (flags.IsBitSet(2)) { length += Location.GetTLLength(); } // Location

            return length;
        }
    }
    public partial class TConnectedBotStarRef
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url
            length += 4; // Date
            length += 8; // BotId
            length += 4; // CommissionPermille
            if (flags.IsBitSet(0)) { length += 4; } // DurationMonths
            length += 8; // Participants
            length += 8; // Revenue

            return length;
        }
    }
    public partial class TContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Mutual

            return length;
        }
    }
    public partial class TContactBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ContactId
            length += Birthday.GetLength(); // Birthday

            return length;
        }
    }
    public partial class TContactStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += Status.GetLength(); // Status

            return length;
        }
    }
    public partial class TDataJSON
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class TDcOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += IpAddress.GetTLLength(); // IpAddress
            length += 4; // Port
            if (flags.IsBitSet(10)) { length += Secret.GetTLLength(); } // Secret

            return length;
        }
    }
    public partial class TDefaultHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Period

            return length;
        }
    }
    public partial class TDestroyAuthKeyFail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDestroyAuthKeyNone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDestroyAuthKeyOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDestroySessionNone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // SessionId

            return length;
        }
    }
    public partial class TDestroySessionOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // SessionId

            return length;
        }
    }
    public partial class TDhGenFail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 16; // NewNonceHash3

            return length;
        }
    }
    public partial class TDhGenOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 16; // NewNonceHash1

            return length;
        }
    }
    public partial class TDhGenRetry
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 16; // NewNonceHash2

            return length;
        }
    }
    public partial class TDialog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // TopMessage
            length += 4; // ReadInboxMaxId
            length += 4; // ReadOutboxMaxId
            length += 4; // UnreadCount
            length += 4; // UnreadMentionsCount
            length += 4; // UnreadReactionsCount
            length += 4; // UnreadPollVotesCount
            length += NotifySettings.GetLength(); // NotifySettings
            if (flags.IsBitSet(0)) { length += 4; } // Pts
            if (flags.IsBitSet(1)) { length += Draft.GetLength(); } // Draft
            if (flags.IsBitSet(4)) { length += 4; } // FolderId
            if (flags.IsBitSet(5)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TDialogCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CommunityId
            length += NotifySettings.GetLength(); // NotifySettings

            return length;
        }
    }
    public partial class TDialogFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += Title.GetLength(); // Title
            if (flags.IsBitSet(25)) { length += Emoticon.GetTLLength(); } // Emoticon
            if (flags.IsBitSet(27)) { length += 4; } // Color
            length += PinnedPeers.GetLength(); // PinnedPeers
            length += IncludePeers.GetLength(); // IncludePeers
            length += ExcludePeers.GetLength(); // ExcludePeers

            return length;
        }
    }
    public partial class TDialogFilterChatlist
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += Title.GetLength(); // Title
            if (flags.IsBitSet(25)) { length += Emoticon.GetTLLength(); } // Emoticon
            if (flags.IsBitSet(27)) { length += 4; } // Color
            length += PinnedPeers.GetLength(); // PinnedPeers
            length += IncludePeers.GetLength(); // IncludePeers

            return length;
        }
    }
    public partial class TDialogFilterDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDialogFilterSuggested
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Filter.GetLength(); // Filter
            length += Description.GetTLLength(); // Description

            return length;
        }
    }
    public partial class TDialogFolder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Folder.GetLength(); // Folder
            length += Peer.GetLength(); // Peer
            length += 4; // TopMessage
            length += 4; // UnreadMutedPeersCount
            length += 4; // UnreadUnmutedPeersCount
            length += 4; // UnreadMutedMessagesCount
            length += 4; // UnreadUnmutedMessagesCount

            return length;
        }
    }
    public partial class TDialogPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TDialogPeerCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // CommunityId

            return length;
        }
    }
    public partial class TDialogPeerFolder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // FolderId

            return length;
        }
    }
    public partial class TDisallowedGiftsSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference
            length += 4; // Date
            length += MimeType.GetTLLength(); // MimeType
            length += 8; // Size
            if (flags.IsBitSet(0)) { length += Thumbs.GetLength(); } // Thumbs
            if (flags.IsBitSet(1)) { length += VideoThumbs.GetLength(); } // VideoThumbs
            length += 4; // DcId
            length += Attributes.GetLength(); // Attributes

            return length;
        }
    }
    public partial class TDocumentAttributeAnimated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDocumentAttributeAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Duration
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += Performer.GetTLLength(); } // Performer
            if (flags.IsBitSet(2)) { length += Waveform.GetTLLength(); } // Waveform

            return length;
        }
    }
    public partial class TDocumentAttributeCustomEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Alt.GetTLLength(); // Alt
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class TDocumentAttributeFilename
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FileName.GetTLLength(); // FileName

            return length;
        }
    }
    public partial class TDocumentAttributeHasStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TDocumentAttributeImageSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // W
            length += 4; // H

            return length;
        }
    }
    public partial class TDocumentAttributeSticker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Alt.GetTLLength(); // Alt
            length += Stickerset.GetLength(); // Stickerset
            if (flags.IsBitSet(0)) { length += MaskCoords.GetLength(); } // MaskCoords

            return length;
        }
    }
    public partial class TDocumentAttributeVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Duration
            length += 4; // W
            length += 4; // H
            if (flags.IsBitSet(2)) { length += 4; } // PreloadPrefixSize
            if (flags.IsBitSet(4)) { length += 8; } // VideoStartTs
            if (flags.IsBitSet(5)) { length += VideoCodec.GetTLLength(); } // VideoCodec

            return length;
        }
    }
    public partial class TDocumentEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TDraftMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(5)) { length += Media.GetLength(); } // Media
            length += 4; // Date
            if (flags.IsBitSet(7)) { length += 8; } // Effect
            if (flags.IsBitSet(8)) { length += SuggestedPost.GetLength(); } // SuggestedPost
            if (flags.IsBitSet(9)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class TDraftMessageEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Date

            return length;
        }
    }
    public partial class TEmailVerificationApple
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TEmailVerificationCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Code.GetTLLength(); // Code

            return length;
        }
    }
    public partial class TEmailVerificationGoogle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TEmailVerifyPurposeLoginChange
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEmailVerifyPurposeLoginSetup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash

            return length;
        }
    }
    public partial class TEmailVerifyPurposePassport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEmojiGroup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += 8; // IconEmojiId
            length += Emoticons.GetLength(); // Emoticons

            return length;
        }
    }
    public partial class TEmojiGroupGreeting
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += 8; // IconEmojiId
            length += Emoticons.GetLength(); // Emoticons

            return length;
        }
    }
    public partial class TEmojiGroupPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += 8; // IconEmojiId

            return length;
        }
    }
    public partial class TEmojiKeyword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Keyword.GetTLLength(); // Keyword
            length += Emoticons.GetLength(); // Emoticons

            return length;
        }
    }
    public partial class TEmojiKeywordDeleted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Keyword.GetTLLength(); // Keyword
            length += Emoticons.GetLength(); // Emoticons

            return length;
        }
    }
    public partial class TEmojiKeywordsDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode
            length += 4; // FromVersion
            length += 4; // Version
            length += Keywords.GetLength(); // Keywords

            return length;
        }
    }
    public partial class TEmojiLanguage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class TEmojiList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += DocumentId.GetLength(); // DocumentId

            return length;
        }
    }
    public partial class TEmojiListNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // DocumentId
            if (flags.IsBitSet(0)) { length += 4; } // Until

            return length;
        }
    }
    public partial class TEmojiStatusCollectible
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CollectibleId
            length += 8; // DocumentId
            length += Title.GetTLLength(); // Title
            length += Slug.GetTLLength(); // Slug
            length += 8; // PatternDocumentId
            length += 4; // CenterColor
            length += 4; // EdgeColor
            length += 4; // PatternColor
            length += 4; // TextColor
            if (flags.IsBitSet(0)) { length += 4; } // Until

            return length;
        }
    }
    public partial class TEmojiStatusEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEmojiURL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TEncryptedChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += GAOrB.GetTLLength(); // GAOrB
            length += 8; // KeyFingerprint

            return length;
        }
    }
    public partial class TEncryptedChatDiscarded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id

            return length;
        }
    }
    public partial class TEncryptedChatEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TEncryptedChatRequested
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // FolderId
            length += 4; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += GA.GetTLLength(); // GA

            return length;
        }
    }
    public partial class TEncryptedChatWaiting
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId

            return length;
        }
    }
    public partial class TEncryptedFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += 8; // Size
            length += 4; // DcId
            length += 4; // KeyFingerprint

            return length;
        }
    }
    public partial class TEncryptedFileEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEncryptedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // RandomId
            length += 4; // ChatId
            length += 4; // Date
            length += Bytes.GetTLLength(); // Bytes
            length += File.GetLength(); // File

            return length;
        }
    }
    public partial class TEncryptedMessageService
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // RandomId
            length += 4; // ChatId
            length += 4; // Date
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TEphemeralMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += FromId.GetLength(); // FromId
            if (flags.IsBitSet(9)) { length += PeerId.GetLength(); } // PeerId
            length += 8; // ReceiverId
            if (flags.IsBitSet(1)) { length += 4; } // TopMsgId
            length += 4; // Date
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(2)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(3)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(4)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(6)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(8)) { length += RichMessage.GetLength(); } // RichMessage
            if (flags.IsBitSet(10)) { length += 8; } // ChatInstance
            if (flags.IsBitSet(11)) { length += 4; } // AnchorMsgId

            return length;
        }
    }
    public partial class TError
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Code
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TExportedChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Url.GetTLLength(); // Url
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class TExportedContactToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 4; // Expires

            return length;
        }
    }
    public partial class TExportedMessageLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Link.GetTLLength(); // Link
            length += Html.GetTLLength(); // Html

            return length;
        }
    }
    public partial class TExportedStoryLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Link.GetTLLength(); // Link

            return length;
        }
    }
    public partial class TFactCheck
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Country.GetTLLength(); } // Country
            if (flags.IsBitSet(1)) { length += Text.GetLength(); } // Text
            length += 8; // Hash

            return length;
        }
    }
    public partial class TFileHash
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Offset
            length += 4; // Limit
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class TFolder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(3)) { length += Photo.GetLength(); } // Photo

            return length;
        }
    }
    public partial class TFolderPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // FolderId

            return length;
        }
    }
    public partial class TForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 4; // Date
            length += Peer.GetLength(); // Peer
            length += Title.GetTLLength(); // Title
            length += 4; // IconColor
            if (flags.IsBitSet(0)) { length += 8; } // IconEmojiId
            length += 4; // TopMessage
            length += 4; // ReadInboxMaxId
            length += 4; // ReadOutboxMaxId
            length += 4; // UnreadCount
            length += 4; // UnreadMentionsCount
            length += 4; // UnreadReactionsCount
            length += 4; // UnreadPollVotesCount
            length += FromId.GetLength(); // FromId
            length += NotifySettings.GetLength(); // NotifySettings
            if (flags.IsBitSet(4)) { length += Draft.GetLength(); } // Draft

            return length;
        }
    }
    public partial class TForumTopicDeleted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TFoundStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Story.GetLength(); // Story

            return length;
        }
    }
    public partial class TFutureSalt
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ValidSince
            length += 4; // ValidUntil
            length += 8; // Salt

            return length;
        }
    }
    public partial class TFutureSalts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ReqMsgId
            length += 4; // Now
            foreach (var item in Salts) { length += item.GetLength(); }; // Salts

            return length;
        }
    }
    public partial class TGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += ShortName.GetTLLength(); // ShortName
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            length += Photo.GetLength(); // Photo
            if (flags.IsBitSet(0)) { length += Document.GetLength(); } // Document

            return length;
        }
    }
    public partial class TGeoPoint
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Long
            length += 8; // Lat
            length += 8; // AccessHash
            if (flags.IsBitSet(0)) { length += 4; } // AccuracyRadius

            return length;
        }
    }
    public partial class TGeoPointAddress
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += CountryIso2.GetTLLength(); // CountryIso2
            if (flags.IsBitSet(0)) { length += State.GetTLLength(); } // State
            if (flags.IsBitSet(1)) { length += City.GetTLLength(); } // City
            if (flags.IsBitSet(2)) { length += Street.GetTLLength(); } // Street

            return length;
        }
    }
    public partial class TGeoPointEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TGlobalPrivacySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(5)) { length += 8; } // NoncontactPeersPaidStars
            if (flags.IsBitSet(6)) { length += DisallowedGifts.GetLength(); } // DisallowedGifts

            return length;
        }
    }
    public partial class TGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // ParticipantsCount
            if (flags.IsBitSet(3)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(4)) { length += 4; } // StreamDcId
            if (flags.IsBitSet(5)) { length += 4; } // RecordStartDate
            if (flags.IsBitSet(7)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(10)) { length += 4; } // UnmutedVideoCount
            length += 4; // UnmutedVideoLimit
            length += 4; // Version
            if (flags.IsBitSet(16)) { length += InviteLink.GetTLLength(); } // InviteLink
            if (flags.IsBitSet(20)) { length += 8; } // SendPaidMessagesStars
            if (flags.IsBitSet(21)) { length += DefaultSendAs.GetLength(); } // DefaultSendAs

            return length;
        }
    }
    public partial class TGroupCallDiscarded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // Duration

            return length;
        }
    }
    public partial class TGroupCallDonor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += PeerId.GetLength(); } // PeerId
            length += 8; // Stars

            return length;
        }
    }
    public partial class TGroupCallMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += FromId.GetLength(); // FromId
            length += 4; // Date
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(0)) { length += 8; } // PaidMessageStars

            return length;
        }
    }
    public partial class TGroupCallParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Date
            if (flags.IsBitSet(3)) { length += 4; } // ActiveDate
            length += 4; // Source
            if (flags.IsBitSet(7)) { length += 4; } // Volume
            if (flags.IsBitSet(11)) { length += About.GetTLLength(); } // About
            if (flags.IsBitSet(13)) { length += 8; } // RaiseHandRating
            if (flags.IsBitSet(6)) { length += Video.GetLength(); } // Video
            if (flags.IsBitSet(14)) { length += Presentation.GetLength(); } // Presentation
            if (flags.IsBitSet(16)) { length += 8; } // PaidStarsTotal

            return length;
        }
    }
    public partial class TGroupCallParticipantVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Endpoint.GetTLLength(); // Endpoint
            length += SourceGroups.GetLength(); // SourceGroups
            if (flags.IsBitSet(1)) { length += 4; } // AudioSource

            return length;
        }
    }
    public partial class TGroupCallParticipantVideoSourceGroup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Semantics.GetTLLength(); // Semantics
            length += Sources.GetLength(); // Sources

            return length;
        }
    }
    public partial class TGroupCallStreamChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Channel
            length += 4; // Scale
            length += 8; // LastTimestampMs

            return length;
        }
    }
    public partial class TGzipPacked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PackedData.GetTLLength(); // PackedData

            return length;
        }
    }
    public partial class THighScore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pos
            length += 8; // UserId
            length += 4; // Score

            return length;
        }
    }
    public partial class TImportedContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 8; // ClientId

            return length;
        }
    }
    public partial class TInlineBotSwitchPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += StartParam.GetTLLength(); // StartParam

            return length;
        }
    }
    public partial class TInlineBotWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TInlineButtonTypeBuy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineButtonTypeCallback
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class TInlineButtonTypeCopy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += CopyText.GetTLLength(); // CopyText

            return length;
        }
    }
    public partial class TInlineButtonTypeDisabled
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineButtonTypeGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineButtonTypeSwitchInline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Query.GetTLLength(); // Query
            if (flags.IsBitSet(1)) { length += PeerTypes.GetLength(); } // PeerTypes

            return length;
        }
    }
    public partial class TInlineButtonTypeUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TInlineButtonTypeUrlAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += FwdText.GetTLLength(); } // FwdText
            length += Url.GetTLLength(); // Url
            length += 4; // ButtonId

            return length;
        }
    }
    public partial class TInlineButtonTypeUserProfile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TInlineButtonTypeWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TInlineQueryPeerTypeBotPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineQueryPeerTypeBroadcast
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineQueryPeerTypeChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineQueryPeerTypeMegagroup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineQueryPeerTypePM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInlineQueryPeerTypeSameBotPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputAiComposeToneDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetTLLength(); // Tone

            return length;
        }
    }
    public partial class TInputAiComposeToneID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputAiComposeToneSingleUse
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += CustomPrompt.GetTLLength(); // CustomPrompt

            return length;
        }
    }
    public partial class TInputAiComposeToneSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputAppEvent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Time
            length += Type.GetTLLength(); // Type
            length += 8; // Peer
            length += Data.GetLength(); // Data

            return length;
        }
    }
    public partial class TInputBotAppID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputBotAppShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += BotId.GetLength(); // BotId
            length += ShortName.GetTLLength(); // ShortName

            return length;
        }
    }
    public partial class TInputBotInlineMessageGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputBotInlineMessageID64
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId
            length += 8; // OwnerId
            length += 4; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaAuto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Vcard.GetTLLength(); // Vcard
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaGeo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += GeoPoint.GetLength(); // GeoPoint
            if (flags.IsBitSet(0)) { length += 4; } // Heading
            if (flags.IsBitSet(1)) { length += 4; } // Period
            if (flags.IsBitSet(3)) { length += 4; } // ProximityNotificationRadius
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            length += Payload.GetTLLength(); // Payload
            length += Provider.GetTLLength(); // Provider
            length += ProviderData.GetLength(); // ProviderData
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += GeoPoint.GetLength(); // GeoPoint
            length += Title.GetTLLength(); // Title
            length += Address.GetTLLength(); // Address
            length += Provider.GetTLLength(); // Provider
            length += VenueId.GetTLLength(); // VenueId
            length += VenueType.GetTLLength(); // VenueType
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageMediaWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineMessageRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            length += RichMessage.GetLength(); // RichMessage

            return length;
        }
    }
    public partial class TInputBotInlineMessageText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup

            return length;
        }
    }
    public partial class TInputBotInlineResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Type.GetTLLength(); // Type
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(3)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(4)) { length += Thumb.GetLength(); } // Thumb
            if (flags.IsBitSet(5)) { length += Content.GetLength(); } // Content
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TInputBotInlineResultDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Type.GetTLLength(); // Type
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Description.GetTLLength(); } // Description
            length += Document.GetLength(); // Document
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TInputBotInlineResultGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += ShortName.GetTLLength(); // ShortName
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TInputBotInlineResultPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Type.GetTLLength(); // Type
            length += Photo.GetLength(); // Photo
            length += SendMessage.GetLength(); // SendMessage

            return length;
        }
    }
    public partial class TInputBusinessAwayMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ShortcutId
            length += Schedule.GetLength(); // Schedule
            length += Recipients.GetLength(); // Recipients

            return length;
        }
    }
    public partial class TInputBusinessBotRecipients
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += Users.GetLength(); } // Users
            if (flags.IsBitSet(6)) { length += ExcludeUsers.GetLength(); } // ExcludeUsers

            return length;
        }
    }
    public partial class TInputBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(0)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title

            return length;
        }
    }
    public partial class TInputBusinessGreetingMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Recipients.GetLength(); // Recipients
            length += 4; // NoActivityDays

            return length;
        }
    }
    public partial class TInputBusinessIntro
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Sticker.GetLength(); } // Sticker

            return length;
        }
    }
    public partial class TInputBusinessRecipients
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += Users.GetLength(); } // Users

            return length;
        }
    }
    public partial class TInputButtonTypeRequestPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ButtonId
            length += PeerType.GetLength(); // PeerType
            length += 4; // MaxQuantity

            return length;
        }
    }
    public partial class TInputChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputChannelEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputChannelFromMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // ChannelId

            return length;
        }
    }
    public partial class TInputChatlistDialogFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // FilterId

            return length;
        }
    }
    public partial class TInputChatPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class TInputChatPhotoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputChatTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TInputChatThemeEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputChatThemeUniqueGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputChatUploadedPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += File.GetLength(); } // File
            if (flags.IsBitSet(1)) { length += Video.GetLength(); } // Video
            if (flags.IsBitSet(2)) { length += 8; } // VideoStartTs
            if (flags.IsBitSet(3)) { length += VideoEmojiMarkup.GetLength(); } // VideoEmojiMarkup

            return length;
        }
    }
    public partial class TInputCheckPasswordEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputCheckPasswordSRP
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // SrpId
            length += A.GetTLLength(); // A
            length += M1.GetTLLength(); // M1

            return length;
        }
    }
    public partial class TInputClientProxy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Address.GetTLLength(); // Address
            length += 4; // Port

            return length;
        }
    }
    public partial class TInputCollectiblePhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Phone.GetTLLength(); // Phone

            return length;
        }
    }
    public partial class TInputCollectibleUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class TInputDialogPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TInputDialogPeerCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Community.GetLength(); // Community

            return length;
        }
    }
    public partial class TInputDialogPeerFolder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // FolderId

            return length;
        }
    }
    public partial class TInputDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference

            return length;
        }
    }
    public partial class TInputDocumentEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputDocumentFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference
            length += ThumbSize.GetTLLength(); // ThumbSize

            return length;
        }
    }
    public partial class TInputEmojiStatusCollectible
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CollectibleId
            if (flags.IsBitSet(0)) { length += 4; } // Until

            return length;
        }
    }
    public partial class TInputEncryptedChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ChatId
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputEncryptedFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputEncryptedFileBigUploaded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Parts
            length += 4; // KeyFingerprint

            return length;
        }
    }
    public partial class TInputEncryptedFileEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputEncryptedFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputEncryptedFileUploaded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Parts
            length += Md5Checksum.GetTLLength(); // Md5Checksum
            length += 4; // KeyFingerprint

            return length;
        }
    }
    public partial class TInputFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Parts
            length += Name.GetTLLength(); // Name
            length += Md5Checksum.GetTLLength(); // Md5Checksum

            return length;
        }
    }
    public partial class TInputFileBig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Parts
            length += Name.GetTLLength(); // Name

            return length;
        }
    }
    public partial class TInputFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // VolumeId
            length += 4; // LocalId
            length += 8; // Secret
            length += FileReference.GetTLLength(); // FileReference

            return length;
        }
    }
    public partial class TInputFileStoryDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class TInputFolderPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // FolderId

            return length;
        }
    }
    public partial class TInputGameID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputGameShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += BotId.GetLength(); // BotId
            length += ShortName.GetTLLength(); // ShortName

            return length;
        }
    }
    public partial class TInputGeoPoint
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Lat
            length += 8; // Long
            if (flags.IsBitSet(0)) { length += 4; } // AccuracyRadius

            return length;
        }
    }
    public partial class TInputGeoPointEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputGroupCallInviteMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TInputGroupCallSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputGroupCallStream
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += 8; // TimeMs
            length += 4; // Scale
            if (flags.IsBitSet(0)) { length += 4; } // VideoChannel
            if (flags.IsBitSet(0)) { length += 4; } // VideoQuality

            return length;
        }
    }
    public partial class TInputInlineButtonTypeUrlAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += FwdText.GetTLLength(); } // FwdText
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(2)) { length += Bot.GetLength(); } // Bot

            return length;
        }
    }
    public partial class TInputInlineButtonTypeUserProfile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class TInputInvoiceBusinessBotTransferStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += 8; // Stars

            return length;
        }
    }
    public partial class TInputInvoiceChatInviteSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class TInputInvoiceMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TInputInvoicePremiumAuthCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class TInputInvoicePremiumGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose
            length += Option.GetLength(); // Option

            return length;
        }
    }
    public partial class TInputInvoicePremiumGiftStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += UserId.GetLength(); // UserId
            length += 4; // Months
            if (flags.IsBitSet(0)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TInputInvoiceSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputInvoiceStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // GiftId
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftAuctionBid
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += Peer.GetLength(); } // Peer
            length += 8; // GiftId
            length += 8; // BidAmount
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftDropOriginalDetails
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftPrepaidUpgrade
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftResale
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Slug.GetTLLength(); // Slug
            length += ToId.GetLength(); // ToId
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftTransfer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift
            length += ToId.GetLength(); // ToId

            return length;
        }
    }
    public partial class TInputInvoiceStarGiftUpgrade
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class TInputInvoiceStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class TInputMediaAreaChannelPost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += Channel.GetLength(); // Channel
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TInputMediaAreaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += 8; // QueryId
            length += ResultId.GetTLLength(); // ResultId

            return length;
        }
    }
    public partial class TInputMediaContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Vcard.GetTLLength(); // Vcard

            return length;
        }
    }
    public partial class TInputMediaDice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TInputMediaDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            if (flags.IsBitSet(3)) { length += VideoCover.GetLength(); } // VideoCover
            if (flags.IsBitSet(4)) { length += 4; } // VideoTimestamp
            if (flags.IsBitSet(0)) { length += 4; } // TtlSeconds
            if (flags.IsBitSet(1)) { length += Query.GetTLLength(); } // Query

            return length;
        }
    }
    public partial class TInputMediaDocumentExternal
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(0)) { length += 4; } // TtlSeconds
            if (flags.IsBitSet(2)) { length += VideoCover.GetLength(); } // VideoCover
            if (flags.IsBitSet(3)) { length += 4; } // VideoTimestamp

            return length;
        }
    }
    public partial class TInputMediaEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMediaGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class TInputMediaGeoLive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += GeoPoint.GetLength(); // GeoPoint
            if (flags.IsBitSet(2)) { length += 4; } // Heading
            if (flags.IsBitSet(1)) { length += 4; } // Period
            if (flags.IsBitSet(3)) { length += 4; } // ProximityNotificationRadius

            return length;
        }
    }
    public partial class TInputMediaGeoPoint
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GeoPoint.GetLength(); // GeoPoint

            return length;
        }
    }
    public partial class TInputMediaInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            length += Payload.GetTLLength(); // Payload
            if (flags.IsBitSet(3)) { length += Provider.GetTLLength(); } // Provider
            length += ProviderData.GetLength(); // ProviderData
            if (flags.IsBitSet(1)) { length += StartParam.GetTLLength(); } // StartParam
            if (flags.IsBitSet(2)) { length += ExtendedMedia.GetLength(); } // ExtendedMedia

            return length;
        }
    }
    public partial class TInputMediaPaidMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // StarsAmount
            length += ExtendedMedia.GetLength(); // ExtendedMedia
            if (flags.IsBitSet(0)) { length += Payload.GetTLLength(); } // Payload

            return length;
        }
    }
    public partial class TInputMediaPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            if (flags.IsBitSet(0)) { length += 4; } // TtlSeconds
            if (flags.IsBitSet(2)) { length += Video.GetLength(); } // Video

            return length;
        }
    }
    public partial class TInputMediaPhotoExternal
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(0)) { length += 4; } // TtlSeconds

            return length;
        }
    }
    public partial class TInputMediaPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Poll.GetLength(); // Poll
            if (flags.IsBitSet(0)) { length += CorrectAnswers.GetLength(); } // CorrectAnswers
            if (flags.IsBitSet(3)) { length += AttachedMedia.GetLength(); } // AttachedMedia
            if (flags.IsBitSet(1)) { length += Solution.GetTLLength(); } // Solution
            if (flags.IsBitSet(1)) { length += SolutionEntities.GetLength(); } // SolutionEntities
            if (flags.IsBitSet(2)) { length += SolutionMedia.GetLength(); } // SolutionMedia

            return length;
        }
    }
    public partial class TInputMediaStakeDice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GameHash.GetTLLength(); // GameHash
            length += 8; // TonAmount
            length += ClientSeed.GetTLLength(); // ClientSeed

            return length;
        }
    }
    public partial class TInputMediaStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class TInputMediaTodo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Todo.GetLength(); // Todo

            return length;
        }
    }
    public partial class TInputMediaUploadedDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += File.GetLength(); // File
            if (flags.IsBitSet(2)) { length += Thumb.GetLength(); } // Thumb
            length += MimeType.GetTLLength(); // MimeType
            length += Attributes.GetLength(); // Attributes
            if (flags.IsBitSet(0)) { length += Stickers.GetLength(); } // Stickers
            if (flags.IsBitSet(6)) { length += VideoCover.GetLength(); } // VideoCover
            if (flags.IsBitSet(7)) { length += 4; } // VideoTimestamp
            if (flags.IsBitSet(1)) { length += 4; } // TtlSeconds

            return length;
        }
    }
    public partial class TInputMediaUploadedPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += File.GetLength(); // File
            if (flags.IsBitSet(0)) { length += Stickers.GetLength(); } // Stickers
            if (flags.IsBitSet(1)) { length += 4; } // TtlSeconds
            if (flags.IsBitSet(3)) { length += Video.GetLength(); } // Video

            return length;
        }
    }
    public partial class TInputMediaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GeoPoint.GetLength(); // GeoPoint
            length += Title.GetTLLength(); // Title
            length += Address.GetTLLength(); // Address
            length += Provider.GetTLLength(); // Provider
            length += VenueId.GetTLLength(); // VenueId
            length += VenueType.GetTLLength(); // VenueType

            return length;
        }
    }
    public partial class TInputMediaWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TInputMessageCallbackQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 8; // QueryId

            return length;
        }
    }
    public partial class TInputMessageEntityMentionName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class TInputMessageID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TInputMessagePinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessageReadMetric
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId
            length += 8; // ViewId
            length += 4; // TimeInViewMs
            length += 4; // ActiveTimeInViewMs
            length += 4; // HeightToViewportRatioPermille
            length += 4; // SeenRangeRatioPermille

            return length;
        }
    }
    public partial class TInputMessageReplyTo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TInputMessagesFilterChatPhotos
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterGeo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterGif
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterMyMentions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterPhoneCalls
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TInputMessagesFilterPhotos
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterPhotoVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterPinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterRoundVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterRoundVoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputMessagesFilterVoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputNotifyBroadcasts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputNotifyChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputNotifyCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Community.GetLength(); // Community

            return length;
        }
    }
    public partial class TInputNotifyForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // TopMsgId

            return length;
        }
    }
    public partial class TInputNotifyPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TInputNotifyUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPageBlockMap
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Geo.GetLength(); // Geo
            length += 4; // Zoom
            length += 4; // W
            length += 4; // H
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TInputPasskeyCredentialFirebasePNV
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PnvToken.GetTLLength(); // PnvToken

            return length;
        }
    }
    public partial class TInputPasskeyCredentialPublicKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += RawId.GetTLLength(); // RawId
            length += Response.GetLength(); // Response

            return length;
        }
    }
    public partial class TInputPasskeyResponseLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ClientData.GetLength(); // ClientData
            length += AuthenticatorData.GetTLLength(); // AuthenticatorData
            length += Signature.GetTLLength(); // Signature
            length += UserHandle.GetTLLength(); // UserHandle

            return length;
        }
    }
    public partial class TInputPasskeyResponseRegister
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ClientData.GetLength(); // ClientData
            length += AttestationData.GetTLLength(); // AttestationData

            return length;
        }
    }
    public partial class TInputPaymentCredentials
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Data.GetLength(); // Data

            return length;
        }
    }
    public partial class TInputPaymentCredentialsApplePay
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PaymentData.GetLength(); // PaymentData

            return length;
        }
    }
    public partial class TInputPaymentCredentialsGooglePay
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PaymentToken.GetLength(); // PaymentToken

            return length;
        }
    }
    public partial class TInputPaymentCredentialsSaved
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += TmpPassword.GetTLLength(); // TmpPassword

            return length;
        }
    }
    public partial class TInputPeerChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputPeerChannelFromMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // ChannelId

            return length;
        }
    }
    public partial class TInputPeerChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class TInputPeerColorCollectible
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // CollectibleId

            return length;
        }
    }
    public partial class TInputPeerEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPeerNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // ShowPreviews
            if (flags.IsBitSet(1)) { length += 4; } // Silent
            if (flags.IsBitSet(2)) { length += 4; } // MuteUntil
            if (flags.IsBitSet(3)) { length += Sound.GetLength(); } // Sound
            if (flags.IsBitSet(6)) { length += 4; } // StoriesMuted
            if (flags.IsBitSet(7)) { length += 4; } // StoriesHideSender
            if (flags.IsBitSet(8)) { length += StoriesSound.GetLength(); } // StoriesSound

            return length;
        }
    }
    public partial class TInputPeerPhotoFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // PhotoId

            return length;
        }
    }
    public partial class TInputPeerSelf
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPeerUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputPeerUserFromMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // UserId

            return length;
        }
    }
    public partial class TInputPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputPhoneContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ClientId
            length += Phone.GetTLLength(); // Phone
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            if (flags.IsBitSet(0)) { length += Note.GetLength(); } // Note

            return length;
        }
    }
    public partial class TInputPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference

            return length;
        }
    }
    public partial class TInputPhotoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPhotoFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference
            length += ThumbSize.GetTLLength(); // ThumbSize

            return length;
        }
    }
    public partial class TInputPhotoLegacyFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference
            length += 8; // VolumeId
            length += 4; // LocalId
            length += 8; // Secret

            return length;
        }
    }
    public partial class TInputPollAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            if (flags.IsBitSet(0)) { length += Media.GetLength(); } // Media

            return length;
        }
    }
    public partial class TInputPrivacyKeyAbout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyAddedByPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyNoPaidMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyPhoneNumber
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyPhoneP2P
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeySavedMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyStarGiftsAutoSave
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyStatusTimestamp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyKeyVoiceMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowCloseFriends
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueAllowUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TInputPrivacyValueDisallowAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueDisallowBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueDisallowChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TInputPrivacyValueDisallowContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputPrivacyValueDisallowUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TInputQuickReplyShortcut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Shortcut.GetTLLength(); // Shortcut

            return length;
        }
    }
    public partial class TInputQuickReplyShortcutId
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId

            return length;
        }
    }
    public partial class TInputReplyToEphemeralMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TInputReplyToMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ReplyToMsgId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += ReplyToPeerId.GetLength(); } // ReplyToPeerId
            if (flags.IsBitSet(2)) { length += QuoteText.GetTLLength(); } // QuoteText
            if (flags.IsBitSet(3)) { length += QuoteEntities.GetLength(); } // QuoteEntities
            if (flags.IsBitSet(4)) { length += 4; } // QuoteOffset
            if (flags.IsBitSet(5)) { length += MonoforumPeerId.GetLength(); } // MonoforumPeerId
            if (flags.IsBitSet(6)) { length += 4; } // TodoItemId
            if (flags.IsBitSet(7)) { length += PollOption.GetTLLength(); } // PollOption

            return length;
        }
    }
    public partial class TInputReplyToMonoForum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MonoforumPeerId.GetLength(); // MonoforumPeerId

            return length;
        }
    }
    public partial class TInputReplyToStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // StoryId

            return length;
        }
    }
    public partial class TInputReportReasonChildAbuse
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonCopyright
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonFake
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonGeoIrrelevant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonIllegalDrugs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonOther
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonPersonalDetails
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonPornography
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputReportReasonViolence
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputRichFileDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Document.GetLength(); // Document

            return length;
        }
    }
    public partial class TInputRichFilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Photo.GetLength(); // Photo

            return length;
        }
    }
    public partial class TInputRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Blocks.GetLength(); // Blocks
            if (flags.IsBitSet(2)) { length += Photos.GetLength(); } // Photos
            if (flags.IsBitSet(3)) { length += Documents.GetLength(); } // Documents
            if (flags.IsBitSet(4)) { length += Users.GetLength(); } // Users

            return length;
        }
    }
    public partial class TInputRichMessageHTML
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Html.GetTLLength(); // Html
            if (flags.IsBitSet(2)) { length += Files.GetLength(); } // Files

            return length;
        }
    }
    public partial class TInputRichMessageMarkdown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Markdown.GetTLLength(); // Markdown
            if (flags.IsBitSet(2)) { length += Files.GetLength(); } // Files

            return length;
        }
    }
    public partial class TInputSavedStarGiftChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // SavedId

            return length;
        }
    }
    public partial class TInputSavedStarGiftSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputSavedStarGiftUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TInputSecureFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputSecureFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputSecureFileUploaded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Parts
            length += Md5Checksum.GetTLLength(); // Md5Checksum
            length += FileHash.GetTLLength(); // FileHash
            length += Secret.GetTLLength(); // Secret

            return length;
        }
    }
    public partial class TInputSecureValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Type.GetLength(); // Type
            if (flags.IsBitSet(0)) { length += Data.GetLength(); } // Data
            if (flags.IsBitSet(1)) { length += FrontSide.GetLength(); } // FrontSide
            if (flags.IsBitSet(2)) { length += ReverseSide.GetLength(); } // ReverseSide
            if (flags.IsBitSet(3)) { length += Selfie.GetLength(); } // Selfie
            if (flags.IsBitSet(6)) { length += Translation.GetLength(); } // Translation
            if (flags.IsBitSet(4)) { length += Files.GetLength(); } // Files
            if (flags.IsBitSet(5)) { length += PlainData.GetLength(); } // PlainData

            return length;
        }
    }
    public partial class TInputSendMessageRichMessageDraftAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // RandomId
            length += RichMessage.GetLength(); // RichMessage

            return length;
        }
    }
    public partial class TInputSingleMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Media.GetLength(); // Media
            length += 8; // RandomId
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(0)) { length += Entities.GetLength(); } // Entities

            return length;
        }
    }
    public partial class TInputStarGiftAuction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId

            return length;
        }
    }
    public partial class TInputStarGiftAuctionSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputStarsTransaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id

            return length;
        }
    }
    public partial class TInputStickeredMediaDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class TInputStickeredMediaPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class TInputStickerSetAnimatedEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetAnimatedEmojiAnimations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetDice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TInputStickerSetEmojiChannelDefaultStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetEmojiDefaultStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetEmojiDefaultTopicIcons
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetEmojiGenericAnimations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputStickerSetItem
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Document.GetLength(); // Document
            length += Emoji.GetTLLength(); // Emoji
            if (flags.IsBitSet(0)) { length += MaskCoords.GetLength(); } // MaskCoords
            if (flags.IsBitSet(1)) { length += Keywords.GetTLLength(); } // Keywords

            return length;
        }
    }
    public partial class TInputStickerSetPremiumGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStickerSetShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ShortName.GetTLLength(); // ShortName

            return length;
        }
    }
    public partial class TInputStickerSetThumb
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += 4; // ThumbVersion

            return length;
        }
    }
    public partial class TInputStickerSetTonGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputStorePaymentAuthCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += 4; // PremiumDays
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TInputStorePaymentGiftPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TInputStorePaymentPremiumGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += BoostPeer.GetLength(); } // BoostPeer
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TInputStorePaymentPremiumGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += BoostPeer.GetLength(); // BoostPeer
            if (flags.IsBitSet(1)) { length += AdditionalPeers.GetLength(); } // AdditionalPeers
            if (flags.IsBitSet(2)) { length += CountriesIso2.GetLength(); } // CountriesIso2
            if (flags.IsBitSet(4)) { length += PrizeDescription.GetTLLength(); } // PrizeDescription
            length += 8; // RandomId
            length += 4; // UntilDate
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TInputStorePaymentPremiumSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TInputStorePaymentStarsGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += 8; // Stars
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TInputStorePaymentStarsGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            length += BoostPeer.GetLength(); // BoostPeer
            if (flags.IsBitSet(1)) { length += AdditionalPeers.GetLength(); } // AdditionalPeers
            if (flags.IsBitSet(2)) { length += CountriesIso2.GetLength(); } // CountriesIso2
            if (flags.IsBitSet(4)) { length += PrizeDescription.GetTLLength(); } // PrizeDescription
            length += 8; // RandomId
            length += 4; // UntilDate
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += 4; // Users

            return length;
        }
    }
    public partial class TInputStorePaymentStarsTopup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            if (flags.IsBitSet(0)) { length += SpendPurposePeer.GetLength(); } // SpendPurposePeer

            return length;
        }
    }
    public partial class TInputTakeoutFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputThemeSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += BaseTheme.GetLength(); // BaseTheme
            length += 4; // AccentColor
            if (flags.IsBitSet(3)) { length += 4; } // OutboxAccentColor
            if (flags.IsBitSet(0)) { length += MessageColors.GetLength(); } // MessageColors
            if (flags.IsBitSet(1)) { length += Wallpaper.GetLength(); } // Wallpaper
            if (flags.IsBitSet(1)) { length += WallpaperSettings.GetLength(); } // WallpaperSettings

            return length;
        }
    }
    public partial class TInputThemeSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputUserEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputUserFromMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // UserId

            return length;
        }
    }
    public partial class TInputUserSelf
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInputWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInputWallPaperNoFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TInputWallPaperSlug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TInputWebDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 4; // Size
            length += MimeType.GetTLLength(); // MimeType
            length += Attributes.GetLength(); // Attributes

            return length;
        }
    }
    public partial class TInputWebFileAudioAlbumThumbLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += Performer.GetTLLength(); } // Performer

            return length;
        }
    }
    public partial class TInputWebFileGeoPointLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GeoPoint.GetLength(); // GeoPoint
            length += 8; // AccessHash
            length += 4; // W
            length += 4; // H
            length += 4; // Zoom
            length += 4; // Scale

            return length;
        }
    }
    public partial class TInputWebFileLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 8; // AccessHash

            return length;
        }
    }
    public partial class TInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += Prices.GetLength(); // Prices
            if (flags.IsBitSet(8)) { length += 8; } // MaxTipAmount
            if (flags.IsBitSet(8)) { length += SuggestedTipAmounts.GetLength(); } // SuggestedTipAmounts
            if (flags.IsBitSet(10)) { length += TermsUrl.GetTLLength(); } // TermsUrl
            if (flags.IsBitSet(11)) { length += 4; } // SubscriptionPeriod

            return length;
        }
    }
    public partial class TJoinChatBotResultApproved
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TJoinChatBotResultDeclined
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TJoinChatBotResultQueued
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TJoinChatBotResultWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TJsonArray
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Value.GetLength(); // Value

            return length;
        }
    }
    public partial class TJsonBool
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Value

            return length;
        }
    }
    public partial class TJsonNull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TJsonNumber
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Value

            return length;
        }
    }
    public partial class TJsonObject
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Value.GetLength(); // Value

            return length;
        }
    }
    public partial class TJsonObjectValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetTLLength(); // Key
            length += Value.GetLength(); // Value

            return length;
        }
    }
    public partial class TJsonString
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Value.GetTLLength(); // Value

            return length;
        }
    }
    public partial class TKeyboardButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(10)) { length += Style.GetLength(); } // Style
            length += Text.GetTLLength(); // Text
            length += Type.GetLength(); // Type

            return length;
        }
    }
    public partial class TKeyboardButtonRow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Buttons.GetLength(); // Buttons

            return length;
        }
    }
    public partial class TKeyboardButtonStyle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += 8; } // Icon

            return length;
        }
    }
    public partial class TKeyboardInlineButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(10)) { length += Style.GetLength(); } // Style
            length += Text.GetTLLength(); // Text
            length += Type.GetLength(); // Type

            return length;
        }
    }
    public partial class TKeyboardInlineButtonRow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Buttons.GetLength(); // Buttons

            return length;
        }
    }
    public partial class TLabeledPrice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Label.GetTLLength(); // Label
            length += 8; // Amount

            return length;
        }
    }
    public partial class TLangPackDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode
            length += 4; // FromVersion
            length += 4; // Version
            length += Strings.GetLength(); // Strings

            return length;
        }
    }
    public partial class TLangPackLanguage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Name.GetTLLength(); // Name
            length += NativeName.GetTLLength(); // NativeName
            length += LangCode.GetTLLength(); // LangCode
            if (flags.IsBitSet(1)) { length += BaseLangCode.GetTLLength(); } // BaseLangCode
            length += PluralCode.GetTLLength(); // PluralCode
            length += 4; // StringsCount
            length += 4; // TranslatedCount
            length += TranslationsUrl.GetTLLength(); // TranslationsUrl

            return length;
        }
    }
    public partial class TLangPackString
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetTLLength(); // Key
            length += Value.GetTLLength(); // Value

            return length;
        }
    }
    public partial class TLangPackStringDeleted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetTLLength(); // Key

            return length;
        }
    }
    public partial class TLangPackStringPluralized
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Key.GetTLLength(); // Key
            if (flags.IsBitSet(0)) { length += ZeroValue.GetTLLength(); } // ZeroValue
            if (flags.IsBitSet(1)) { length += OneValue.GetTLLength(); } // OneValue
            if (flags.IsBitSet(2)) { length += TwoValue.GetTLLength(); } // TwoValue
            if (flags.IsBitSet(3)) { length += FewValue.GetTLLength(); } // FewValue
            if (flags.IsBitSet(4)) { length += ManyValue.GetTLLength(); } // ManyValue
            length += OtherValue.GetTLLength(); // OtherValue

            return length;
        }
    }
    public partial class TMaskCoords
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // N
            length += 8; // X
            length += 8; // Y
            length += 8; // Zoom

            return length;
        }
    }
    public partial class TMediaAreaChannelPost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += 8; // ChannelId
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TMediaAreaCoordinates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // X
            length += 8; // Y
            length += 8; // W
            length += 8; // H
            length += 8; // Rotation
            if (flags.IsBitSet(0)) { length += 8; } // Radius

            return length;
        }
    }
    public partial class TMediaAreaGeoPoint
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Coordinates.GetLength(); // Coordinates
            length += Geo.GetLength(); // Geo
            if (flags.IsBitSet(0)) { length += Address.GetLength(); } // Address

            return length;
        }
    }
    public partial class TMediaAreaStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TMediaAreaSuggestedReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Coordinates.GetLength(); // Coordinates
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class TMediaAreaUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TMediaAreaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += Geo.GetLength(); // Geo
            length += Title.GetTLLength(); // Title
            length += Address.GetTLLength(); // Address
            length += Provider.GetTLLength(); // Provider
            length += VenueId.GetTLLength(); // VenueId
            length += VenueType.GetTLLength(); // VenueType

            return length;
        }
    }
    public partial class TMediaAreaWeather
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Coordinates.GetLength(); // Coordinates
            length += Emoji.GetTLLength(); // Emoji
            length += 8; // TemperatureC
            length += 4; // Color

            return length;
        }
    }
    public partial class TMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 4; // Id
            if (flags.IsBitSet(8)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(29)) { length += 4; } // FromBoostsApplied
            if (flags2.IsBitSet(12)) { length += FromRank.GetTLLength(); } // FromRank
            length += PeerId.GetLength(); // PeerId
            if (flags.IsBitSet(28)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            if (flags.IsBitSet(2)) { length += FwdFrom.GetLength(); } // FwdFrom
            if (flags.IsBitSet(11)) { length += 8; } // ViaBotId
            if (flags2.IsBitSet(0)) { length += 8; } // ViaBusinessBotId
            if (flags2.IsBitSet(19)) { length += GuestchatViaFrom.GetLength(); } // GuestchatViaFrom
            if (flags.IsBitSet(3)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += 4; // Date
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(9)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(6)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(7)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(10)) { length += 4; } // Views
            if (flags.IsBitSet(10)) { length += 4; } // Forwards
            if (flags.IsBitSet(23)) { length += Replies.GetLength(); } // Replies
            if (flags.IsBitSet(15)) { length += 4; } // EditDate
            if (flags.IsBitSet(16)) { length += PostAuthor.GetTLLength(); } // PostAuthor
            if (flags.IsBitSet(17)) { length += 8; } // GroupedId
            if (flags.IsBitSet(20)) { length += Reactions.GetLength(); } // Reactions
            if (flags.IsBitSet(22)) { length += RestrictionReason.GetLength(); } // RestrictionReason
            if (flags.IsBitSet(25)) { length += 4; } // TtlPeriod
            if (flags.IsBitSet(30)) { length += 4; } // QuickReplyShortcutId
            if (flags2.IsBitSet(2)) { length += 8; } // Effect
            if (flags2.IsBitSet(3)) { length += Factcheck.GetLength(); } // Factcheck
            if (flags2.IsBitSet(5)) { length += 4; } // ReportDeliveryUntilDate
            if (flags2.IsBitSet(6)) { length += 8; } // PaidMessageStars
            if (flags2.IsBitSet(7)) { length += SuggestedPost.GetLength(); } // SuggestedPost
            if (flags2.IsBitSet(10)) { length += 4; } // ScheduleRepeatPeriod
            if (flags2.IsBitSet(11)) { length += SummaryFromLanguage.GetTLLength(); } // SummaryFromLanguage
            if (flags2.IsBitSet(13)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class TMessageActionBoostApply
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Boosts

            return length;
        }
    }
    public partial class TMessageActionBotAllowed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Domain.GetTLLength(); } // Domain
            if (flags.IsBitSet(2)) { length += App.GetLength(); } // App

            return length;
        }
    }
    public partial class TMessageActionChangeCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // CommunityId

            return length;
        }
    }
    public partial class TMessageActionChangeCreator
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // NewCreatorId

            return length;
        }
    }
    public partial class TMessageActionChannelCreate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TMessageActionChannelMigrateFrom
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += 8; // ChatId

            return length;
        }
    }
    public partial class TMessageActionChatAddUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessageActionChatCreate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessageActionChatDeletePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionChatDeleteUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TMessageActionChatEditPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Photo.GetLength(); // Photo

            return length;
        }
    }
    public partial class TMessageActionChatEditTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TMessageActionChatJoinedByLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // InviterId

            return length;
        }
    }
    public partial class TMessageActionChatJoinedByRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionChatJoinedViaCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // CommunityId

            return length;
        }
    }
    public partial class TMessageActionChatMigrateTo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId

            return length;
        }
    }
    public partial class TMessageActionConferenceCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CallId
            if (flags.IsBitSet(2)) { length += 4; } // Duration
            if (flags.IsBitSet(3)) { length += OtherParticipants.GetLength(); } // OtherParticipants

            return length;
        }
    }
    public partial class TMessageActionContactSignUp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionCustomAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class TMessageActionEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionGameScore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GameId
            length += 4; // Score

            return length;
        }
    }
    public partial class TMessageActionGeoProximityReached
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FromId.GetLength(); // FromId
            length += ToId.GetLength(); // ToId
            length += 4; // Distance

            return length;
        }
    }
    public partial class TMessageActionGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += BoostPeer.GetLength(); } // BoostPeer
            length += 4; // Days
            length += Slug.GetTLLength(); // Slug
            if (flags.IsBitSet(2)) { length += Currency.GetTLLength(); } // Currency
            if (flags.IsBitSet(2)) { length += 8; } // Amount
            if (flags.IsBitSet(3)) { length += CryptoCurrency.GetTLLength(); } // CryptoCurrency
            if (flags.IsBitSet(3)) { length += 8; } // CryptoAmount
            if (flags.IsBitSet(4)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TMessageActionGiftPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += 4; // Days
            if (flags.IsBitSet(0)) { length += CryptoCurrency.GetTLLength(); } // CryptoCurrency
            if (flags.IsBitSet(0)) { length += 8; } // CryptoAmount
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TMessageActionGiftStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += 8; // Stars
            if (flags.IsBitSet(0)) { length += CryptoCurrency.GetTLLength(); } // CryptoCurrency
            if (flags.IsBitSet(0)) { length += 8; } // CryptoAmount
            if (flags.IsBitSet(1)) { length += TransactionId.GetTLLength(); } // TransactionId

            return length;
        }
    }
    public partial class TMessageActionGiftTon
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += CryptoCurrency.GetTLLength(); // CryptoCurrency
            length += 8; // CryptoAmount
            if (flags.IsBitSet(0)) { length += TransactionId.GetTLLength(); } // TransactionId

            return length;
        }
    }
    public partial class TMessageActionGiveawayLaunch
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // Stars

            return length;
        }
    }
    public partial class TMessageActionGiveawayResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // WinnersCount
            length += 4; // UnclaimedCount

            return length;
        }
    }
    public partial class TMessageActionGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            if (flags.IsBitSet(0)) { length += 4; } // Duration

            return length;
        }
    }
    public partial class TMessageActionGroupCallScheduled
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // ScheduleDate

            return length;
        }
    }
    public partial class TMessageActionHistoryClear
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionInviteToGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessageActionManagedBotCreated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId

            return length;
        }
    }
    public partial class TMessageActionNewCreatorPending
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // NewCreatorId

            return length;
        }
    }
    public partial class TMessageActionNoForwardsRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // PrevValue
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TMessageActionNoForwardsToggle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // PrevValue
            length += 4; // NewValue

            return length;
        }
    }
    public partial class TMessageActionPaidMessagesPrice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars

            return length;
        }
    }
    public partial class TMessageActionPaidMessagesRefunded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += 8; // Stars

            return length;
        }
    }
    public partial class TMessageActionPaymentRefunded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            if (flags.IsBitSet(0)) { length += Payload.GetTLLength(); } // Payload
            length += Charge.GetLength(); // Charge

            return length;
        }
    }
    public partial class TMessageActionPaymentSent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            if (flags.IsBitSet(0)) { length += InvoiceSlug.GetTLLength(); } // InvoiceSlug
            if (flags.IsBitSet(4)) { length += 4; } // SubscriptionUntilDate

            return length;
        }
    }
    public partial class TMessageActionPaymentSentMe
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            length += Payload.GetTLLength(); // Payload
            if (flags.IsBitSet(0)) { length += Info.GetLength(); } // Info
            if (flags.IsBitSet(1)) { length += ShippingOptionId.GetTLLength(); } // ShippingOptionId
            length += Charge.GetLength(); // Charge
            if (flags.IsBitSet(4)) { length += 4; } // SubscriptionUntilDate

            return length;
        }
    }
    public partial class TMessageActionPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CallId
            if (flags.IsBitSet(0)) { length += Reason.GetLength(); } // Reason
            if (flags.IsBitSet(1)) { length += 4; } // Duration

            return length;
        }
    }
    public partial class TMessageActionPinMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionPollAppendAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Answer.GetLength(); // Answer

            return length;
        }
    }
    public partial class TMessageActionPollDeleteAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Answer.GetLength(); // Answer

            return length;
        }
    }
    public partial class TMessageActionPrizeStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            length += TransactionId.GetTLLength(); // TransactionId
            length += BoostPeer.GetLength(); // BoostPeer
            length += 4; // GiveawayMsgId

            return length;
        }
    }
    public partial class TMessageActionRequestedPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ButtonId
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class TMessageActionRequestedPeerSentMe
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ButtonId
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class TMessageActionScreenshotTaken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageActionSecureValuesSent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Types.GetLength(); // Types

            return length;
        }
    }
    public partial class TMessageActionSecureValuesSentMe
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Values.GetLength(); // Values
            length += Credentials.GetLength(); // Credentials

            return length;
        }
    }
    public partial class TMessageActionSetChatTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Theme.GetLength(); // Theme

            return length;
        }
    }
    public partial class TMessageActionSetChatWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Wallpaper.GetLength(); // Wallpaper

            return length;
        }
    }
    public partial class TMessageActionSetMessagesTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Period
            if (flags.IsBitSet(0)) { length += 8; } // AutoSettingFrom

            return length;
        }
    }
    public partial class TMessageActionStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Gift.GetLength(); // Gift
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message
            if (flags.IsBitSet(4)) { length += 8; } // ConvertStars
            if (flags.IsBitSet(5)) { length += 4; } // UpgradeMsgId
            if (flags.IsBitSet(8)) { length += 8; } // UpgradeStars
            if (flags.IsBitSet(11)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(12)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(12)) { length += 8; } // SavedId
            if (flags.IsBitSet(14)) { length += PrepaidUpgradeHash.GetTLLength(); } // PrepaidUpgradeHash
            if (flags.IsBitSet(15)) { length += 4; } // GiftMsgId
            if (flags.IsBitSet(18)) { length += ToId.GetLength(); } // ToId
            if (flags.IsBitSet(19)) { length += 4; } // GiftNum

            return length;
        }
    }
    public partial class TMessageActionStarGiftPurchaseOffer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Gift.GetLength(); // Gift
            length += Price.GetLength(); // Price
            length += 4; // ExpiresAt

            return length;
        }
    }
    public partial class TMessageActionStarGiftPurchaseOfferDeclined
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Gift.GetLength(); // Gift
            length += Price.GetLength(); // Price

            return length;
        }
    }
    public partial class TMessageActionStarGiftUnique
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Gift.GetLength(); // Gift
            if (flags.IsBitSet(3)) { length += 4; } // CanExportAt
            if (flags.IsBitSet(4)) { length += 8; } // TransferStars
            if (flags.IsBitSet(6)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(7)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(7)) { length += 8; } // SavedId
            if (flags.IsBitSet(8)) { length += ResaleAmount.GetLength(); } // ResaleAmount
            if (flags.IsBitSet(9)) { length += 4; } // CanTransferAt
            if (flags.IsBitSet(10)) { length += 4; } // CanResellAt
            if (flags.IsBitSet(12)) { length += 8; } // DropOriginalDetailsStars
            if (flags.IsBitSet(15)) { length += 4; } // CanCraftAt
            if (flags.IsBitSet(18)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TMessageActionSuggestBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Birthday.GetLength(); // Birthday

            return length;
        }
    }
    public partial class TMessageActionSuggestedPostApproval
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += RejectComment.GetTLLength(); } // RejectComment
            if (flags.IsBitSet(3)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(4)) { length += Price.GetLength(); } // Price

            return length;
        }
    }
    public partial class TMessageActionSuggestedPostRefund
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TMessageActionSuggestedPostSuccess
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Price.GetLength(); // Price

            return length;
        }
    }
    public partial class TMessageActionSuggestProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Photo.GetLength(); // Photo

            return length;
        }
    }
    public partial class TMessageActionTodoAppendTasks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += List.GetLength(); // List

            return length;
        }
    }
    public partial class TMessageActionTodoCompletions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Completed.GetLength(); // Completed
            length += Incompleted.GetLength(); // Incompleted

            return length;
        }
    }
    public partial class TMessageActionTopicCreate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += 4; // IconColor
            if (flags.IsBitSet(0)) { length += 8; } // IconEmojiId

            return length;
        }
    }
    public partial class TMessageActionTopicEdit
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += 8; } // IconEmojiId
            if (flags.IsBitSet(2)) { length += 4; } // Closed
            if (flags.IsBitSet(3)) { length += 4; } // Hidden

            return length;
        }
    }
    public partial class TMessageActionWebViewDataSent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TMessageActionWebViewDataSentMe
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class TMessageEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += PeerId.GetLength(); } // PeerId

            return length;
        }
    }
    public partial class TMessageEntityBankCard
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityBlockquote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityBold
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityBotCommand
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityCashtag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityCustomEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += 8; // DocumentId

            return length;
        }
    }
    public partial class TMessageEntityDiffDelete
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityDiffInsert
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityDiffReplace
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += OldText.GetTLLength(); // OldText

            return length;
        }
    }
    public partial class TMessageEntityEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityFormattedDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Offset
            length += 4; // Length
            length += 4; // Date

            return length;
        }
    }
    public partial class TMessageEntityHashtag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityItalic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityMention
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityMentionName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += 8; // UserId

            return length;
        }
    }
    public partial class TMessageEntityPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityPre
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += Language.GetTLLength(); // Language

            return length;
        }
    }
    public partial class TMessageEntitySpoiler
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityStrike
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityTextUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TMessageEntityUnderline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageEntityUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Length

            return length;
        }
    }
    public partial class TMessageExtendedMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class TMessageExtendedMediaPreview
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // W
            if (flags.IsBitSet(0)) { length += 4; } // H
            if (flags.IsBitSet(1)) { length += Thumb.GetLength(); } // Thumb
            if (flags.IsBitSet(2)) { length += 4; } // VideoDuration

            return length;
        }
    }
    public partial class TMessageFwdHeader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(5)) { length += FromName.GetTLLength(); } // FromName
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += 4; } // ChannelPost
            if (flags.IsBitSet(3)) { length += PostAuthor.GetTLLength(); } // PostAuthor
            if (flags.IsBitSet(4)) { length += SavedFromPeer.GetLength(); } // SavedFromPeer
            if (flags.IsBitSet(4)) { length += 4; } // SavedFromMsgId
            if (flags.IsBitSet(8)) { length += SavedFromId.GetLength(); } // SavedFromId
            if (flags.IsBitSet(9)) { length += SavedFromName.GetTLLength(); } // SavedFromName
            if (flags.IsBitSet(10)) { length += 4; } // SavedDate
            if (flags.IsBitSet(6)) { length += PsaType.GetTLLength(); } // PsaType

            return length;
        }
    }
    public partial class TMessageMediaContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Vcard.GetTLLength(); // Vcard
            length += 8; // UserId

            return length;
        }
    }
    public partial class TMessageMediaDice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Value
            length += Emoticon.GetTLLength(); // Emoticon
            if (flags.IsBitSet(0)) { length += GameOutcome.GetLength(); } // GameOutcome

            return length;
        }
    }
    public partial class TMessageMediaDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(5)) { length += AltDocuments.GetLength(); } // AltDocuments
            if (flags.IsBitSet(9)) { length += VideoCover.GetLength(); } // VideoCover
            if (flags.IsBitSet(10)) { length += 4; } // VideoTimestamp
            if (flags.IsBitSet(2)) { length += 4; } // TtlSeconds

            return length;
        }
    }
    public partial class TMessageMediaEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageMediaGame
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Game.GetLength(); // Game

            return length;
        }
    }
    public partial class TMessageMediaGeo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Geo.GetLength(); // Geo

            return length;
        }
    }
    public partial class TMessageMediaGeoLive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Geo.GetLength(); // Geo
            if (flags.IsBitSet(0)) { length += 4; } // Heading
            length += 4; // Period
            if (flags.IsBitSet(1)) { length += 4; } // ProximityNotificationRadius

            return length;
        }
    }
    public partial class TMessageMediaGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channels.GetLength(); // Channels
            if (flags.IsBitSet(1)) { length += CountriesIso2.GetLength(); } // CountriesIso2
            if (flags.IsBitSet(3)) { length += PrizeDescription.GetTLLength(); } // PrizeDescription
            length += 4; // Quantity
            if (flags.IsBitSet(4)) { length += 4; } // Months
            if (flags.IsBitSet(5)) { length += 8; } // Stars
            length += 4; // UntilDate

            return length;
        }
    }
    public partial class TMessageMediaGiveawayResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            if (flags.IsBitSet(3)) { length += 4; } // AdditionalPeersCount
            length += 4; // LaunchMsgId
            length += 4; // WinnersCount
            length += 4; // UnclaimedCount
            length += Winners.GetLength(); // Winners
            if (flags.IsBitSet(4)) { length += 4; } // Months
            if (flags.IsBitSet(5)) { length += 8; } // Stars
            if (flags.IsBitSet(1)) { length += PrizeDescription.GetTLLength(); } // PrizeDescription
            length += 4; // UntilDate

            return length;
        }
    }
    public partial class TMessageMediaInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(2)) { length += 4; } // ReceiptMsgId
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            length += StartParam.GetTLLength(); // StartParam
            if (flags.IsBitSet(4)) { length += ExtendedMedia.GetLength(); } // ExtendedMedia

            return length;
        }
    }
    public partial class TMessageMediaPaidMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // StarsAmount
            length += ExtendedMedia.GetLength(); // ExtendedMedia

            return length;
        }
    }
    public partial class TMessageMediaPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(2)) { length += 4; } // TtlSeconds
            if (flags.IsBitSet(4)) { length += Video.GetLength(); } // Video

            return length;
        }
    }
    public partial class TMessageMediaPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Poll.GetLength(); // Poll
            length += Results.GetLength(); // Results
            if (flags.IsBitSet(0)) { length += AttachedMedia.GetLength(); } // AttachedMedia

            return length;
        }
    }
    public partial class TMessageMediaStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Story.GetLength(); } // Story

            return length;
        }
    }
    public partial class TMessageMediaToDo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Todo.GetLength(); // Todo
            if (flags.IsBitSet(0)) { length += Completions.GetLength(); } // Completions

            return length;
        }
    }
    public partial class TMessageMediaUnsupported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TMessageMediaVenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Geo.GetLength(); // Geo
            length += Title.GetTLLength(); // Title
            length += Address.GetTLLength(); // Address
            length += Provider.GetTLLength(); // Provider
            length += VenueId.GetTLLength(); // VenueId
            length += VenueType.GetTLLength(); // VenueType

            return length;
        }
    }
    public partial class TMessageMediaVideoStream
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class TMessageMediaWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Webpage.GetLength(); // Webpage

            return length;
        }
    }
    public partial class TMessagePeerReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PeerId.GetLength(); // PeerId
            length += 4; // Date
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class TMessagePeerVote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Option.GetTLLength(); // Option
            length += 4; // Date

            return length;
        }
    }
    public partial class TMessagePeerVoteInputOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Date

            return length;
        }
    }
    public partial class TMessagePeerVoteMultiple
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Options.GetLength(); // Options
            length += 4; // Date

            return length;
        }
    }
    public partial class TMessageRange
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MinId
            length += 4; // MaxId

            return length;
        }
    }
    public partial class TMessageReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Results.GetLength(); // Results
            if (flags.IsBitSet(1)) { length += RecentReactions.GetLength(); } // RecentReactions
            if (flags.IsBitSet(4)) { length += TopReactors.GetLength(); } // TopReactors

            return length;
        }
    }
    public partial class TMessageReactor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += PeerId.GetLength(); } // PeerId
            length += 4; // Count

            return length;
        }
    }
    public partial class TMessageReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Replies
            length += 4; // RepliesPts
            if (flags.IsBitSet(1)) { length += RecentRepliers.GetLength(); } // RecentRepliers
            if (flags.IsBitSet(0)) { length += 8; } // ChannelId
            if (flags.IsBitSet(2)) { length += 4; } // MaxId
            if (flags.IsBitSet(3)) { length += 4; } // ReadMaxId

            return length;
        }
    }
    public partial class TMessageReplyHeader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += 4; } // ReplyToMsgId
            if (flags.IsBitSet(0)) { length += ReplyToPeerId.GetLength(); } // ReplyToPeerId
            if (flags.IsBitSet(5)) { length += ReplyFrom.GetLength(); } // ReplyFrom
            if (flags.IsBitSet(8)) { length += ReplyMedia.GetLength(); } // ReplyMedia
            if (flags.IsBitSet(1)) { length += 4; } // ReplyToTopId
            if (flags.IsBitSet(6)) { length += QuoteText.GetTLLength(); } // QuoteText
            if (flags.IsBitSet(7)) { length += QuoteEntities.GetLength(); } // QuoteEntities
            if (flags.IsBitSet(10)) { length += 4; } // QuoteOffset
            if (flags.IsBitSet(11)) { length += 4; } // TodoItemId
            if (flags.IsBitSet(12)) { length += PollOption.GetTLLength(); } // PollOption

            return length;
        }
    }
    public partial class TMessageReplyStoryHeader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // StoryId

            return length;
        }
    }
    public partial class TMessageReportOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Option.GetTLLength(); // Option

            return length;
        }
    }
    public partial class TMessageService
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            if (flags.IsBitSet(8)) { length += FromId.GetLength(); } // FromId
            length += PeerId.GetLength(); // PeerId
            if (flags.IsBitSet(28)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            if (flags.IsBitSet(3)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += 4; // Date
            length += Action.GetLength(); // Action
            if (flags.IsBitSet(20)) { length += Reactions.GetLength(); } // Reactions
            if (flags.IsBitSet(25)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TMessageViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Views
            if (flags.IsBitSet(1)) { length += 4; } // Forwards
            if (flags.IsBitSet(2)) { length += Replies.GetLength(); } // Replies

            return length;
        }
    }
    public partial class TMissingInvitee
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId

            return length;
        }
    }
    public partial class TMonoForumDialog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // TopMessage
            length += 4; // ReadInboxMaxId
            length += 4; // ReadOutboxMaxId
            length += 4; // UnreadCount
            length += 4; // UnreadReactionsCount
            if (flags.IsBitSet(1)) { length += Draft.GetLength(); } // Draft

            return length;
        }
    }
    public partial class TMsgContainer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            foreach (var item in Messages) { length += item.GetLength(); }; // Messages

            return length;
        }
    }
    public partial class TMsgCopy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += OrigMessage.GetLength(); // OrigMessage

            return length;
        }
    }
    public partial class TMsgDetailedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // MsgId
            length += 8; // AnswerMsgId
            length += 4; // Bytes
            length += 4; // Status

            return length;
        }
    }
    public partial class TMsgNewDetailedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // AnswerMsgId
            length += 4; // Bytes
            length += 4; // Status

            return length;
        }
    }
    public partial class TMsgResendReq
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MsgIds.GetLength(); // MsgIds

            return length;
        }
    }
    public partial class TMsgsAck
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MsgIds.GetLength(); // MsgIds

            return length;
        }
    }
    public partial class TMsgsAllInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MsgIds.GetLength(); // MsgIds
            length += Info.GetTLLength(); // Info

            return length;
        }
    }
    public partial class TMsgsStateInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ReqMsgId
            length += Info.GetTLLength(); // Info

            return length;
        }
    }
    public partial class TMsgsStateReq
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MsgIds.GetLength(); // MsgIds

            return length;
        }
    }
    public partial class TMyBoost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Slot
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            length += 4; // Date
            length += 4; // Expires
            if (flags.IsBitSet(1)) { length += 4; } // CooldownUntilDate

            return length;
        }
    }
    public partial class TNearestDc
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Country.GetTLLength(); // Country
            length += 4; // ThisDc
            length += 4; // NearestDc

            return length;
        }
    }
    public partial class TNewSessionCreated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // FirstMsgId
            length += 8; // UniqueId
            length += 8; // ServerSalt

            return length;
        }
    }
    public partial class TNotificationSoundDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TNotificationSoundLocal
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class TNotificationSoundNone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TNotificationSoundRingtone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TNotifyBroadcasts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TNotifyChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TNotifyCommunity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // CommunityId

            return length;
        }
    }
    public partial class TNotifyForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // TopMsgId

            return length;
        }
    }
    public partial class TNotifyPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TNotifyUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TNull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TOutboxReadDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date

            return length;
        }
    }
    public partial class TPQInnerData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Pq.GetTLLength(); // Pq
            length += P.GetTLLength(); // P
            length += Q.GetTLLength(); // Q
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 32; // NewNonce

            return length;
        }
    }
    public partial class TPQInnerDataDc
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Pq.GetTLLength(); // Pq
            length += P.GetTLLength(); // P
            length += Q.GetTLLength(); // Q
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 32; // NewNonce
            length += 4; // Dc

            return length;
        }
    }
    public partial class TPQInnerDataTemp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Pq.GetTLLength(); // Pq
            length += P.GetTLLength(); // P
            length += Q.GetTLLength(); // Q
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 32; // NewNonce
            length += 4; // ExpiresIn

            return length;
        }
    }
    public partial class TPQInnerDataTempDc
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Pq.GetTLLength(); // Pq
            length += P.GetTLLength(); // P
            length += Q.GetTLLength(); // Q
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 32; // NewNonce
            length += 4; // Dc
            length += 4; // ExpiresIn

            return length;
        }
    }
    public partial class TPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url
            length += Blocks.GetLength(); // Blocks
            length += Photos.GetLength(); // Photos
            length += Documents.GetLength(); // Documents
            if (flags.IsBitSet(3)) { length += 4; } // Views

            return length;
        }
    }
    public partial class TPageBlockAnchor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name

            return length;
        }
    }
    public partial class TPageBlockAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // AudioId
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockAuthorDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Author.GetLength(); // Author
            length += 4; // PublishedDate

            return length;
        }
    }
    public partial class TPageBlockBlockquote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockBlockquoteBlocks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Blocks.GetLength(); // Blocks
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockButtonRow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Buttons.GetLength(); // Buttons

            return length;
        }
    }
    public partial class TPageBlockChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class TPageBlockCollage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Items.GetLength(); // Items
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockCover
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Cover.GetLength(); // Cover

            return length;
        }
    }
    public partial class TPageBlockDetails
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Blocks.GetLength(); // Blocks
            length += Title.GetLength(); // Title

            return length;
        }
    }
    public partial class TPageBlockDivider
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPageBlockDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockEmbed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(2)) { length += Html.GetTLLength(); } // Html
            if (flags.IsBitSet(4)) { length += 8; } // PosterPhotoId
            if (flags.IsBitSet(5)) { length += 4; } // W
            if (flags.IsBitSet(5)) { length += 4; } // H
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockEmbedPost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 8; // WebpageId
            length += 8; // AuthorPhotoId
            length += Author.GetTLLength(); // Author
            length += 4; // Date
            length += Blocks.GetLength(); // Blocks
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockFooter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading1
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading2
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading3
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading4
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading5
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockHeading6
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockKicker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Items.GetLength(); // Items

            return length;
        }
    }
    public partial class TPageBlockMap
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Geo.GetLength(); // Geo
            length += 4; // Zoom
            length += 4; // W
            length += 4; // H
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockMath
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Source.GetTLLength(); // Source

            return length;
        }
    }
    public partial class TPageBlockOrderedList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Items.GetLength(); // Items
            if (flags.IsBitSet(0)) { length += 4; } // Start
            if (flags.IsBitSet(1)) { length += Type.GetTLLength(); } // Type

            return length;
        }
    }
    public partial class TPageBlockParagraph
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // PhotoId
            length += Caption.GetLength(); // Caption
            if (flags.IsBitSet(0)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(0)) { length += 8; } // WebpageId

            return length;
        }
    }
    public partial class TPageBlockPreformatted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Language.GetTLLength(); // Language

            return length;
        }
    }
    public partial class TPageBlockPullquote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockRelatedArticles
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetLength(); // Title
            length += Articles.GetLength(); // Articles

            return length;
        }
    }
    public partial class TPageBlockSlideshow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Items.GetLength(); // Items
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageBlockSubheader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockSubtitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockTable
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetLength(); // Title
            length += Rows.GetLength(); // Rows

            return length;
        }
    }
    public partial class TPageBlockThinking
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageBlockUnsupported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPageBlockVideo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // VideoId
            length += Caption.GetLength(); // Caption

            return length;
        }
    }
    public partial class TPageButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            length += Type.GetLength(); // Type
            if (flags.IsBitSet(0)) { length += Style.GetLength(); } // Style

            return length;
        }
    }
    public partial class TPageCaption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Credit.GetLength(); // Credit

            return length;
        }
    }
    public partial class TPageListItemBlocks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Blocks.GetLength(); // Blocks

            return length;
        }
    }
    public partial class TPageListItemText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TPageListOrderedItemBlocks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Num.GetTLLength(); } // Num
            length += Blocks.GetLength(); // Blocks
            if (flags.IsBitSet(3)) { length += 4; } // Value
            if (flags.IsBitSet(4)) { length += Type.GetTLLength(); } // Type

            return length;
        }
    }
    public partial class TPageListOrderedItemText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Num.GetTLLength(); } // Num
            length += Text.GetLength(); // Text
            if (flags.IsBitSet(3)) { length += 4; } // Value
            if (flags.IsBitSet(4)) { length += Type.GetTLLength(); } // Type

            return length;
        }
    }
    public partial class TPageRelatedArticle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Url.GetTLLength(); // Url
            length += 8; // WebpageId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(2)) { length += 8; } // PhotoId
            if (flags.IsBitSet(3)) { length += Author.GetTLLength(); } // Author
            if (flags.IsBitSet(4)) { length += 4; } // PublishedDate

            return length;
        }
    }
    public partial class TPageTableCell
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(7)) { length += Text.GetLength(); } // Text
            if (flags.IsBitSet(1)) { length += 4; } // Colspan
            if (flags.IsBitSet(2)) { length += 4; } // Rowspan

            return length;
        }
    }
    public partial class TPageTableRow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Cells.GetLength(); // Cells

            return length;
        }
    }
    public partial class TPaidReactionPrivacyAnonymous
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPaidReactionPrivacyDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPaidReactionPrivacyPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TPasskey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Name.GetTLLength(); // Name
            length += 4; // Date
            if (flags.IsBitSet(0)) { length += 8; } // SoftwareEmojiId
            if (flags.IsBitSet(1)) { length += 4; } // LastUsageDate

            return length;
        }
    }
    public partial class TPasswordKdfAlgoSHA256SHA256PBKDF2HMACSHA512iter100000SHA256ModPow
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Salt1.GetTLLength(); // Salt1
            length += Salt2.GetTLLength(); // Salt2
            length += 4; // G
            length += P.GetTLLength(); // P

            return length;
        }
    }
    public partial class TPasswordKdfAlgoUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPaymentCharge
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += ProviderChargeId.GetTLLength(); // ProviderChargeId

            return length;
        }
    }
    public partial class TPaymentFormMethod
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TPaymentRequestedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Name.GetTLLength(); } // Name
            if (flags.IsBitSet(1)) { length += Phone.GetTLLength(); } // Phone
            if (flags.IsBitSet(2)) { length += Email.GetTLLength(); } // Email
            if (flags.IsBitSet(3)) { length += ShippingAddress.GetLength(); } // ShippingAddress

            return length;
        }
    }
    public partial class TPaymentSavedCredentialsCard
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class TPeerBlocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PeerId.GetLength(); // PeerId
            length += 4; // Date

            return length;
        }
    }
    public partial class TPeerChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId

            return length;
        }
    }
    public partial class TPeerChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class TPeerColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Color
            if (flags.IsBitSet(1)) { length += 8; } // BackgroundEmojiId

            return length;
        }
    }
    public partial class TPeerColorCollectible
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // CollectibleId
            length += 8; // GiftEmojiId
            length += 8; // BackgroundEmojiId
            length += 4; // AccentColor
            length += Colors.GetLength(); // Colors
            if (flags.IsBitSet(0)) { length += 4; } // DarkAccentColor
            if (flags.IsBitSet(1)) { length += DarkColors.GetLength(); } // DarkColors

            return length;
        }
    }
    public partial class TPeerLocated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Expires
            length += 4; // Distance

            return length;
        }
    }
    public partial class TPeerNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // ShowPreviews
            if (flags.IsBitSet(1)) { length += 4; } // Silent
            if (flags.IsBitSet(2)) { length += 4; } // MuteUntil
            if (flags.IsBitSet(3)) { length += IosSound.GetLength(); } // IosSound
            if (flags.IsBitSet(4)) { length += AndroidSound.GetLength(); } // AndroidSound
            if (flags.IsBitSet(5)) { length += OtherSound.GetLength(); } // OtherSound
            if (flags.IsBitSet(6)) { length += 4; } // StoriesMuted
            if (flags.IsBitSet(7)) { length += 4; } // StoriesHideSender
            if (flags.IsBitSet(8)) { length += StoriesIosSound.GetLength(); } // StoriesIosSound
            if (flags.IsBitSet(9)) { length += StoriesAndroidSound.GetLength(); } // StoriesAndroidSound
            if (flags.IsBitSet(10)) { length += StoriesOtherSound.GetLength(); } // StoriesOtherSound

            return length;
        }
    }
    public partial class TPeerSelfLocated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires

            return length;
        }
    }
    public partial class TPeerSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(6)) { length += 4; } // GeoDistance
            if (flags.IsBitSet(9)) { length += RequestChatTitle.GetTLLength(); } // RequestChatTitle
            if (flags.IsBitSet(9)) { length += 4; } // RequestChatDate
            if (flags.IsBitSet(13)) { length += 8; } // BusinessBotId
            if (flags.IsBitSet(13)) { length += BusinessBotManageUrl.GetTLLength(); } // BusinessBotManageUrl
            if (flags.IsBitSet(14)) { length += 8; } // ChargePaidMessageStars
            if (flags.IsBitSet(15)) { length += RegistrationMonth.GetTLLength(); } // RegistrationMonth
            if (flags.IsBitSet(16)) { length += PhoneCountry.GetTLLength(); } // PhoneCountry
            if (flags.IsBitSet(17)) { length += 4; } // NameChangeDate
            if (flags.IsBitSet(18)) { length += 4; } // PhotoChangeDate

            return length;
        }
    }
    public partial class TPeerStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // MaxReadId
            length += Stories.GetLength(); // Stories

            return length;
        }
    }
    public partial class TPeerUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TPendingSuggestion
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Suggestion.GetTLLength(); // Suggestion
            length += Title.GetLength(); // Title
            length += Description.GetLength(); // Description
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += GAOrB.GetTLLength(); // GAOrB
            length += 8; // KeyFingerprint
            length += Protocol.GetLength(); // Protocol
            length += Connections.GetLength(); // Connections
            length += 4; // StartDate
            if (flags.IsBitSet(7)) { length += CustomParameters.GetLength(); } // CustomParameters

            return length;
        }
    }
    public partial class TPhoneCallAccepted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += GB.GetTLLength(); // GB
            length += Protocol.GetLength(); // Protocol

            return length;
        }
    }
    public partial class TPhoneCallDiscarded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            if (flags.IsBitSet(0)) { length += Reason.GetLength(); } // Reason
            if (flags.IsBitSet(1)) { length += 4; } // Duration

            return length;
        }
    }
    public partial class TPhoneCallDiscardReasonBusy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPhoneCallDiscardReasonDisconnect
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPhoneCallDiscardReasonHangup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPhoneCallDiscardReasonMigrateConferenceCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class TPhoneCallDiscardReasonMissed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPhoneCallEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TPhoneCallProtocol
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // MinLayer
            length += 4; // MaxLayer
            length += LibraryVersions.GetLength(); // LibraryVersions

            return length;
        }
    }
    public partial class TPhoneCallRequested
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += GAHash.GetTLLength(); // GAHash
            length += Protocol.GetLength(); // Protocol

            return length;
        }
    }
    public partial class TPhoneCallWaiting
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += 4; // Date
            length += 8; // AdminId
            length += 8; // ParticipantId
            length += Protocol.GetLength(); // Protocol
            if (flags.IsBitSet(0)) { length += 4; } // ReceiveDate

            return length;
        }
    }
    public partial class TPhoneConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Ip.GetTLLength(); // Ip
            length += Ipv6.GetTLLength(); // Ipv6
            length += 4; // Port
            length += PeerTag.GetTLLength(); // PeerTag

            return length;
        }
    }
    public partial class TPhoneConnectionWebrtc
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Ip.GetTLLength(); // Ip
            length += Ipv6.GetTLLength(); // Ipv6
            length += 4; // Port
            length += Username.GetTLLength(); // Username
            length += Password.GetTLLength(); // Password

            return length;
        }
    }
    public partial class TPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += FileReference.GetTLLength(); // FileReference
            length += 4; // Date
            length += Sizes.GetLength(); // Sizes
            if (flags.IsBitSet(1)) { length += VideoSizes.GetLength(); } // VideoSizes
            length += 4; // DcId

            return length;
        }
    }
    public partial class TPhotoCachedSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type
            length += 4; // W
            length += 4; // H
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TPhotoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TPhotoPathSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TPhotoSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type
            length += 4; // W
            length += 4; // H
            length += 4; // Size

            return length;
        }
    }
    public partial class TPhotoSizeEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type

            return length;
        }
    }
    public partial class TPhotoSizeProgressive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type
            length += 4; // W
            length += 4; // H
            length += Sizes.GetLength(); // Sizes

            return length;
        }
    }
    public partial class TPhotoStrippedSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetTLLength(); // Type
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TPoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 8; // Id
            length += 4; // Flags
            length += Question.GetLength(); // Question
            length += Answers.GetLength(); // Answers
            if (flags.IsBitSet(4)) { length += 4; } // ClosePeriod
            if (flags.IsBitSet(5)) { length += 4; } // CloseDate
            if (flags.IsBitSet(12)) { length += CountriesIso2.GetLength(); } // CountriesIso2
            length += 8; // Hash

            return length;
        }
    }
    public partial class TPollAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            length += Option.GetTLLength(); // Option
            if (flags.IsBitSet(0)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(1)) { length += AddedBy.GetLength(); } // AddedBy
            if (flags.IsBitSet(1)) { length += 4; } // Date

            return length;
        }
    }
    public partial class TPollAnswerVoters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Option.GetTLLength(); // Option
            if (flags.IsBitSet(2)) { length += 4; } // Voters
            if (flags.IsBitSet(2)) { length += RecentVoters.GetLength(); } // RecentVoters

            return length;
        }
    }
    public partial class TPollResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Results.GetLength(); } // Results
            if (flags.IsBitSet(2)) { length += 4; } // TotalVoters
            if (flags.IsBitSet(3)) { length += RecentVoters.GetLength(); } // RecentVoters
            if (flags.IsBitSet(4)) { length += Solution.GetTLLength(); } // Solution
            if (flags.IsBitSet(4)) { length += SolutionEntities.GetLength(); } // SolutionEntities
            if (flags.IsBitSet(5)) { length += SolutionMedia.GetLength(); } // SolutionMedia

            return length;
        }
    }
    public partial class TPong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // MsgId
            length += 8; // PingId

            return length;
        }
    }
    public partial class TPopularContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ClientId
            length += 4; // Importers

            return length;
        }
    }
    public partial class TPostAddress
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += StreetLine1.GetTLLength(); // StreetLine1
            length += StreetLine2.GetTLLength(); // StreetLine2
            length += City.GetTLLength(); // City
            length += State.GetTLLength(); // State
            length += CountryIso2.GetTLLength(); // CountryIso2
            length += PostCode.GetTLLength(); // PostCode

            return length;
        }
    }
    public partial class TPostInteractionCountersMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId
            length += 4; // Views
            length += 4; // Forwards
            length += 4; // Reactions

            return length;
        }
    }
    public partial class TPostInteractionCountersStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // StoryId
            length += 4; // Views
            length += 4; // Forwards
            length += 4; // Reactions

            return length;
        }
    }
    public partial class TPremiumGiftCodeOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Users
            length += 4; // Months
            if (flags.IsBitSet(0)) { length += StoreProduct.GetTLLength(); } // StoreProduct
            if (flags.IsBitSet(1)) { length += 4; } // StoreQuantity
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TPremiumSubscriptionOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += Transaction.GetTLLength(); } // Transaction
            length += 4; // Months
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += BotUrl.GetTLLength(); // BotUrl
            if (flags.IsBitSet(0)) { length += StoreProduct.GetTLLength(); } // StoreProduct

            return length;
        }
    }
    public partial class TPrepaidGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 4; // Months
            length += 4; // Quantity
            length += 4; // Date

            return length;
        }
    }
    public partial class TPrepaidStarsGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // Stars
            length += 4; // Quantity
            length += 4; // Boosts
            length += 4; // Date

            return length;
        }
    }
    public partial class TPrivacyKeyAbout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyAddedByPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyNoPaidMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyPhoneNumber
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyPhoneP2P
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeySavedMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyStarGiftsAutoSave
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyStatusTimestamp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyKeyVoiceMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TPrivacyValueAllowCloseFriends
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueAllowUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPrivacyValueDisallowAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueDisallowBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueDisallowChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TPrivacyValueDisallowContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPrivacyValueDisallowUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TProfileTabFiles
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabGifs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabLinks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabPosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TProfileTabVoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPublicForwardMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TPublicForwardStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Story.GetLength(); // Story

            return length;
        }
    }
    public partial class TQuickReply
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Shortcut.GetTLLength(); // Shortcut
            length += 4; // TopMessage
            length += 4; // Count

            return length;
        }
    }
    public partial class TReactionCount
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // ChosenOrder
            length += Reaction.GetLength(); // Reaction
            length += 4; // Count

            return length;
        }
    }
    public partial class TReactionCustomEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId

            return length;
        }
    }
    public partial class TReactionEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TReactionEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TReactionNotificationsFromAll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TReactionNotificationsFromContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TReactionPaid
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TReactionsNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += MessagesNotifyFrom.GetLength(); } // MessagesNotifyFrom
            if (flags.IsBitSet(1)) { length += StoriesNotifyFrom.GetLength(); } // StoriesNotifyFrom
            if (flags.IsBitSet(2)) { length += PollVotesNotifyFrom.GetLength(); } // PollVotesNotifyFrom
            length += Sound.GetLength(); // Sound
            length += 4; // ShowPreviews

            return length;
        }
    }
    public partial class TReadParticipantDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Date

            return length;
        }
    }
    public partial class TReceivedNotifyMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 4; // Flags

            return length;
        }
    }
    public partial class TRecentMeUrlChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 8; // ChatId

            return length;
        }
    }
    public partial class TRecentMeUrlChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += ChatInvite.GetLength(); // ChatInvite

            return length;
        }
    }
    public partial class TRecentMeUrlStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += Set.GetLength(); // Set

            return length;
        }
    }
    public partial class TRecentMeUrlUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TRecentMeUrlUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 8; // UserId

            return length;
        }
    }
    public partial class TRecentStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // MaxId

            return length;
        }
    }
    public partial class TReplyInlineMarkup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Rows.GetLength(); // Rows

            return length;
        }
    }
    public partial class TReplyKeyboardForceReply
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += Placeholder.GetTLLength(); } // Placeholder

            return length;
        }
    }
    public partial class TReplyKeyboardHide
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TReplyKeyboardMarkup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Rows.GetLength(); // Rows
            if (flags.IsBitSet(3)) { length += Placeholder.GetTLLength(); } // Placeholder

            return length;
        }
    }
    public partial class TReportResultAddComment
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Option.GetTLLength(); // Option

            return length;
        }
    }
    public partial class TReportResultChooseOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += Options.GetLength(); // Options

            return length;
        }
    }
    public partial class TReportResultReported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TRequestedPeerChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += Username.GetTLLength(); } // Username
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo

            return length;
        }
    }
    public partial class TRequestedPeerChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChatId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo

            return length;
        }
    }
    public partial class TRequestedPeerUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += FirstName.GetTLLength(); } // FirstName
            if (flags.IsBitSet(0)) { length += LastName.GetTLLength(); } // LastName
            if (flags.IsBitSet(1)) { length += Username.GetTLLength(); } // Username
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo

            return length;
        }
    }
    public partial class TRequestPeerTypeBroadcast
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += 4; } // HasUsername
            if (flags.IsBitSet(1)) { length += UserAdminRights.GetLength(); } // UserAdminRights
            if (flags.IsBitSet(2)) { length += BotAdminRights.GetLength(); } // BotAdminRights

            return length;
        }
    }
    public partial class TRequestPeerTypeChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += 4; } // HasUsername
            if (flags.IsBitSet(4)) { length += 4; } // Forum
            if (flags.IsBitSet(1)) { length += UserAdminRights.GetLength(); } // UserAdminRights
            if (flags.IsBitSet(2)) { length += BotAdminRights.GetLength(); } // BotAdminRights

            return length;
        }
    }
    public partial class TRequestPeerTypeCreateBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += SuggestedName.GetTLLength(); } // SuggestedName
            if (flags.IsBitSet(2)) { length += SuggestedUsername.GetTLLength(); } // SuggestedUsername

            return length;
        }
    }
    public partial class TRequestPeerTypeUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // Bot
            if (flags.IsBitSet(1)) { length += 4; } // Premium

            return length;
        }
    }
    public partial class TRequirementToContactEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TRequirementToContactPaidMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // StarsAmount

            return length;
        }
    }
    public partial class TRequirementToContactPremium
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TResPQ
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += Pq.GetTLLength(); // Pq
            length += ServerPublicKeyFingerprints.GetLength(); // ServerPublicKeyFingerprints

            return length;
        }
    }
    public partial class TRestrictionReason
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Platform.GetTLLength(); // Platform
            length += Reason.GetTLLength(); // Reason
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TRichButtonStyle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Blocks.GetLength(); // Blocks
            length += Photos.GetLength(); // Photos
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TRpcAnswerDropped
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // MsgId
            length += 4; // SeqNo
            length += 4; // Bytes

            return length;
        }
    }
    public partial class TRpcAnswerDroppedRunning
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TRpcAnswerUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TRpcError
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ErrorCode
            length += ErrorMessage.GetTLLength(); // ErrorMessage

            return length;
        }
    }
    public partial class TRpcResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ReqMsgId
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class TSavedDialog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // TopMessage

            return length;
        }
    }
    public partial class TSavedPhoneContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Phone.GetTLLength(); // Phone
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += 4; // Date

            return length;
        }
    }
    public partial class TSavedReactionTag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Reaction.GetLength(); // Reaction
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            length += 4; // Count

            return length;
        }
    }
    public partial class TSavedStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += FromId.GetLength(); } // FromId
            length += 4; // Date
            length += Gift.GetLength(); // Gift
            if (flags.IsBitSet(2)) { length += Message.GetLength(); } // Message
            if (flags.IsBitSet(3)) { length += 4; } // MsgId
            if (flags.IsBitSet(11)) { length += 8; } // SavedId
            if (flags.IsBitSet(4)) { length += 8; } // ConvertStars
            if (flags.IsBitSet(6)) { length += 8; } // UpgradeStars
            if (flags.IsBitSet(7)) { length += 4; } // CanExportAt
            if (flags.IsBitSet(8)) { length += 8; } // TransferStars
            if (flags.IsBitSet(13)) { length += 4; } // CanTransferAt
            if (flags.IsBitSet(14)) { length += 4; } // CanResellAt
            if (flags.IsBitSet(15)) { length += CollectionId.GetLength(); } // CollectionId
            if (flags.IsBitSet(16)) { length += PrepaidUpgradeHash.GetTLLength(); } // PrepaidUpgradeHash
            if (flags.IsBitSet(18)) { length += 8; } // DropOriginalDetailsStars
            if (flags.IsBitSet(19)) { length += 4; } // GiftNum
            if (flags.IsBitSet(20)) { length += 4; } // CanCraftAt

            return length;
        }
    }
    public partial class TSearchPostsFlood
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // TotalDaily
            length += 4; // Remains
            if (flags.IsBitSet(1)) { length += 4; } // WaitTill
            length += 8; // StarsAmount

            return length;
        }
    }
    public partial class TSearchResultPosition
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId
            length += 4; // Date
            length += 4; // Offset

            return length;
        }
    }
    public partial class TSearchResultsCalendarPeriod
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date
            length += 4; // MinMsgId
            length += 4; // MaxMsgId
            length += 4; // Count

            return length;
        }
    }
    public partial class TSecureCredentialsEncrypted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Data.GetTLLength(); // Data
            length += Hash.GetTLLength(); // Hash
            length += Secret.GetTLLength(); // Secret

            return length;
        }
    }
    public partial class TSecureData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Data.GetTLLength(); // Data
            length += DataHash.GetTLLength(); // DataHash
            length += Secret.GetTLLength(); // Secret

            return length;
        }
    }
    public partial class TSecureFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += 8; // AccessHash
            length += 8; // Size
            length += 4; // DcId
            length += 4; // Date
            length += FileHash.GetTLLength(); // FileHash
            length += Secret.GetTLLength(); // Secret

            return length;
        }
    }
    public partial class TSecureFileEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecurePasswordKdfAlgoPBKDF2HMACSHA512iter100000
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Salt.GetTLLength(); // Salt

            return length;
        }
    }
    public partial class TSecurePasswordKdfAlgoSHA512
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Salt.GetTLLength(); // Salt

            return length;
        }
    }
    public partial class TSecurePasswordKdfAlgoUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecurePlainEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Email.GetTLLength(); // Email

            return length;
        }
    }
    public partial class TSecurePlainPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Phone.GetTLLength(); // Phone

            return length;
        }
    }
    public partial class TSecureRequiredType
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Type.GetLength(); // Type

            return length;
        }
    }
    public partial class TSecureRequiredTypeOneOf
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Types.GetLength(); // Types

            return length;
        }
    }
    public partial class TSecureSecretSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += SecureAlgo.GetLength(); // SecureAlgo
            length += SecureSecret.GetTLLength(); // SecureSecret
            length += 8; // SecureSecretId

            return length;
        }
    }
    public partial class TSecureValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Type.GetLength(); // Type
            if (flags.IsBitSet(0)) { length += Data.GetLength(); } // Data
            if (flags.IsBitSet(1)) { length += FrontSide.GetLength(); } // FrontSide
            if (flags.IsBitSet(2)) { length += ReverseSide.GetLength(); } // ReverseSide
            if (flags.IsBitSet(3)) { length += Selfie.GetLength(); } // Selfie
            if (flags.IsBitSet(6)) { length += Translation.GetLength(); } // Translation
            if (flags.IsBitSet(4)) { length += Files.GetLength(); } // Files
            if (flags.IsBitSet(5)) { length += PlainData.GetLength(); } // PlainData
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class TSecureValueError
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += Hash.GetTLLength(); // Hash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += DataHash.GetTLLength(); // DataHash
            length += Field.GetTLLength(); // Field
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetTLLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorFiles
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorFrontSide
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetTLLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorReverseSide
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetTLLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorSelfie
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetTLLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorTranslationFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetTLLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueErrorTranslationFiles
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += FileHash.GetLength(); // FileHash
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSecureValueHash
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class TSecureValueTypeAddress
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeBankStatement
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeDriverLicense
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeIdentityCard
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeInternalPassport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypePassport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypePassportRegistration
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypePersonalDetails
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypePhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeRentalAgreement
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeTemporaryRegistration
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSecureValueTypeUtilityBill
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendAsPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TSendMessageCancelAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageChooseContactAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageChooseStickerAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageEmojiInteraction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon
            length += 4; // MsgId
            length += Interaction.GetLength(); // Interaction

            return length;
        }
    }
    public partial class TSendMessageEmojiInteractionSeen
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon

            return length;
        }
    }
    public partial class TSendMessageGamePlayAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageGeoLocationAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageHistoryImportAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TSendMessageRecordAudioAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageRecordRoundAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageRecordVideoAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageRichMessageDraftAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // RandomId
            length += RichMessage.GetLength(); // RichMessage

            return length;
        }
    }
    public partial class TSendMessageStopDraftAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // RandomId

            return length;
        }
    }
    public partial class TSendMessageTextDraftAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // RandomId
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TSendMessageTypingAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendMessageUploadAudioAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TSendMessageUploadDocumentAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TSendMessageUploadPhotoAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TSendMessageUploadRoundAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TSendMessageUploadVideoAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Progress

            return length;
        }
    }
    public partial class TServerDHInnerData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += 4; // G
            length += DhPrime.GetTLLength(); // DhPrime
            length += GA.GetTLLength(); // GA
            length += 4; // ServerTime

            return length;
        }
    }
    public partial class TServerDHParamsOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += EncryptedAnswer.GetTLLength(); // EncryptedAnswer

            return length;
        }
    }
    public partial class TShippingOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Title.GetTLLength(); // Title
            length += Prices.GetLength(); // Prices

            return length;
        }
    }
    public partial class TSmsJob
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += JobId.GetTLLength(); // JobId
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TSpeakingInGroupCallAction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSponsoredMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += RandomId.GetTLLength(); // RandomId
            length += Url.GetTLLength(); // Url
            length += Title.GetTLLength(); // Title
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(6)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(14)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(13)) { length += Color.GetLength(); } // Color
            length += ButtonText.GetTLLength(); // ButtonText
            if (flags.IsBitSet(7)) { length += SponsorInfo.GetTLLength(); } // SponsorInfo
            if (flags.IsBitSet(8)) { length += AdditionalInfo.GetTLLength(); } // AdditionalInfo
            if (flags.IsBitSet(15)) { length += 4; } // MinDisplayDuration
            if (flags.IsBitSet(15)) { length += 4; } // MaxDisplayDuration

            return length;
        }
    }
    public partial class TSponsoredMessageReportOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Option.GetTLLength(); // Option

            return length;
        }
    }
    public partial class TSponsoredPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += RandomId.GetTLLength(); // RandomId
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += SponsorInfo.GetTLLength(); } // SponsorInfo
            if (flags.IsBitSet(1)) { length += AdditionalInfo.GetTLLength(); } // AdditionalInfo

            return length;
        }
    }
    public partial class TStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Sticker.GetLength(); // Sticker
            length += 8; // Stars
            if (flags.IsBitSet(0)) { length += 4; } // AvailabilityRemains
            if (flags.IsBitSet(0)) { length += 4; } // AvailabilityTotal
            if (flags.IsBitSet(4)) { length += 8; } // AvailabilityResale
            length += 8; // ConvertStars
            if (flags.IsBitSet(1)) { length += 4; } // FirstSaleDate
            if (flags.IsBitSet(1)) { length += 4; } // LastSaleDate
            if (flags.IsBitSet(3)) { length += 8; } // UpgradeStars
            if (flags.IsBitSet(4)) { length += 8; } // ResellMinStars
            if (flags.IsBitSet(5)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(6)) { length += ReleasedBy.GetLength(); } // ReleasedBy
            if (flags.IsBitSet(8)) { length += 4; } // PerUserTotal
            if (flags.IsBitSet(8)) { length += 4; } // PerUserRemains
            if (flags.IsBitSet(9)) { length += 4; } // LockedUntilDate
            if (flags.IsBitSet(11)) { length += AuctionSlug.GetTLLength(); } // AuctionSlug
            if (flags.IsBitSet(11)) { length += 4; } // GiftsPerRound
            if (flags.IsBitSet(11)) { length += 4; } // AuctionStartDate
            if (flags.IsBitSet(12)) { length += 4; } // UpgradeVariants
            if (flags.IsBitSet(13)) { length += Background.GetLength(); } // Background

            return length;
        }
    }
    public partial class TStarGiftActiveAuctionState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift
            length += State.GetLength(); // State
            length += UserState.GetLength(); // UserState

            return length;
        }
    }
    public partial class TStarGiftAttributeBackdrop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name
            length += 4; // BackdropId
            length += 4; // CenterColor
            length += 4; // EdgeColor
            length += 4; // PatternColor
            length += 4; // TextColor
            length += Rarity.GetLength(); // Rarity

            return length;
        }
    }
    public partial class TStarGiftAttributeCounter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Attribute.GetLength(); // Attribute
            length += 4; // Count

            return length;
        }
    }
    public partial class TStarGiftAttributeIdBackdrop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // BackdropId

            return length;
        }
    }
    public partial class TStarGiftAttributeIdModel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId

            return length;
        }
    }
    public partial class TStarGiftAttributeIdPattern
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId

            return length;
        }
    }
    public partial class TStarGiftAttributeModel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Name.GetTLLength(); // Name
            length += Document.GetLength(); // Document
            length += Rarity.GetLength(); // Rarity

            return length;
        }
    }
    public partial class TStarGiftAttributeOriginalDetails
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += SenderId.GetLength(); } // SenderId
            length += RecipientId.GetLength(); // RecipientId
            length += 4; // Date
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class TStarGiftAttributePattern
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name
            length += Document.GetLength(); // Document
            length += Rarity.GetLength(); // Rarity

            return length;
        }
    }
    public partial class TStarGiftAttributeRarity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Permille

            return length;
        }
    }
    public partial class TStarGiftAttributeRarityEpic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAttributeRarityLegendary
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAttributeRarityRare
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAttributeRarityUncommon
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAuctionAcquiredGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Date
            length += 8; // BidAmount
            length += 4; // Round
            length += 4; // Pos
            if (flags.IsBitSet(1)) { length += Message.GetLength(); } // Message
            if (flags.IsBitSet(2)) { length += 4; } // GiftNum

            return length;
        }
    }
    public partial class TStarGiftAuctionRound
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Num
            length += 4; // Duration

            return length;
        }
    }
    public partial class TStarGiftAuctionRoundExtendable
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Num
            length += 4; // Duration
            length += 4; // ExtendTop
            length += 4; // ExtendWindow

            return length;
        }
    }
    public partial class TStarGiftAuctionState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Version
            length += 4; // StartDate
            length += 4; // EndDate
            length += 8; // MinBidAmount
            length += BidLevels.GetLength(); // BidLevels
            length += TopBidders.GetLength(); // TopBidders
            length += 4; // NextRoundAt
            length += 4; // LastGiftNum
            length += 4; // GiftsLeft
            length += 4; // CurrentRound
            length += 4; // TotalRounds
            length += Rounds.GetLength(); // Rounds

            return length;
        }
    }
    public partial class TStarGiftAuctionStateFinished
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // StartDate
            length += 4; // EndDate
            length += 8; // AveragePrice
            if (flags.IsBitSet(0)) { length += 4; } // ListedCount
            if (flags.IsBitSet(1)) { length += 4; } // FragmentListedCount
            if (flags.IsBitSet(1)) { length += FragmentListedUrl.GetTLLength(); } // FragmentListedUrl

            return length;
        }
    }
    public partial class TStarGiftAuctionStateNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAuctionUserState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // BidAmount
            if (flags.IsBitSet(0)) { length += 4; } // BidDate
            if (flags.IsBitSet(0)) { length += 8; } // MinBidAmount
            if (flags.IsBitSet(0)) { length += BidPeer.GetLength(); } // BidPeer
            length += 4; // AcquiredCount

            return length;
        }
    }
    public partial class TStarGiftBackground
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // CenterColor
            length += 4; // EdgeColor
            length += 4; // TextColor

            return length;
        }
    }
    public partial class TStarGiftCollection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // CollectionId
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += Icon.GetLength(); } // Icon
            length += 4; // GiftsCount
            length += 8; // Hash

            return length;
        }
    }
    public partial class TStarGiftUnique
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // GiftId
            length += Title.GetTLLength(); // Title
            length += Slug.GetTLLength(); // Slug
            length += 4; // Num
            if (flags.IsBitSet(0)) { length += OwnerId.GetLength(); } // OwnerId
            if (flags.IsBitSet(1)) { length += OwnerName.GetTLLength(); } // OwnerName
            if (flags.IsBitSet(2)) { length += OwnerAddress.GetTLLength(); } // OwnerAddress
            length += Attributes.GetLength(); // Attributes
            length += 4; // AvailabilityIssued
            length += 4; // AvailabilityTotal
            if (flags.IsBitSet(3)) { length += GiftAddress.GetTLLength(); } // GiftAddress
            if (flags.IsBitSet(4)) { length += ResellAmount.GetLength(); } // ResellAmount
            if (flags.IsBitSet(5)) { length += ReleasedBy.GetLength(); } // ReleasedBy
            if (flags.IsBitSet(8)) { length += 8; } // ValueAmount
            if (flags.IsBitSet(8)) { length += ValueCurrency.GetTLLength(); } // ValueCurrency
            if (flags.IsBitSet(8)) { length += 8; } // ValueUsdAmount
            if (flags.IsBitSet(10)) { length += ThemePeer.GetLength(); } // ThemePeer
            if (flags.IsBitSet(11)) { length += PeerColor.GetLength(); } // PeerColor
            if (flags.IsBitSet(12)) { length += HostId.GetLength(); } // HostId
            if (flags.IsBitSet(13)) { length += 4; } // OfferMinStars
            if (flags.IsBitSet(16)) { length += 4; } // CraftChancePermille

            return length;
        }
    }
    public partial class TStarGiftUpgradePrice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date
            length += 8; // UpgradeStars

            return length;
        }
    }
    public partial class TStarRefProgram
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // BotId
            length += 4; // CommissionPermille
            if (flags.IsBitSet(0)) { length += 4; } // DurationMonths
            if (flags.IsBitSet(1)) { length += 4; } // EndDate
            if (flags.IsBitSet(2)) { length += DailyRevenuePerUser.GetLength(); } // DailyRevenuePerUser

            return length;
        }
    }
    public partial class TStarsAmount
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Amount
            length += 4; // Nanos

            return length;
        }
    }
    public partial class TStarsGiftOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            if (flags.IsBitSet(0)) { length += StoreProduct.GetTLLength(); } // StoreProduct
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TStarsGiveawayOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            length += 4; // YearlyBoosts
            if (flags.IsBitSet(2)) { length += StoreProduct.GetTLLength(); } // StoreProduct
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += Winners.GetLength(); // Winners

            return length;
        }
    }
    public partial class TStarsGiveawayWinnersOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Users
            length += 8; // PerUserStars

            return length;
        }
    }
    public partial class TStarsRating
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Level
            length += 8; // CurrentLevelStars
            length += 8; // Stars
            if (flags.IsBitSet(0)) { length += 8; } // NextLevelStars

            return length;
        }
    }
    public partial class TStarsRevenueStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += CurrentBalance.GetLength(); // CurrentBalance
            length += AvailableBalance.GetLength(); // AvailableBalance
            length += OverallRevenue.GetLength(); // OverallRevenue
            if (flags.IsBitSet(1)) { length += 4; } // NextWithdrawalAt

            return length;
        }
    }
    public partial class TStarsSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Peer.GetLength(); // Peer
            length += 4; // UntilDate
            length += Pricing.GetLength(); // Pricing
            if (flags.IsBitSet(3)) { length += ChatInviteHash.GetTLLength(); } // ChatInviteHash
            if (flags.IsBitSet(4)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(5)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(6)) { length += InvoiceSlug.GetTLLength(); } // InvoiceSlug

            return length;
        }
    }
    public partial class TStarsSubscriptionPricing
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Period
            length += 8; // Amount

            return length;
        }
    }
    public partial class TStarsTonAmount
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Amount

            return length;
        }
    }
    public partial class TStarsTopupOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stars
            if (flags.IsBitSet(0)) { length += StoreProduct.GetTLLength(); } // StoreProduct
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TStarsTransaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetTLLength(); // Id
            length += Amount.GetLength(); // Amount
            length += 4; // Date
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(5)) { length += 4; } // TransactionDate
            if (flags.IsBitSet(5)) { length += TransactionUrl.GetTLLength(); } // TransactionUrl
            if (flags.IsBitSet(7)) { length += BotPayload.GetTLLength(); } // BotPayload
            if (flags.IsBitSet(8)) { length += 4; } // MsgId
            if (flags.IsBitSet(9)) { length += ExtendedMedia.GetLength(); } // ExtendedMedia
            if (flags.IsBitSet(12)) { length += 4; } // SubscriptionPeriod
            if (flags.IsBitSet(13)) { length += 4; } // GiveawayPostId
            if (flags.IsBitSet(14)) { length += Stargift.GetLength(); } // Stargift
            if (flags.IsBitSet(15)) { length += 4; } // FloodskipNumber
            if (flags.IsBitSet(16)) { length += 4; } // StarrefCommissionPermille
            if (flags.IsBitSet(17)) { length += StarrefPeer.GetLength(); } // StarrefPeer
            if (flags.IsBitSet(17)) { length += StarrefAmount.GetLength(); } // StarrefAmount
            if (flags.IsBitSet(19)) { length += 4; } // PaidMessages
            if (flags.IsBitSet(20)) { length += 4; } // PremiumGiftMonths
            if (flags.IsBitSet(23)) { length += 4; } // AdsProceedsFromDate
            if (flags.IsBitSet(23)) { length += 4; } // AdsProceedsToDate

            return length;
        }
    }
    public partial class TStarsTransactionPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TStarsTransactionPeerAds
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerAPI
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerAppStore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerFragment
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerPlayMarket
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerPremiumBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarsTransactionPeerUnsupported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStatsAbsValueAndPrev
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Current
            length += 8; // Previous

            return length;
        }
    }
    public partial class TStatsDateRangeDays
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MinDate
            length += 4; // MaxDate

            return length;
        }
    }
    public partial class TStatsGraph
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Json.GetLength(); // Json
            if (flags.IsBitSet(0)) { length += ZoomToken.GetTLLength(); } // ZoomToken

            return length;
        }
    }
    public partial class TStatsGraphAsync
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TStatsGraphError
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Error.GetTLLength(); // Error

            return length;
        }
    }
    public partial class TStatsGroupTopAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Deleted
            length += 4; // Kicked
            length += 4; // Banned

            return length;
        }
    }
    public partial class TStatsGroupTopInviter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Invitations

            return length;
        }
    }
    public partial class TStatsGroupTopPoster
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Messages
            length += 4; // AvgChars

            return length;
        }
    }
    public partial class TStatsPercentValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Part
            length += 8; // Total

            return length;
        }
    }
    public partial class TStatsURL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TStickerKeyword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId
            length += Keyword.GetLength(); // Keyword

            return length;
        }
    }
    public partial class TStickerPack
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // InstalledDate
            length += 8; // Id
            length += 8; // AccessHash
            length += Title.GetTLLength(); // Title
            length += ShortName.GetTLLength(); // ShortName
            if (flags.IsBitSet(4)) { length += Thumbs.GetLength(); } // Thumbs
            if (flags.IsBitSet(4)) { length += 4; } // ThumbDcId
            if (flags.IsBitSet(4)) { length += 4; } // ThumbVersion
            if (flags.IsBitSet(8)) { length += 8; } // ThumbDocumentId
            length += 4; // Count
            length += 4; // Hash

            return length;
        }
    }
    public partial class TStickerSetCovered
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Set.GetLength(); // Set
            length += Cover.GetLength(); // Cover

            return length;
        }
    }
    public partial class TStickerSetFullCovered
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Set.GetLength(); // Set
            length += Packs.GetLength(); // Packs
            length += Keywords.GetLength(); // Keywords
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TStickerSetMultiCovered
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Set.GetLength(); // Set
            length += Covers.GetLength(); // Covers

            return length;
        }
    }
    public partial class TStickerSetNoCovered
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Set.GetLength(); // Set

            return length;
        }
    }
    public partial class TStoriesStealthMode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // ActiveUntilDate
            if (flags.IsBitSet(1)) { length += 4; } // CooldownUntilDate

            return length;
        }
    }
    public partial class TStoryAlbum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // AlbumId
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += IconPhoto.GetLength(); } // IconPhoto
            if (flags.IsBitSet(1)) { length += IconVideo.GetLength(); } // IconVideo

            return length;
        }
    }
    public partial class TStoryFwdHeader
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += From.GetLength(); } // From
            if (flags.IsBitSet(1)) { length += FromName.GetTLLength(); } // FromName
            if (flags.IsBitSet(2)) { length += 4; } // StoryId

            return length;
        }
    }
    public partial class TStoryItem
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 4; // Date
            if (flags.IsBitSet(18)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(17)) { length += FwdFrom.GetLength(); } // FwdFrom
            length += 4; // ExpireDate
            if (flags.IsBitSet(0)) { length += Caption.GetTLLength(); } // Caption
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            length += Media.GetLength(); // Media
            if (flags.IsBitSet(14)) { length += MediaAreas.GetLength(); } // MediaAreas
            if (flags.IsBitSet(2)) { length += Privacy.GetLength(); } // Privacy
            if (flags.IsBitSet(3)) { length += Views.GetLength(); } // Views
            if (flags.IsBitSet(15)) { length += SentReaction.GetLength(); } // SentReaction
            if (flags.IsBitSet(19)) { length += Albums.GetLength(); } // Albums
            if (flags.IsBitSet(20)) { length += Music.GetLength(); } // Music

            return length;
        }
    }
    public partial class TStoryItemDeleted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id

            return length;
        }
    }
    public partial class TStoryItemSkipped
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 4; // Date
            length += 4; // ExpireDate

            return length;
        }
    }
    public partial class TStoryReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PeerId.GetLength(); // PeerId
            length += 4; // Date
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class TStoryReactionPublicForward
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TStoryReactionPublicRepost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PeerId.GetLength(); // PeerId
            length += Story.GetLength(); // Story

            return length;
        }
    }
    public partial class TStoryView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += Reaction.GetLength(); } // Reaction

            return length;
        }
    }
    public partial class TStoryViewPublicForward
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TStoryViewPublicRepost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PeerId.GetLength(); // PeerId
            length += Story.GetLength(); // Story

            return length;
        }
    }
    public partial class TStoryViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ViewsCount
            if (flags.IsBitSet(2)) { length += 4; } // ForwardsCount
            if (flags.IsBitSet(3)) { length += Reactions.GetLength(); } // Reactions
            if (flags.IsBitSet(4)) { length += 4; } // ReactionsCount
            if (flags.IsBitSet(0)) { length += RecentViewers.GetLength(); } // RecentViewers

            return length;
        }
    }
    public partial class TSuggestedPost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += Price.GetLength(); } // Price
            if (flags.IsBitSet(0)) { length += 4; } // ScheduleDate

            return length;
        }
    }
    public partial class TTextAnchor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Name.GetTLLength(); // Name

            return length;
        }
    }
    public partial class TTextAutoEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextAutoPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextAutoUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextBankCard
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextBold
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextBotCommand
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            length += Type.GetLength(); // Type
            if (flags.IsBitSet(0)) { length += Style.GetLength(); } // Style

            return length;
        }
    }
    public partial class TTextCashtag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextConcat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Texts.GetLength(); // Texts

            return length;
        }
    }
    public partial class TTextCustomEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId
            length += Alt.GetTLLength(); // Alt

            return length;
        }
    }
    public partial class TTextDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            length += 4; // Date

            return length;
        }
    }
    public partial class TTextDiff
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += OldText.GetLength(); // OldText

            return length;
        }
    }
    public partial class TTextEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Email.GetTLLength(); // Email

            return length;
        }
    }
    public partial class TTextEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTextFixed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextHashtag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextImage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // DocumentId
            length += 4; // W
            length += 4; // H

            return length;
        }
    }
    public partial class TTextItalic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextMarked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextMath
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Source.GetTLLength(); // Source

            return length;
        }
    }
    public partial class TTextMention
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextMentionName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += 8; // UserId

            return length;
        }
    }
    public partial class TTextPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Phone.GetTLLength(); // Phone

            return length;
        }
    }
    public partial class TTextPlain
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TTextSpoiler
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextStrike
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextSubscript
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextSuperscript
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextUnderline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class TTextUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetLength(); // Text
            length += Url.GetTLLength(); // Url
            length += 8; // WebpageId

            return length;
        }
    }
    public partial class TTextWithEntities
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Text.GetTLLength(); // Text
            length += Entities.GetLength(); // Entities

            return length;
        }
    }
    public partial class TTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += 8; // AccessHash
            length += Slug.GetTLLength(); // Slug
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(2)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(3)) { length += Settings.GetLength(); } // Settings
            if (flags.IsBitSet(6)) { length += Emoticon.GetTLLength(); } // Emoticon
            if (flags.IsBitSet(4)) { length += 4; } // InstallsCount

            return length;
        }
    }
    public partial class TThemeSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += BaseTheme.GetLength(); // BaseTheme
            length += 4; // AccentColor
            if (flags.IsBitSet(3)) { length += 4; } // OutboxAccentColor
            if (flags.IsBitSet(0)) { length += MessageColors.GetLength(); } // MessageColors
            if (flags.IsBitSet(1)) { length += Wallpaper.GetLength(); } // Wallpaper

            return length;
        }
    }
    public partial class TTimezone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += Name.GetTLLength(); // Name
            length += 4; // UtcOffset

            return length;
        }
    }
    public partial class TTodoCompletion
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += CompletedBy.GetLength(); // CompletedBy
            length += 4; // Date

            return length;
        }
    }
    public partial class TTodoItem
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += Title.GetLength(); // Title

            return length;
        }
    }
    public partial class TTodoList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetLength(); // Title
            length += List.GetLength(); // List

            return length;
        }
    }
    public partial class TTopPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // Rating

            return length;
        }
    }
    public partial class TTopPeerCategoryBotsApp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryBotsGuestChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryBotsInline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryBotsPM
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryCorrespondents
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryForwardChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryForwardUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeerCategoryPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Category.GetLength(); // Category
            length += 4; // Count
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class TTopPeerCategoryPhoneCalls
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTrue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateAiComposeTones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateAttachMenuBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateAutoSaveSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateBotBusinessConnect
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Connection.GetLength(); // Connection
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotCallbackQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // ChatInstance
            if (flags.IsBitSet(0)) { length += Data.GetTLLength(); } // Data
            if (flags.IsBitSet(1)) { length += GameShortName.GetTLLength(); } // GameShortName

            return length;
        }
    }
    public partial class TUpdateBotChatBoost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Boost.GetLength(); // Boost
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotChatInviteRequester
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Date
            length += 8; // UserId
            length += About.GetTLLength(); // About
            length += Invite.GetLength(); // Invite
            length += 4; // Qts
            if (flags.IsBitSet(0)) { length += 8; } // QueryId

            return length;
        }
    }
    public partial class TUpdateBotCommands
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // BotId
            length += Commands.GetLength(); // Commands

            return length;
        }
    }
    public partial class TUpdateBotDeleteBusinessMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += Peer.GetLength(); // Peer
            length += Messages.GetLength(); // Messages
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotEditBusinessMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(0)) { length += ReplyToMessage.GetLength(); } // ReplyToMessage
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotGuestChatQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(0)) { length += ReferenceMessages.GetLength(); } // ReferenceMessages
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotInlineQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            length += Query.GetTLLength(); // Query
            if (flags.IsBitSet(0)) { length += Geo.GetLength(); } // Geo
            if (flags.IsBitSet(1)) { length += PeerType.GetLength(); } // PeerType
            length += Offset.GetTLLength(); // Offset

            return length;
        }
    }
    public partial class TUpdateBotInlineSend
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += Query.GetTLLength(); // Query
            if (flags.IsBitSet(0)) { length += Geo.GetLength(); } // Geo
            length += Id.GetTLLength(); // Id
            if (flags.IsBitSet(1)) { length += MsgId.GetLength(); } // MsgId

            return length;
        }
    }
    public partial class TUpdateBotMenuButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId
            length += Button.GetLength(); // Button

            return length;
        }
    }
    public partial class TUpdateBotMessageReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 4; // Date
            length += Actor.GetLength(); // Actor
            length += OldReactions.GetLength(); // OldReactions
            length += NewReactions.GetLength(); // NewReactions
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotMessageReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 4; // Date
            length += Reactions.GetLength(); // Reactions
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotNewBusinessMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(0)) { length += ReplyToMessage.GetLength(); } // ReplyToMessage
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotPrecheckoutQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            length += Payload.GetTLLength(); // Payload
            if (flags.IsBitSet(0)) { length += Info.GetLength(); } // Info
            if (flags.IsBitSet(1)) { length += ShippingOptionId.GetTLLength(); } // ShippingOptionId
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount

            return length;
        }
    }
    public partial class TUpdateBotPurchasedPaidMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += Payload.GetTLLength(); // Payload
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotShippingQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += 8; // UserId
            length += Payload.GetTLLength(); // Payload
            length += ShippingAddress.GetLength(); // ShippingAddress

            return length;
        }
    }
    public partial class TUpdateBotStarsSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            length += Payload.GetTLLength(); // Payload
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotStopped
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 4; // Date
            length += 4; // Stopped
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateBotWebhookJSON
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Data.GetLength(); // Data

            return length;
        }
    }
    public partial class TUpdateBotWebhookJSONQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += Data.GetLength(); // Data
            length += 4; // Timeout

            return length;
        }
    }
    public partial class TUpdateBusinessBotCallbackQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(2)) { length += ReplyToMessage.GetLength(); } // ReplyToMessage
            length += 8; // ChatInstance
            if (flags.IsBitSet(0)) { length += Data.GetTLLength(); } // Data

            return length;
        }
    }
    public partial class TUpdateChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId

            return length;
        }
    }
    public partial class TUpdateChannelAvailableMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // AvailableMinId

            return length;
        }
    }
    public partial class TUpdateChannelMessageForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // Id
            length += 4; // Forwards

            return length;
        }
    }
    public partial class TUpdateChannelMessageViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // Id
            length += 4; // Views

            return length;
        }
    }
    public partial class TUpdateChannelParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            length += 4; // Date
            length += 8; // ActorId
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += PrevParticipant.GetLength(); } // PrevParticipant
            if (flags.IsBitSet(1)) { length += NewParticipant.GetLength(); } // NewParticipant
            if (flags.IsBitSet(2)) { length += Invite.GetLength(); } // Invite
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateChannelReadMessagesContents
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class TUpdateChannelTooLong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            if (flags.IsBitSet(0)) { length += 4; } // Pts

            return length;
        }
    }
    public partial class TUpdateChannelUserTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += FromId.GetLength(); // FromId
            length += Action.GetLength(); // Action

            return length;
        }
    }
    public partial class TUpdateChannelViewForumAsMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // Enabled

            return length;
        }
    }
    public partial class TUpdateChannelWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += Webpage.GetLength(); // Webpage
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class TUpdateChatDefaultBannedRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += DefaultBannedRights.GetLength(); // DefaultBannedRights
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateChatParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChatId
            length += 4; // Date
            length += 8; // ActorId
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += PrevParticipant.GetLength(); } // PrevParticipant
            if (flags.IsBitSet(1)) { length += NewParticipant.GetLength(); } // NewParticipant
            if (flags.IsBitSet(2)) { length += Invite.GetLength(); } // Invite
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateChatParticipantAdd
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += 8; // UserId
            length += 8; // InviterId
            length += 4; // Date
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateChatParticipantAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += 8; // UserId
            length += 4; // IsAdmin
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateChatParticipantDelete
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += 8; // UserId
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateChatParticipantRank
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += 8; // UserId
            length += Rank.GetTLLength(); // Rank
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateChatParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participants.GetLength(); // Participants

            return length;
        }
    }
    public partial class TUpdateChatUserTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += FromId.GetLength(); // FromId
            length += Action.GetLength(); // Action

            return length;
        }
    }
    public partial class TUpdateConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateContactsReset
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateDcOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += DcOptions.GetLength(); // DcOptions

            return length;
        }
    }
    public partial class TUpdateDeleteChannelMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += Messages.GetLength(); // Messages
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateDeleteEphemeralMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Ids.GetLength(); // Ids

            return length;
        }
    }
    public partial class TUpdateDeleteGroupCallMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class TUpdateDeleteMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Messages.GetLength(); // Messages
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateDeleteQuickReply
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId

            return length;
        }
    }
    public partial class TUpdateDeleteQuickReplyMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class TUpdateDeleteScheduledMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Messages.GetLength(); // Messages
            if (flags.IsBitSet(0)) { length += SentMessages.GetLength(); } // SentMessages

            return length;
        }
    }
    public partial class TUpdateDialogFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Filter.GetLength(); } // Filter

            return length;
        }
    }
    public partial class TUpdateDialogFilterOrder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class TUpdateDialogFilters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateDialogPinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // FolderId
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TUpdateDialogUnreadMark
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId

            return length;
        }
    }
    public partial class TUpdateDraftMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += Draft.GetLength(); // Draft

            return length;
        }
    }
    public partial class TUpdateEditChannelMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateEditEphemeralMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateEditMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateEmojiGameInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Info.GetLength(); // Info

            return length;
        }
    }
    public partial class TUpdateEncryptedChatTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ChatId

            return length;
        }
    }
    public partial class TUpdateEncryptedMessagesRead
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ChatId
            length += 4; // MaxDate
            length += 4; // Date

            return length;
        }
    }
    public partial class TUpdateEncryption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chat.GetLength(); // Chat
            length += 4; // Date

            return length;
        }
    }
    public partial class TUpdateEphemeralBotCallbackQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            length += 4; // MsgId
            length += Data.GetTLLength(); // Data
            if (flags.IsBitSet(1)) { length += 8; } // ChatInstance
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateFavedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateFolderPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FolderPeers.GetLength(); // FolderPeers
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateGeoLiveViewed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class TUpdateGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Peer.GetLength(); } // Peer
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class TUpdateGroupCallChainBlocks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // SubChainId
            length += Blocks.GetLength(); // Blocks
            length += 4; // NextOffset

            return length;
        }
    }
    public partial class TUpdateGroupCallConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Params.GetLength(); // Params

            return length;
        }
    }
    public partial class TUpdateGroupCallEncryptedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += FromId.GetLength(); // FromId
            length += EncryptedMessage.GetTLLength(); // EncryptedMessage

            return length;
        }
    }
    public partial class TUpdateGroupCallMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateGroupCallParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Participants.GetLength(); // Participants
            length += 4; // Version

            return length;
        }
    }
    public partial class TUpdateInlineBotCallbackQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += 8; // UserId
            length += MsgId.GetLength(); // MsgId
            length += 8; // ChatInstance
            if (flags.IsBitSet(0)) { length += Data.GetTLLength(); } // Data
            if (flags.IsBitSet(1)) { length += GameShortName.GetTLLength(); } // GameShortName

            return length;
        }
    }
    public partial class TUpdateJoinChatWebViewDecision
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // QueryId
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class TUpdateLangPack
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Difference.GetLength(); // Difference

            return length;
        }
    }
    public partial class TUpdateLangPackTooLong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class TUpdateLoginToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateManagedBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += 8; // BotId
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateMessageExtendedMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += ExtendedMedia.GetLength(); // ExtendedMedia

            return length;
        }
    }
    public partial class TUpdateMessageID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 8; // RandomId

            return length;
        }
    }
    public partial class TUpdateMessagePoll
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(1)) { length += 4; } // MsgId
            if (flags.IsBitSet(2)) { length += 4; } // TopMsgId
            length += 8; // PollId
            if (flags.IsBitSet(0)) { length += Poll.GetLength(); } // Poll
            length += Results.GetLength(); // Results

            return length;
        }
    }
    public partial class TUpdateMessagePollVote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PollId
            length += Peer.GetLength(); // Peer
            length += Options.GetLength(); // Options
            length += Positions.GetLength(); // Positions
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateMessageReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += Reactions.GetLength(); // Reactions

            return length;
        }
    }
    public partial class TUpdateMonoForumNoPaidException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            length += SavedPeerId.GetLength(); // SavedPeerId

            return length;
        }
    }
    public partial class TUpdateMoveStickerSetToTop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Stickerset

            return length;
        }
    }
    public partial class TUpdateNewAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash
            if (flags.IsBitSet(0)) { length += 4; } // Date
            if (flags.IsBitSet(0)) { length += Device.GetTLLength(); } // Device
            if (flags.IsBitSet(0)) { length += Location.GetTLLength(); } // Location

            return length;
        }
    }
    public partial class TUpdateNewBotConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // BotId
            if (flags.IsBitSet(1)) { length += 4; } // Date
            if (flags.IsBitSet(1)) { length += Device.GetTLLength(); } // Device
            if (flags.IsBitSet(1)) { length += Location.GetTLLength(); } // Location

            return length;
        }
    }
    public partial class TUpdateNewChannelMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateNewEncryptedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message
            length += 4; // Qts

            return length;
        }
    }
    public partial class TUpdateNewEphemeralMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateNewMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateNewQuickReply
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += QuickReply.GetLength(); // QuickReply

            return length;
        }
    }
    public partial class TUpdateNewScheduledMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateNewStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class TUpdateNewStoryReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // StoryId
            length += Peer.GetLength(); // Peer
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class TUpdateNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += NotifySettings.GetLength(); // NotifySettings

            return length;
        }
    }
    public partial class TUpdatePaidReactionPrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Private.GetLength(); // Private

            return length;
        }
    }
    public partial class TUpdatePeerBlocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PeerId.GetLength(); // PeerId

            return length;
        }
    }
    public partial class TUpdatePeerHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TUpdatePeerLocated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class TUpdatePeerSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class TUpdatePeerWallpaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Wallpaper.GetLength(); } // Wallpaper

            return length;
        }
    }
    public partial class TUpdatePendingJoinRequests
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // RequestsPending
            length += RecentRequesters.GetLength(); // RecentRequesters

            return length;
        }
    }
    public partial class TUpdatePhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneCall.GetLength(); // PhoneCall

            return length;
        }
    }
    public partial class TUpdatePhoneCallSignalingData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PhoneCallId
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class TUpdatePinnedChannelMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            length += Messages.GetLength(); // Messages
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdatePinnedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // FolderId
            if (flags.IsBitSet(0)) { length += Order.GetLength(); } // Order

            return length;
        }
    }
    public partial class TUpdatePinnedForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // TopicId

            return length;
        }
    }
    public partial class TUpdatePinnedForumTopics
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Order.GetLength(); } // Order

            return length;
        }
    }
    public partial class TUpdatePinnedMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Messages.GetLength(); // Messages
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdatePinnedSavedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Order.GetLength(); } // Order

            return length;
        }
    }
    public partial class TUpdatePrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetLength(); // Key
            length += Rules.GetLength(); // Rules

            return length;
        }
    }
    public partial class TUpdatePtsChanged
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateQuickReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += QuickReplies.GetLength(); // QuickReplies

            return length;
        }
    }
    public partial class TUpdateQuickReplyMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetLength(); // Message

            return length;
        }
    }
    public partial class TUpdateReadChannelDiscussionInbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChannelId
            length += 4; // TopMsgId
            length += 4; // ReadMaxId
            if (flags.IsBitSet(0)) { length += 8; } // BroadcastId
            if (flags.IsBitSet(0)) { length += 4; } // BroadcastPost

            return length;
        }
    }
    public partial class TUpdateReadChannelDiscussionOutbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // TopMsgId
            length += 4; // ReadMaxId

            return length;
        }
    }
    public partial class TUpdateReadChannelInbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // FolderId
            length += 8; // ChannelId
            length += 4; // MaxId
            length += 4; // StillUnreadCount
            length += 4; // Pts

            return length;
        }
    }
    public partial class TUpdateReadChannelOutbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += 4; // MaxId

            return length;
        }
    }
    public partial class TUpdateReadFeaturedEmojiStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateReadFeaturedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateReadHistoryInbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // FolderId
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += 4; } // TopMsgId
            length += 4; // MaxId
            length += 4; // StillUnreadCount
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateReadHistoryOutbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateReadMessagesContents
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Messages.GetLength(); // Messages
            length += 4; // Pts
            length += 4; // PtsCount
            if (flags.IsBitSet(0)) { length += 4; } // Date

            return length;
        }
    }
    public partial class TUpdateReadMonoForumInbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += SavedPeerId.GetLength(); // SavedPeerId
            length += 4; // ReadMaxId

            return length;
        }
    }
    public partial class TUpdateReadMonoForumOutbox
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChannelId
            length += SavedPeerId.GetLength(); // SavedPeerId
            length += 4; // ReadMaxId

            return length;
        }
    }
    public partial class TUpdateReadStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId

            return length;
        }
    }
    public partial class TUpdateRecentEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateRecentReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateRecentStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Updates.GetLength(); // Updates
            length += Users.GetLength(); // Users
            length += Chats.GetLength(); // Chats
            length += 4; // Date
            length += 4; // Seq

            return length;
        }
    }
    public partial class TUpdateSavedDialogPinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class TUpdateSavedGifs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateSavedReactionTags
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateSavedRingtones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdatesCombined
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Updates.GetLength(); // Updates
            length += Users.GetLength(); // Users
            length += Chats.GetLength(); // Chats
            length += 4; // Date
            length += 4; // SeqStart
            length += 4; // Seq

            return length;
        }
    }
    public partial class TUpdateSentPhoneCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += SentCode.GetLength(); // SentCode

            return length;
        }
    }
    public partial class TUpdateSentStoryReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // StoryId
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class TUpdateServiceNotification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // InboxDate
            length += Type.GetTLLength(); // Type
            length += Message.GetTLLength(); // Message
            length += Media.GetLength(); // Media
            length += Entities.GetLength(); // Entities

            return length;
        }
    }
    public partial class TUpdateShort
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Update.GetLength(); // Update
            length += 4; // Date

            return length;
        }
    }
    public partial class TUpdateShortChatMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 8; // FromId
            length += 8; // ChatId
            length += Message.GetTLLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += FwdFrom.GetLength(); } // FwdFrom
            if (flags.IsBitSet(11)) { length += 8; } // ViaBotId
            if (flags.IsBitSet(3)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(7)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(25)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TUpdateShortMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 8; // UserId
            length += Message.GetTLLength(); // Message
            length += 4; // Pts
            length += 4; // PtsCount
            length += 4; // Date
            if (flags.IsBitSet(2)) { length += FwdFrom.GetLength(); } // FwdFrom
            if (flags.IsBitSet(11)) { length += 8; } // ViaBotId
            if (flags.IsBitSet(3)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(7)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(25)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TUpdateShortSentMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += 4; // Pts
            length += 4; // PtsCount
            length += 4; // Date
            if (flags.IsBitSet(9)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(7)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(25)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class TUpdateSmsJob
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += JobId.GetTLLength(); // JobId

            return length;
        }
    }
    public partial class TUpdateStarGiftAuctionState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId
            length += State.GetLength(); // State

            return length;
        }
    }
    public partial class TUpdateStarGiftAuctionUserState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId
            length += UserState.GetLength(); // UserState

            return length;
        }
    }
    public partial class TUpdateStarGiftCraftFail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateStarsBalance
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Balance.GetLength(); // Balance

            return length;
        }
    }
    public partial class TUpdateStarsRevenueStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Status.GetLength(); // Status

            return length;
        }
    }
    public partial class TUpdateStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TUpdateStickerSetsOrder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class TUpdatesTooLong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUpdateStoriesStealthMode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += StealthMode.GetLength(); // StealthMode

            return length;
        }
    }
    public partial class TUpdateStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Story.GetLength(); // Story

            return length;
        }
    }
    public partial class TUpdateStoryID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Id
            length += 8; // RandomId

            return length;
        }
    }
    public partial class TUpdateTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Theme.GetLength(); // Theme

            return length;
        }
    }
    public partial class TUpdateTranscribedAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // TranscriptionId
            length += Text.GetTLLength(); // Text

            return length;
        }
    }
    public partial class TUpdateUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TUpdateUserEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += EmojiStatus.GetLength(); // EmojiStatus

            return length;
        }
    }
    public partial class TUpdateUserName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Usernames.GetLength(); // Usernames

            return length;
        }
    }
    public partial class TUpdateUserPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += Phone.GetTLLength(); // Phone

            return length;
        }
    }
    public partial class TUpdateUserStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId
            length += Status.GetLength(); // Status

            return length;
        }
    }
    public partial class TUpdateUserTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // UserId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += Action.GetLength(); // Action

            return length;
        }
    }
    public partial class TUpdateWebBrowserException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // OpenExternalBrowser
            length += Exception.GetLength(); // Exception

            return length;
        }
    }
    public partial class TUpdateWebBrowserSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TUpdateWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Webpage.GetLength(); // Webpage
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TUpdateWebViewResultSent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId

            return length;
        }
    }
    public partial class TUrlAuthResultAccepted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Url.GetTLLength(); } // Url

            return length;
        }
    }
    public partial class TUrlAuthResultDefault
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUrlAuthResultRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            length += Domain.GetTLLength(); // Domain
            if (flags.IsBitSet(2)) { length += Browser.GetTLLength(); } // Browser
            if (flags.IsBitSet(2)) { length += Platform.GetTLLength(); } // Platform
            if (flags.IsBitSet(2)) { length += Ip.GetTLLength(); } // Ip
            if (flags.IsBitSet(2)) { length += Region.GetTLLength(); } // Region
            if (flags.IsBitSet(3)) { length += MatchCodes.GetLength(); } // MatchCodes
            if (flags.IsBitSet(4)) { length += 8; } // UserIdHint
            if (flags.IsBitSet(7)) { length += VerifiedAppName.GetTLLength(); } // VerifiedAppName

            return length;
        }
    }
    public partial class TUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 8; // Id
            if (flags.IsBitSet(0)) { length += 8; } // AccessHash
            if (flags.IsBitSet(1)) { length += FirstName.GetTLLength(); } // FirstName
            if (flags.IsBitSet(2)) { length += LastName.GetTLLength(); } // LastName
            if (flags.IsBitSet(3)) { length += Username.GetTLLength(); } // Username
            if (flags.IsBitSet(4)) { length += Phone.GetTLLength(); } // Phone
            if (flags.IsBitSet(5)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(6)) { length += Status.GetLength(); } // Status
            if (flags.IsBitSet(14)) { length += 4; } // BotInfoVersion
            if (flags.IsBitSet(18)) { length += RestrictionReason.GetLength(); } // RestrictionReason
            if (flags.IsBitSet(19)) { length += BotInlinePlaceholder.GetTLLength(); } // BotInlinePlaceholder
            if (flags.IsBitSet(22)) { length += LangCode.GetTLLength(); } // LangCode
            if (flags.IsBitSet(30)) { length += EmojiStatus.GetLength(); } // EmojiStatus
            if (flags2.IsBitSet(0)) { length += Usernames.GetLength(); } // Usernames
            if (flags2.IsBitSet(5)) { length += StoriesMaxId.GetLength(); } // StoriesMaxId
            if (flags2.IsBitSet(8)) { length += Color.GetLength(); } // Color
            if (flags2.IsBitSet(9)) { length += ProfileColor.GetLength(); } // ProfileColor
            if (flags2.IsBitSet(12)) { length += 4; } // BotActiveUsers
            if (flags2.IsBitSet(14)) { length += 8; } // BotVerificationIcon
            if (flags2.IsBitSet(15)) { length += 8; } // SendPaidMessagesStars
            if (flags2.IsBitSet(21)) { length += 8; } // LinkedCommunityId

            return length;
        }
    }
    public partial class TUserEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TUserFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            var flags2  = CalculateFlags2();
            length += 4; // Flags
            length += 4; // Flags2
            length += 8; // Id
            if (flags.IsBitSet(1)) { length += About.GetTLLength(); } // About
            length += Settings.GetLength(); // Settings
            if (flags.IsBitSet(21)) { length += PersonalPhoto.GetLength(); } // PersonalPhoto
            if (flags.IsBitSet(2)) { length += ProfilePhoto.GetLength(); } // ProfilePhoto
            if (flags.IsBitSet(22)) { length += FallbackPhoto.GetLength(); } // FallbackPhoto
            length += NotifySettings.GetLength(); // NotifySettings
            if (flags.IsBitSet(3)) { length += BotInfo.GetLength(); } // BotInfo
            if (flags.IsBitSet(6)) { length += 4; } // PinnedMsgId
            length += 4; // CommonChatsCount
            if (flags.IsBitSet(11)) { length += 4; } // FolderId
            if (flags.IsBitSet(14)) { length += 4; } // TtlPeriod
            if (flags.IsBitSet(15)) { length += Theme.GetLength(); } // Theme
            if (flags.IsBitSet(16)) { length += PrivateForwardName.GetTLLength(); } // PrivateForwardName
            if (flags.IsBitSet(17)) { length += BotGroupAdminRights.GetLength(); } // BotGroupAdminRights
            if (flags.IsBitSet(18)) { length += BotBroadcastAdminRights.GetLength(); } // BotBroadcastAdminRights
            if (flags.IsBitSet(24)) { length += Wallpaper.GetLength(); } // Wallpaper
            if (flags.IsBitSet(25)) { length += Stories.GetLength(); } // Stories
            if (flags2.IsBitSet(0)) { length += BusinessWorkHours.GetLength(); } // BusinessWorkHours
            if (flags2.IsBitSet(1)) { length += BusinessLocation.GetLength(); } // BusinessLocation
            if (flags2.IsBitSet(2)) { length += BusinessGreetingMessage.GetLength(); } // BusinessGreetingMessage
            if (flags2.IsBitSet(3)) { length += BusinessAwayMessage.GetLength(); } // BusinessAwayMessage
            if (flags2.IsBitSet(4)) { length += BusinessIntro.GetLength(); } // BusinessIntro
            if (flags2.IsBitSet(5)) { length += Birthday.GetLength(); } // Birthday
            if (flags2.IsBitSet(6)) { length += 8; } // PersonalChannelId
            if (flags2.IsBitSet(6)) { length += 4; } // PersonalChannelMessage
            if (flags2.IsBitSet(8)) { length += 4; } // StargiftsCount
            if (flags2.IsBitSet(11)) { length += StarrefProgram.GetLength(); } // StarrefProgram
            if (flags2.IsBitSet(12)) { length += BotVerification.GetLength(); } // BotVerification
            if (flags2.IsBitSet(14)) { length += 8; } // SendPaidMessagesStars
            if (flags2.IsBitSet(15)) { length += DisallowedGifts.GetLength(); } // DisallowedGifts
            if (flags2.IsBitSet(17)) { length += StarsRating.GetLength(); } // StarsRating
            if (flags2.IsBitSet(18)) { length += StarsMyPendingRating.GetLength(); } // StarsMyPendingRating
            if (flags2.IsBitSet(18)) { length += 4; } // StarsMyPendingRatingDate
            if (flags2.IsBitSet(20)) { length += MainTab.GetLength(); } // MainTab
            if (flags2.IsBitSet(21)) { length += SavedMusic.GetLength(); } // SavedMusic
            if (flags2.IsBitSet(22)) { length += Note.GetLength(); } // Note
            if (flags2.IsBitSet(25)) { length += 8; } // BotManagerId

            return length;
        }
    }
    public partial class TUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class TUserProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // PhotoId
            if (flags.IsBitSet(1)) { length += StrippedThumb.GetTLLength(); } // StrippedThumb
            length += 4; // DcId

            return length;
        }
    }
    public partial class TUserProfilePhotoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUserStatusEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUserStatusLastMonth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TUserStatusLastWeek
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TUserStatusOffline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // WasOnline

            return length;
        }
    }
    public partial class TUserStatusOnline
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires

            return length;
        }
    }
    public partial class TUserStatusRecently
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TVideoSize
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Type.GetTLLength(); // Type
            length += 4; // W
            length += 4; // H
            length += 4; // Size
            if (flags.IsBitSet(0)) { length += 8; } // VideoStartTs

            return length;
        }
    }
    public partial class TVideoSizeEmojiMarkup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // EmojiId
            length += BackgroundColors.GetLength(); // BackgroundColors

            return length;
        }
    }
    public partial class TVideoSizeStickerMarkup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += 8; // StickerId
            length += BackgroundColors.GetLength(); // BackgroundColors

            return length;
        }
    }
    public partial class TWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 8; // Id
            length += 4; // Flags
            length += 8; // AccessHash
            length += Slug.GetTLLength(); // Slug
            length += Document.GetLength(); // Document
            if (flags.IsBitSet(2)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class TWallPaperNoFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 8; // Id
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class TWallPaperSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // BackgroundColor
            if (flags.IsBitSet(4)) { length += 4; } // SecondBackgroundColor
            if (flags.IsBitSet(5)) { length += 4; } // ThirdBackgroundColor
            if (flags.IsBitSet(6)) { length += 4; } // FourthBackgroundColor
            if (flags.IsBitSet(3)) { length += 4; } // Intensity
            if (flags.IsBitSet(4)) { length += 4; } // Rotation
            if (flags.IsBitSet(7)) { length += Emoticon.GetTLLength(); } // Emoticon

            return length;
        }
    }
    public partial class TWebAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += 8; // BotId
            length += Domain.GetTLLength(); // Domain
            length += Browser.GetTLLength(); // Browser
            length += Platform.GetTLLength(); // Platform
            length += 4; // DateCreated
            length += 4; // DateActive
            length += Ip.GetTLLength(); // Ip
            length += Region.GetTLLength(); // Region

            return length;
        }
    }
    public partial class TWebDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 8; // AccessHash
            length += 4; // Size
            length += MimeType.GetTLLength(); // MimeType
            length += Attributes.GetLength(); // Attributes

            return length;
        }
    }
    public partial class TWebDocumentNoProxy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 4; // Size
            length += MimeType.GetTLLength(); // MimeType
            length += Attributes.GetLength(); // Attributes

            return length;
        }
    }
    public partial class TWebDomainException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Domain.GetTLLength(); // Domain
            length += Url.GetTLLength(); // Url
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += 8; } // Favicon

            return length;
        }
    }
    public partial class TWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            length += Url.GetTLLength(); // Url
            length += DisplayUrl.GetTLLength(); // DisplayUrl
            length += 4; // Hash
            if (flags.IsBitSet(0)) { length += Type.GetTLLength(); } // Type
            if (flags.IsBitSet(1)) { length += SiteName.GetTLLength(); } // SiteName
            if (flags.IsBitSet(2)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(3)) { length += Description.GetTLLength(); } // Description
            if (flags.IsBitSet(4)) { length += Photo.GetLength(); } // Photo
            if (flags.IsBitSet(5)) { length += EmbedUrl.GetTLLength(); } // EmbedUrl
            if (flags.IsBitSet(5)) { length += EmbedType.GetTLLength(); } // EmbedType
            if (flags.IsBitSet(6)) { length += 4; } // EmbedWidth
            if (flags.IsBitSet(6)) { length += 4; } // EmbedHeight
            if (flags.IsBitSet(7)) { length += 4; } // Duration
            if (flags.IsBitSet(8)) { length += Author.GetTLLength(); } // Author
            if (flags.IsBitSet(9)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(10)) { length += CachedPage.GetLength(); } // CachedPage
            if (flags.IsBitSet(12)) { length += Attributes.GetLength(); } // Attributes

            return length;
        }
    }
    public partial class TWebPageAttributeAiComposeTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // EmojiId

            return length;
        }
    }
    public partial class TWebPageAttributeStarGiftAuction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift
            length += 4; // EndDate

            return length;
        }
    }
    public partial class TWebPageAttributeStarGiftCollection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Icons.GetLength(); // Icons

            return length;
        }
    }
    public partial class TWebPageAttributeStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stickers.GetLength(); // Stickers

            return length;
        }
    }
    public partial class TWebPageAttributeStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Story.GetLength(); } // Story

            return length;
        }
    }
    public partial class TWebPageAttributeTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Documents.GetLength(); } // Documents
            if (flags.IsBitSet(1)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class TWebPageAttributeUniqueStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift

            return length;
        }
    }
    public partial class TWebPageEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            if (flags.IsBitSet(0)) { length += Url.GetTLLength(); } // Url

            return length;
        }
    }
    public partial class TWebPageNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // CachedPageViews

            return length;
        }
    }
    public partial class TWebPagePending
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Id
            if (flags.IsBitSet(0)) { length += Url.GetTLLength(); } // Url
            length += 4; // Date

            return length;
        }
    }
    public partial class TWebViewMessageSent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += MsgId.GetLength(); } // MsgId

            return length;
        }
    }
    public partial class TWebViewResultUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // QueryId
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
}
namespace MyTelegram.Schema.Account
{
    public partial class TAuthorizationForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += RequiredTypes.GetLength(); // RequiredTypes
            length += Values.GetLength(); // Values
            length += Errors.GetLength(); // Errors
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += PrivacyPolicyUrl.GetTLLength(); } // PrivacyPolicyUrl

            return length;
        }
    }
    public partial class TAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // AuthorizationTtlDays
            length += Authorizations.GetLength(); // Authorizations

            return length;
        }
    }
    public partial class TAutoDownloadSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Low.GetLength(); // Low
            length += Medium.GetLength(); // Medium
            length += High.GetLength(); // High

            return length;
        }
    }
    public partial class TAutoSaveSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UsersSettings.GetLength(); // UsersSettings
            length += ChatsSettings.GetLength(); // ChatsSettings
            length += BroadcastsSettings.GetLength(); // BroadcastsSettings
            length += Exceptions.GetLength(); // Exceptions
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TBusinessChatLinks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Links.GetLength(); // Links
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatThemes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash
            length += Themes.GetLength(); // Themes
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
    public partial class TChatThemesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TConnectedBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ConnectedBots.GetLength(); // ConnectedBots
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TContentSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TEmailVerified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Email.GetTLLength(); // Email

            return length;
        }
    }
    public partial class TEmailVerifiedLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Email.GetTLLength(); // Email
            length += SentCode.GetLength(); // SentCode

            return length;
        }
    }
    public partial class TEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Statuses.GetLength(); // Statuses

            return length;
        }
    }
    public partial class TEmojiStatusesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPaidMessagesRevenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // StarsAmount

            return length;
        }
    }
    public partial class TPasskeyRegistrationOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Options.GetLength(); // Options

            return length;
        }
    }
    public partial class TPasskeys
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Passkeys.GetLength(); // Passkeys

            return length;
        }
    }
    public partial class TPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += CurrentAlgo.GetLength(); } // CurrentAlgo
            if (flags.IsBitSet(2)) { length += SrpB.GetTLLength(); } // SrpB
            if (flags.IsBitSet(2)) { length += 8; } // SrpId
            if (flags.IsBitSet(3)) { length += Hint.GetTLLength(); } // Hint
            if (flags.IsBitSet(4)) { length += EmailUnconfirmedPattern.GetTLLength(); } // EmailUnconfirmedPattern
            length += NewAlgo.GetLength(); // NewAlgo
            length += NewSecureAlgo.GetLength(); // NewSecureAlgo
            length += SecureRandom.GetTLLength(); // SecureRandom
            if (flags.IsBitSet(5)) { length += 4; } // PendingResetDate
            if (flags.IsBitSet(6)) { length += LoginEmailPattern.GetTLLength(); } // LoginEmailPattern

            return length;
        }
    }
    public partial class TPasswordInputSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += NewAlgo.GetLength(); } // NewAlgo
            if (flags.IsBitSet(0)) { length += NewPasswordHash.GetTLLength(); } // NewPasswordHash
            if (flags.IsBitSet(0)) { length += Hint.GetTLLength(); } // Hint
            if (flags.IsBitSet(1)) { length += Email.GetTLLength(); } // Email
            if (flags.IsBitSet(2)) { length += NewSecureSettings.GetLength(); } // NewSecureSettings

            return length;
        }
    }
    public partial class TPasswordSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Email.GetTLLength(); } // Email
            if (flags.IsBitSet(1)) { length += SecureSettings.GetLength(); } // SecureSettings

            return length;
        }
    }
    public partial class TPrivacyRules
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Rules.GetLength(); // Rules
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TResetPasswordFailedWait
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // RetryDate

            return length;
        }
    }
    public partial class TResetPasswordOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TResetPasswordRequestedWait
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // UntilDate

            return length;
        }
    }
    public partial class TResolvedBusinessChatLinks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(0)) { length += Entities.GetLength(); } // Entities
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSavedMusicIds
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Ids.GetLength(); // Ids

            return length;
        }
    }
    public partial class TSavedMusicIdsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSavedRingtone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSavedRingtoneConverted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Document.GetLength(); // Document

            return length;
        }
    }
    public partial class TSavedRingtones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Ringtones.GetLength(); // Ringtones

            return length;
        }
    }
    public partial class TSavedRingtonesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSentEmailCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += EmailPattern.GetTLLength(); // EmailPattern
            length += 4; // Length

            return length;
        }
    }
    public partial class TTakeout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class TThemes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Themes.GetLength(); // Themes

            return length;
        }
    }
    public partial class TThemesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTmpPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += TmpPassword.GetTLLength(); // TmpPassword
            length += 4; // ValidUntil

            return length;
        }
    }
    public partial class TWallPapers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Wallpapers.GetLength(); // Wallpapers

            return length;
        }
    }
    public partial class TWallPapersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TWebAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Authorizations.GetLength(); // Authorizations
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TWebBrowserSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += ExternalExceptions.GetLength(); // ExternalExceptions
            length += InappExceptions.GetLength(); // InappExceptions
            length += 8; // Hash

            return length;
        }
    }
    public partial class TWebBrowserSettingsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.AiCompose
{
    public partial class TTones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Tones.GetLength(); // Tones
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TTonesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.Auth
{
    public partial class TAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // OtherwiseReloginDays
            if (flags.IsBitSet(0)) { length += 4; } // TmpSessions
            if (flags.IsBitSet(2)) { length += FutureAuthToken.GetTLLength(); } // FutureAuthToken
            length += User.GetLength(); // User

            return length;
        }
    }
    public partial class TAuthorizationSignUpRequired
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += TermsOfService.GetLength(); } // TermsOfService

            return length;
        }
    }
    public partial class TCodeTypeCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCodeTypeFlashCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCodeTypeFragmentSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCodeTypeMissedCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCodeTypeSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TExportedAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TFirebasePnvIntent
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Nonce.GetTLLength(); // Nonce
            length += DigitalCredentialPayload.GetTLLength(); // DigitalCredentialPayload

            return length;
        }
    }
    public partial class TLoggedOut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += FutureAuthToken.GetTLLength(); } // FutureAuthToken

            return length;
        }
    }
    public partial class TLoginToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TLoginTokenMigrateTo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TLoginTokenSuccess
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Authorization.GetLength(); // Authorization

            return length;
        }
    }
    public partial class TPasskeyLoginOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Options.GetLength(); // Options

            return length;
        }
    }
    public partial class TPasswordRecovery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += EmailPattern.GetTLLength(); // EmailPattern

            return length;
        }
    }
    public partial class TSentCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Type.GetLength(); // Type
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            if (flags.IsBitSet(1)) { length += NextType.GetLength(); } // NextType
            if (flags.IsBitSet(2)) { length += 4; } // Timeout

            return length;
        }
    }
    public partial class TSentCodePaymentRequired
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += StoreProduct.GetTLLength(); // StoreProduct
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += SupportEmailAddress.GetTLLength(); // SupportEmailAddress
            length += SupportEmailSubject.GetTLLength(); // SupportEmailSubject
            length += 4; // PremiumDays
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount

            return length;
        }
    }
    public partial class TSentCodeSuccess
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Authorization.GetLength(); // Authorization

            return length;
        }
    }
    public partial class TSentCodeTypeApp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeEmailCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += EmailPattern.GetTLLength(); // EmailPattern
            length += 4; // Length
            if (flags.IsBitSet(3)) { length += 4; } // ResetAvailablePeriod
            if (flags.IsBitSet(4)) { length += 4; } // ResetPendingDate

            return length;
        }
    }
    public partial class TSentCodeTypeFirebaseSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Nonce.GetTLLength(); } // Nonce
            if (flags.IsBitSet(2)) { length += 8; } // PlayIntegrityProjectId
            if (flags.IsBitSet(2)) { length += PlayIntegrityNonce.GetTLLength(); } // PlayIntegrityNonce
            if (flags.IsBitSet(1)) { length += Receipt.GetTLLength(); } // Receipt
            if (flags.IsBitSet(1)) { length += 4; } // PushTimeout
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeFlashCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Pattern.GetTLLength(); // Pattern

            return length;
        }
    }
    public partial class TSentCodeTypeFragmentSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeMissedCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Prefix.GetTLLength(); // Prefix
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeSetUpEmailRequired
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TSentCodeTypeSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Length

            return length;
        }
    }
    public partial class TSentCodeTypeSmsPhrase
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Beginning.GetTLLength(); } // Beginning

            return length;
        }
    }
    public partial class TSentCodeTypeSmsWord
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Beginning.GetTLLength(); } // Beginning

            return length;
        }
    }
}

namespace MyTelegram.Schema.Auth.LayerN
{
}
namespace MyTelegram.Schema.Bots
{
    public partial class TAccessSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += AddUsers.GetLength(); } // AddUsers

            return length;
        }
    }
    public partial class TBotInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name
            length += About.GetTLLength(); // About
            length += Description.GetTLLength(); // Description

            return length;
        }
    }
    public partial class TExportedBotToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class TPopularAppBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPreviewInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Media.GetLength(); // Media
            length += LangCodes.GetLength(); // LangCodes

            return length;
        }
    }
    public partial class TRequestedButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += WebappReqId.GetTLLength(); // WebappReqId

            return length;
        }
    }
}
namespace MyTelegram.Schema.Channels
{
    public partial class TAdminLogResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Events.GetLength(); // Events
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Participants.GetLength(); // Participants
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelParticipantsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSendAsPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peers.GetLength(); // Peers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSponsoredMessageReportResultAdsHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSponsoredMessageReportResultChooseOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += Options.GetLength(); // Options

            return length;
        }
    }
    public partial class TSponsoredMessageReportResultReported
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.Channels.LayerN
{
    public partial class TChannelParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Participant.GetLength(); // Participant
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Participants.GetLength(); // Participants
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Chatlists
{
    public partial class TChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetLength(); // Title
            if (flags.IsBitSet(0)) { length += Emoticon.GetTLLength(); } // Emoticon
            length += Peers.GetLength(); // Peers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatlistInviteAlready
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // FilterId
            length += MissingPeers.GetLength(); // MissingPeers
            length += AlreadyPeers.GetLength(); // AlreadyPeers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatlistUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MissingPeers.GetLength(); // MissingPeers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TExportedChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Filter.GetLength(); // Filter
            length += Invite.GetLength(); // Invite

            return length;
        }
    }
    public partial class TExportedInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invites.GetLength(); // Invites
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Communities
{
    public partial class TParticipantJoinedChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += CreatorChatIds.GetLength(); // CreatorChatIds
            length += JoinedChatIds.GetLength(); // JoinedChatIds
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPeerLinkRequests
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // TotalCount
            length += Requests.GetLength(); // Requests
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Contacts
{
    public partial class TBlocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Blocked.GetLength(); // Blocked
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TBlockedSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Blocked.GetLength(); // Blocked
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TContactBirthdays
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Contacts.GetLength(); // Contacts
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Contacts.GetLength(); // Contacts
            length += 4; // SavedCount
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TContactsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFound
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MyResults.GetLength(); // MyResults
            length += Results.GetLength(); // Results
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TImportedContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Imported.GetLength(); // Imported
            length += PopularInvites.GetLength(); // PopularInvites
            length += RetryContacts.GetLength(); // RetryContacts
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TResolvedPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSponsoredPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peers.GetLength(); // Peers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSponsoredPeersEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Categories.GetLength(); // Categories
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TTopPeersDisabled
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTopPeersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.E2e
{
    public partial class TCallPacket
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCallPacketLargeMsgId
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBlock
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 64; // Signature
            length += 4; // Flags
            length += 32; // PrevBlockHash
            foreach (var item in Changes) { length += item.GetLength(); }; // Changes
            length += 4; // Height
            length += StateProof.GetLength(); // StateProof
            if (flags.IsBitSet(0)) { length += 32; } // SignaturePublicKey

            return length;
        }
    }
    public partial class TChangeNoop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // Nonce

            return length;
        }
    }
    public partial class TChangeSetGroupState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GroupState.GetLength(); // GroupState

            return length;
        }
    }
    public partial class TChangeSetSharedKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += SharedKey.GetLength(); // SharedKey

            return length;
        }
    }
    public partial class TChangeSetValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetTLLength(); // Key
            length += Value.GetTLLength(); // Value

            return length;
        }
    }
    public partial class TGroupBroadcastNonceCommit
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 64; // Signature
            length += 8; // UserId
            length += 4; // ChainHeight
            length += 32; // ChainHash
            length += 32; // NonceHash

            return length;
        }
    }
    public partial class TGroupBroadcastNonceReveal
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 64; // Signature
            length += 8; // UserId
            length += 4; // ChainHeight
            length += 32; // ChainHash
            length += 32; // Nonce

            return length;
        }
    }
    public partial class TGroupParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 8; // UserId
            length += 32; // PublicKey
            length += 4; // Flags
            length += 4; // Version

            return length;
        }
    }
    public partial class TGroupState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            foreach (var item in Participants) { length += item.GetLength(); }; // Participants
            length += 4; // ExternalPermissions

            return length;
        }
    }
    public partial class TSharedKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // Ek
            length += EncryptedSharedKey.GetTLLength(); // EncryptedSharedKey
            foreach (var item in DestUserId) { length += 8; }; // DestUserId
            foreach (var item in DestHeader) { length += item.GetTLLength(); }; // DestHeader

            return length;
        }
    }
    public partial class TStateProof
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 32; // KvHash
            if (flags.IsBitSet(0)) { length += GroupState.GetLength(); } // GroupState
            if (flags.IsBitSet(1)) { length += SharedKey.GetLength(); } // SharedKey

            return length;
        }
    }
    public partial class THandshakeLoginExport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Accept.GetTLLength(); // Accept
            length += EncryptedKey.GetTLLength(); // EncryptedKey

            return length;
        }
    }
    public partial class THandshakePrivateAccept
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // AlicePK
            length += 32; // BobPK
            length += 8; // AliceUserId
            length += 8; // BobUserId
            length += 32; // AliceNonce
            length += 32; // BobNonce

            return length;
        }
    }
    public partial class THandshakePrivateFinish
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // AlicePK
            length += 32; // BobPK
            length += 8; // AliceUserId
            length += 8; // BobUserId
            length += 32; // AliceNonce
            length += 32; // BobNonce

            return length;
        }
    }
    public partial class THandshakeQR
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // BobEphemeralPK
            length += 32; // BobNonce

            return length;
        }
    }
    public partial class TKeyContactByPublicKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // PublicKey

            return length;
        }
    }
    public partial class TKeyContactByUserId
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TNop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPersonalContactState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TPersonalData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 32; // PublicKey
            foreach (var item in Data) { length += item.GetLength(); }; // Data

            return length;
        }
    }
    public partial class TPersonalEmojiNonces
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 32; } // SelfNonce
            if (flags.IsBitSet(1)) { length += 32; } // ContactNonceHash
            if (flags.IsBitSet(2)) { length += 32; } // ContactNonce

            return length;
        }
    }
    public partial class TPersonalName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName

            return length;
        }
    }
    public partial class TPersonalOnClient
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // SignedAt
            length += Personal.GetLength(); // Personal

            return length;
        }
    }
    public partial class TPersonalOnServer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 64; // Signature
            length += 4; // SignedAt
            length += Personal.GetLength(); // Personal

            return length;
        }
    }
    public partial class TPersonalPhoneNumber
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber

            return length;
        }
    }
    public partial class TPersonalUserId
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // UserId

            return length;
        }
    }
    public partial class TValueContactByPublicKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            foreach (var item in Entries) { length += item.GetLength(); }; // Entries

            return length;
        }
    }
    public partial class TValueContactByUserId
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            foreach (var item in PublicKeys) { length += 32; }; // PublicKeys

            return length;
        }
    }
}
namespace MyTelegram.Schema.Ephemeral
{
    public partial class TWelcomeMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class TWelcomeMessagesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.Fragment
{
    public partial class TCollectibleInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // PurchaseDate
            length += Currency.GetTLLength(); // Currency
            length += 8; // Amount
            length += CryptoCurrency.GetTLLength(); // CryptoCurrency
            length += 8; // CryptoAmount
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
}
namespace MyTelegram.Schema.Help
{
    public partial class TAppConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Config.GetLength(); // Config

            return length;
        }
    }
    public partial class TAppConfigNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAppUpdate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            length += Version.GetTLLength(); // Version
            length += Text.GetTLLength(); // Text
            length += Entities.GetLength(); // Entities
            if (flags.IsBitSet(1)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(2)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(3)) { length += Sticker.GetLength(); } // Sticker

            return length;
        }
    }
    public partial class TCountriesList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Countries.GetLength(); // Countries
            length += 4; // Hash

            return length;
        }
    }
    public partial class TCountriesListNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCountry
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Iso2.GetTLLength(); // Iso2
            length += DefaultName.GetTLLength(); // DefaultName
            if (flags.IsBitSet(1)) { length += Name.GetTLLength(); } // Name
            length += CountryCodes.GetLength(); // CountryCodes

            return length;
        }
    }
    public partial class TCountryCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += CountryCode.GetTLLength(); // CountryCode
            if (flags.IsBitSet(0)) { length += Prefixes.GetLength(); } // Prefixes
            if (flags.IsBitSet(1)) { length += Patterns.GetLength(); } // Patterns

            return length;
        }
    }
    public partial class TDeepLinkInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities

            return length;
        }
    }
    public partial class TDeepLinkInfoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TInviteText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class TNoAppUpdate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPassportConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += CountriesLangs.GetLength(); // CountriesLangs

            return length;
        }
    }
    public partial class TPassportConfigNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPeerColorOption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ColorId
            if (flags.IsBitSet(1)) { length += Colors.GetLength(); } // Colors
            if (flags.IsBitSet(2)) { length += DarkColors.GetLength(); } // DarkColors
            if (flags.IsBitSet(3)) { length += 4; } // ChannelMinLevel
            if (flags.IsBitSet(4)) { length += 4; } // GroupMinLevel

            return length;
        }
    }
    public partial class TPeerColorProfileSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PaletteColors.GetLength(); // PaletteColors
            length += BgColors.GetLength(); // BgColors
            length += StoryColors.GetLength(); // StoryColors

            return length;
        }
    }
    public partial class TPeerColors
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Colors.GetLength(); // Colors

            return length;
        }
    }
    public partial class TPeerColorSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Colors.GetLength(); // Colors

            return length;
        }
    }
    public partial class TPeerColorsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TPremiumPromo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += StatusText.GetTLLength(); // StatusText
            length += StatusEntities.GetLength(); // StatusEntities
            length += VideoSections.GetLength(); // VideoSections
            length += Videos.GetLength(); // Videos
            length += PeriodOptions.GetLength(); // PeriodOptions
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPromoData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Expires
            if (flags.IsBitSet(3)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(1)) { length += PsaType.GetTLLength(); } // PsaType
            if (flags.IsBitSet(2)) { length += PsaMessage.GetTLLength(); } // PsaMessage
            length += PendingSuggestions.GetLength(); // PendingSuggestions
            length += DismissedSuggestions.GetLength(); // DismissedSuggestions
            if (flags.IsBitSet(4)) { length += CustomPendingSuggestion.GetLength(); } // CustomPendingSuggestion
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPromoDataEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires

            return length;
        }
    }
    public partial class TRecentMeUrls
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Urls.GetLength(); // Urls
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSupport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += User.GetLength(); // User

            return length;
        }
    }
    public partial class TSupportName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Name.GetTLLength(); // Name

            return length;
        }
    }
    public partial class TTermsOfService
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            length += Text.GetTLLength(); // Text
            length += Entities.GetLength(); // Entities
            if (flags.IsBitSet(1)) { length += 4; } // MinAgeConfirm

            return length;
        }
    }
    public partial class TTermsOfServiceUpdate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires
            length += TermsOfService.GetLength(); // TermsOfService

            return length;
        }
    }
    public partial class TTermsOfServiceUpdateEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Expires

            return length;
        }
    }
    public partial class TTimezonesList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Timezones.GetLength(); // Timezones
            length += 4; // Hash

            return length;
        }
    }
    public partial class TTimezonesListNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TUserInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Message.GetTLLength(); // Message
            length += Entities.GetLength(); // Entities
            length += Author.GetTLLength(); // Author
            length += 4; // Date

            return length;
        }
    }
    public partial class TUserInfoEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}

namespace MyTelegram.Schema.LayerN
{
    public partial class RequestInitConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ApiId
            length += DeviceModel.GetTLLength(); // DeviceModel
            length += SystemVersion.GetTLLength(); // SystemVersion
            length += AppVersion.GetTLLength(); // AppVersion
            length += SystemLangCode.GetTLLength(); // SystemLangCode
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode
            if (flags.IsBitSet(0)) { length += Proxy.GetLength(); } // Proxy
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class TMessageMediaDocument
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(5)) { length += AltDocument.GetLength(); } // AltDocument
            if (flags.IsBitSet(2)) { length += 4; } // TtlSeconds

            return length;
        }
    }
    public partial class TPhoneCallProtocol
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // MinLayer
            length += 4; // MaxLayer

            return length;
        }
    }
}
namespace MyTelegram.Schema.Messages
{
    public partial class TAffectedFoundMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pts
            length += 4; // PtsCount
            length += 4; // Offset
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class TAffectedHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pts
            length += 4; // PtsCount
            length += 4; // Offset

            return length;
        }
    }
    public partial class TAffectedMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pts
            length += 4; // PtsCount

            return length;
        }
    }
    public partial class TAllStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Sets.GetLength(); // Sets

            return length;
        }
    }
    public partial class TAllStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TArchivedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Sets.GetLength(); // Sets

            return length;
        }
    }
    public partial class TAvailableEffects
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Effects.GetLength(); // Effects
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TAvailableEffectsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAvailableReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Reactions.GetLength(); // Reactions

            return length;
        }
    }
    public partial class TAvailableReactionsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TBotApp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += App.GetLength(); // App

            return length;
        }
    }
    public partial class TBotCallbackAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Message.GetTLLength(); } // Message
            if (flags.IsBitSet(2)) { length += Url.GetTLLength(); } // Url
            length += 4; // CacheTime

            return length;
        }
    }
    public partial class TBotPreparedInlineMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id
            length += 4; // ExpireDate

            return length;
        }
    }
    public partial class TBotResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            if (flags.IsBitSet(1)) { length += NextOffset.GetTLLength(); } // NextOffset
            if (flags.IsBitSet(2)) { length += SwitchPm.GetLength(); } // SwitchPm
            if (flags.IsBitSet(3)) { length += SwitchWebview.GetLength(); } // SwitchWebview
            length += Results.GetLength(); // Results
            length += 4; // CacheTime
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Pts
            length += 4; // Count
            if (flags.IsBitSet(2)) { length += 4; } // OffsetIdOffset
            length += Messages.GetLength(); // Messages
            length += Topics.GetLength(); // Topics
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatAdminsWithInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Admins.GetLength(); // Admins
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FullChat.GetLength(); // FullChat
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatInviteImporters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Importers.GetLength(); // Importers
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChatInviteJoinResultOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Updates.GetLength(); // Updates

            return length;
        }
    }
    public partial class TChatInviteJoinResultWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId
            length += 8; // QueryId
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TChatsSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TCheckedHistoryImportPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ConfirmText.GetTLLength(); // ConfirmText

            return length;
        }
    }
    public partial class TComposedMessageWithAI
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += ResultText.GetLength(); // ResultText
            if (flags.IsBitSet(0)) { length += DiffText.GetLength(); } // DiffText

            return length;
        }
    }
    public partial class TComposedRichMessageWithAI
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class TDhConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // G
            length += P.GetTLLength(); // P
            length += 4; // Version
            length += Random.GetTLLength(); // Random

            return length;
        }
    }
    public partial class TDhConfigNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Random.GetTLLength(); // Random

            return length;
        }
    }
    public partial class TDialogFilters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Filters.GetLength(); // Filters

            return length;
        }
    }
    public partial class TDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Dialogs.GetLength(); // Dialogs
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TDialogsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count

            return length;
        }
    }
    public partial class TDialogsSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Dialogs.GetLength(); // Dialogs
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TDiscussionMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Messages.GetLength(); // Messages
            if (flags.IsBitSet(0)) { length += 4; } // MaxId
            if (flags.IsBitSet(1)) { length += 4; } // ReadInboxMaxId
            if (flags.IsBitSet(2)) { length += 4; } // ReadOutboxMaxId
            length += 4; // UnreadCount
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TEmojiGameDiceInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += GameHash.GetTLLength(); // GameHash
            length += 8; // PrevStake
            length += 4; // CurrentStreak
            length += Params.GetLength(); // Params
            if (flags.IsBitSet(0)) { length += 4; } // PlaysLeft

            return length;
        }
    }
    public partial class TEmojiGameOutcome
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Seed.GetTLLength(); // Seed
            length += 8; // StakeTonAmount
            length += 8; // TonAmount

            return length;
        }
    }
    public partial class TEmojiGameUnavailable
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TEmojiGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Groups.GetLength(); // Groups

            return length;
        }
    }
    public partial class TEmojiGroupsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TExportedChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invite.GetLength(); // Invite
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TExportedChatInviteReplaced
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Invite.GetLength(); // Invite
            length += NewInvite.GetLength(); // NewInvite
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TExportedChatInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Invites.GetLength(); // Invites
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TFavedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Packs.GetLength(); // Packs
            length += Stickers.GetLength(); // Stickers

            return length;
        }
    }
    public partial class TFavedStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFeaturedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash
            length += 4; // Count
            length += Sets.GetLength(); // Sets
            length += Unread.GetLength(); // Unread

            return length;
        }
    }
    public partial class TFeaturedStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count

            return length;
        }
    }
    public partial class TForumTopics
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Topics.GetLength(); // Topics
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += 4; // Pts

            return length;
        }
    }
    public partial class TFoundStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // NextOffset
            length += 8; // Hash
            length += Stickers.GetLength(); // Stickers

            return length;
        }
    }
    public partial class TFoundStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Sets.GetLength(); // Sets

            return length;
        }
    }
    public partial class TFoundStickerSetsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFoundStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // NextOffset

            return length;
        }
    }
    public partial class THighScores
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Scores.GetLength(); // Scores
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class THistoryImport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id

            return length;
        }
    }
    public partial class THistoryImportParsed
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Title.GetTLLength(); } // Title

            return length;
        }
    }
    public partial class TInactiveChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Dates.GetLength(); // Dates
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TInvitedUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Updates.GetLength(); // Updates
            length += MissingInvitees.GetLength(); // MissingInvitees

            return length;
        }
    }
    public partial class TMessageEditData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class TMessageReactionsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Reactions.GetLength(); // Reactions
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
    public partial class TMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Messages.GetLength(); // Messages
            length += Topics.GetLength(); // Topics
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessagesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count

            return length;
        }
    }
    public partial class TMessagesSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            if (flags.IsBitSet(0)) { length += 4; } // NextRate
            if (flags.IsBitSet(2)) { length += 4; } // OffsetIdOffset
            if (flags.IsBitSet(3)) { length += SearchFlood.GetLength(); } // SearchFlood
            length += Messages.GetLength(); // Messages
            length += Topics.GetLength(); // Topics
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessageViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Views.GetLength(); // Views
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMyStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Sets.GetLength(); // Sets

            return length;
        }
    }
    public partial class TPeerDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Dialogs.GetLength(); // Dialogs
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += State.GetLength(); // State

            return length;
        }
    }
    public partial class TPeerSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Settings.GetLength(); // Settings
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPreparedInlineMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += Result.GetLength(); // Result
            length += PeerTypes.GetLength(); // PeerTypes
            length += 4; // CacheTime
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TQuickReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += QuickReplies.GetLength(); // QuickReplies
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TQuickRepliesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Reactions.GetLength(); // Reactions

            return length;
        }
    }
    public partial class TReactionsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TRecentStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Packs.GetLength(); // Packs
            length += Stickers.GetLength(); // Stickers
            length += Dates.GetLength(); // Dates

            return length;
        }
    }
    public partial class TRecentStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSavedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Dialogs.GetLength(); // Dialogs
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSavedDialogsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count

            return length;
        }
    }
    public partial class TSavedDialogsSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Dialogs.GetLength(); // Dialogs
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSavedGifs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Gifs.GetLength(); // Gifs

            return length;
        }
    }
    public partial class TSavedGifsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSavedReactionTags
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tags.GetLength(); // Tags
            length += 8; // Hash

            return length;
        }
    }
    public partial class TSavedReactionTagsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TSearchCounter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Filter.GetLength(); // Filter
            length += 4; // Count

            return length;
        }
    }
    public partial class TSearchResultsCalendar
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += 4; // MinDate
            length += 4; // MinMsgId
            if (flags.IsBitSet(1)) { length += 4; } // OffsetIdOffset
            length += Periods.GetLength(); // Periods
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSearchResultsPositions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Positions.GetLength(); // Positions

            return length;
        }
    }
    public partial class TSentEncryptedFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date
            length += File.GetLength(); // File

            return length;
        }
    }
    public partial class TSentEncryptedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date

            return length;
        }
    }
    public partial class TSponsoredMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // PostsBetween
            if (flags.IsBitSet(1)) { length += 4; } // StartDelay
            if (flags.IsBitSet(2)) { length += 4; } // BetweenDelay
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSponsoredMessagesEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Stickers.GetLength(); // Stickers

            return length;
        }
    }
    public partial class TStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Set.GetLength(); // Set
            length += Packs.GetLength(); // Packs
            length += Keywords.GetLength(); // Keywords
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TStickerSetInstallResultArchive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Sets.GetLength(); // Sets

            return length;
        }
    }
    public partial class TStickerSetInstallResultSuccess
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStickerSetNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStickersNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TTranscribedAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // TranscriptionId
            length += Text.GetTLLength(); // Text
            if (flags.IsBitSet(1)) { length += 4; } // TrialRemainsNum
            if (flags.IsBitSet(1)) { length += 4; } // TrialRemainsUntilDate

            return length;
        }
    }
    public partial class TTranslatedRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class TTranslateResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class TVotesList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Votes.GetLength(); // Votes
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
    public partial class TWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Webpage.GetLength(); // Webpage
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TWebPagePreview
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Media.GetLength(); // Media
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
}

namespace MyTelegram.Schema.Payments
{
    public partial class TBankCardData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title
            length += OpenUrls.GetLength(); // OpenUrls

            return length;
        }
    }
    public partial class TCheckCanSendGiftResultFail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Reason.GetLength(); // Reason

            return length;
        }
    }
    public partial class TCheckCanSendGiftResultOk
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TCheckedGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(3)) { length += 4; } // GiveawayMsgId
            if (flags.IsBitSet(0)) { length += 8; } // ToId
            length += 4; // Date
            length += 4; // Days
            if (flags.IsBitSet(1)) { length += 4; } // UsedDate
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TConnectedStarRefBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += ConnectedBots.GetLength(); // ConnectedBots
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TExportedInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TGiveawayInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // StartDate
            if (flags.IsBitSet(1)) { length += 4; } // JoinedTooEarlyDate
            if (flags.IsBitSet(2)) { length += 8; } // AdminDisallowedChatId
            if (flags.IsBitSet(4)) { length += DisallowedCountry.GetTLLength(); } // DisallowedCountry

            return length;
        }
    }
    public partial class TGiveawayInfoResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // StartDate
            if (flags.IsBitSet(3)) { length += GiftCodeSlug.GetTLLength(); } // GiftCodeSlug
            if (flags.IsBitSet(4)) { length += 8; } // StarsPrize
            length += 4; // FinishDate
            length += 4; // WinnersCount
            if (flags.IsBitSet(2)) { length += 4; } // ActivatedCount

            return length;
        }
    }
    public partial class TPaymentForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // FormId
            length += 8; // BotId
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(5)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            length += 8; // ProviderId
            length += Url.GetTLLength(); // Url
            if (flags.IsBitSet(4)) { length += NativeProvider.GetTLLength(); } // NativeProvider
            if (flags.IsBitSet(4)) { length += NativeParams.GetLength(); } // NativeParams
            if (flags.IsBitSet(6)) { length += AdditionalMethods.GetLength(); } // AdditionalMethods
            if (flags.IsBitSet(0)) { length += SavedInfo.GetLength(); } // SavedInfo
            if (flags.IsBitSet(1)) { length += SavedCredentials.GetLength(); } // SavedCredentials
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPaymentFormStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // FormId
            length += Invoice.GetLength(); // Invoice

            return length;
        }
    }
    public partial class TPaymentFormStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // FormId
            length += 8; // BotId
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(5)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPaymentReceipt
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Date
            length += 8; // BotId
            length += 8; // ProviderId
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            if (flags.IsBitSet(0)) { length += Info.GetLength(); } // Info
            if (flags.IsBitSet(1)) { length += Shipping.GetLength(); } // Shipping
            if (flags.IsBitSet(3)) { length += 8; } // TipAmount
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            length += CredentialsTitle.GetTLLength(); // CredentialsTitle
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPaymentReceiptStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Date
            length += 8; // BotId
            length += Title.GetTLLength(); // Title
            length += Description.GetTLLength(); // Description
            if (flags.IsBitSet(2)) { length += Photo.GetLength(); } // Photo
            length += Invoice.GetLength(); // Invoice
            length += Currency.GetTLLength(); // Currency
            length += 8; // TotalAmount
            length += TransactionId.GetTLLength(); // TransactionId
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPaymentResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Updates.GetLength(); // Updates

            return length;
        }
    }
    public partial class TPaymentVerificationNeeded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TResaleStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Gifts.GetLength(); // Gifts
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            if (flags.IsBitSet(1)) { length += Attributes.GetLength(); } // Attributes
            if (flags.IsBitSet(1)) { length += 8; } // AttributesHash
            length += Chats.GetLength(); // Chats
            if (flags.IsBitSet(2)) { length += Counters.GetLength(); } // Counters
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSavedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += SavedInfo.GetLength(); } // SavedInfo

            return length;
        }
    }
    public partial class TSavedStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            if (flags.IsBitSet(1)) { length += 4; } // ChatNotificationsEnabled
            length += Gifts.GetLength(); // Gifts
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStarGiftActiveAuctions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Auctions.GetLength(); // Auctions
            length += Users.GetLength(); // Users
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TStarGiftActiveAuctionsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftAuctionAcquiredGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gifts.GetLength(); // Gifts
            length += Users.GetLength(); // Users
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TStarGiftAuctionState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift
            length += State.GetLength(); // State
            length += UserState.GetLength(); // UserState
            length += 4; // Timeout
            length += Users.GetLength(); // Users
            length += Chats.GetLength(); // Chats

            return length;
        }
    }
    public partial class TStarGiftCollections
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Collections.GetLength(); // Collections

            return length;
        }
    }
    public partial class TStarGiftCollectionsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash
            length += Gifts.GetLength(); // Gifts
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStarGiftsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TStarGiftUpgradeAttributes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Attributes.GetLength(); // Attributes

            return length;
        }
    }
    public partial class TStarGiftUpgradePreview
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += SampleAttributes.GetLength(); // SampleAttributes
            length += Prices.GetLength(); // Prices
            length += NextPrices.GetLength(); // NextPrices

            return length;
        }
    }
    public partial class TStarGiftWithdrawalUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TStarsRevenueAdsAccountUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TStarsRevenueStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += TopHoursGraph.GetLength(); } // TopHoursGraph
            length += RevenueGraph.GetLength(); // RevenueGraph
            length += Status.GetLength(); // Status
            length += 8; // UsdRate

            return length;
        }
    }
    public partial class TStarsRevenueWithdrawalUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class TStarsStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Balance.GetLength(); // Balance
            if (flags.IsBitSet(1)) { length += Subscriptions.GetLength(); } // Subscriptions
            if (flags.IsBitSet(2)) { length += SubscriptionsNextOffset.GetTLLength(); } // SubscriptionsNextOffset
            if (flags.IsBitSet(4)) { length += 8; } // SubscriptionsMissingBalance
            if (flags.IsBitSet(3)) { length += History.GetLength(); } // History
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TSuggestedStarRefBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += SuggestedBots.GetLength(); // SuggestedBots
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
    public partial class TUniqueStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Gift.GetLength(); // Gift
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TUniqueStarGiftValueInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Currency.GetTLLength(); // Currency
            length += 8; // Value
            length += 4; // InitialSaleDate
            length += 8; // InitialSaleStars
            length += 8; // InitialSalePrice
            if (flags.IsBitSet(0)) { length += 4; } // LastSaleDate
            if (flags.IsBitSet(0)) { length += 8; } // LastSalePrice
            if (flags.IsBitSet(2)) { length += 8; } // FloorPrice
            if (flags.IsBitSet(3)) { length += 8; } // AveragePrice
            if (flags.IsBitSet(4)) { length += 4; } // ListedCount
            if (flags.IsBitSet(5)) { length += 4; } // FragmentListedCount
            if (flags.IsBitSet(5)) { length += FragmentListedUrl.GetTLLength(); } // FragmentListedUrl

            return length;
        }
    }
    public partial class TValidatedRequestedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Id.GetTLLength(); } // Id
            if (flags.IsBitSet(1)) { length += ShippingOptions.GetLength(); } // ShippingOptions

            return length;
        }
    }
}
namespace MyTelegram.Schema.Phone
{
    public partial class TExportedGroupCallInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Link.GetTLLength(); // Link

            return length;
        }
    }
    public partial class TGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Participants.GetLength(); // Participants
            length += ParticipantsNextOffset.GetTLLength(); // ParticipantsNextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TGroupCallStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // TotalStars
            length += TopDonors.GetLength(); // TopDonors
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TGroupCallStreamChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channels.GetLength(); // Channels

            return length;
        }
    }
    public partial class TGroupCallStreamRtmpUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += Key.GetTLLength(); // Key

            return length;
        }
    }
    public partial class TGroupParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Participants.GetLength(); // Participants
            length += NextOffset.GetTLLength(); // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += 4; // Version

            return length;
        }
    }
    public partial class TJoinAsPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peers.GetLength(); // Peers
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPhoneCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneCall.GetLength(); // PhoneCall
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Photos
{
    public partial class TPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Photo.GetLength(); // Photo
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPhotos
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Photos.GetLength(); // Photos
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPhotosSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Photos.GetLength(); // Photos
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Premium
{
    public partial class TBoostsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Boosts.GetLength(); // Boosts
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TBoostsStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Level
            length += 4; // CurrentLevelBoosts
            length += 4; // Boosts
            if (flags.IsBitSet(4)) { length += 4; } // GiftBoosts
            if (flags.IsBitSet(0)) { length += 4; } // NextLevelBoosts
            if (flags.IsBitSet(1)) { length += PremiumAudience.GetLength(); } // PremiumAudience
            length += BoostUrl.GetTLLength(); // BoostUrl
            if (flags.IsBitSet(3)) { length += PrepaidGiveaways.GetLength(); } // PrepaidGiveaways
            if (flags.IsBitSet(2)) { length += MyBoostSlots.GetLength(); } // MyBoostSlots

            return length;
        }
    }
    public partial class TMyBoosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MyBoosts.GetLength(); // MyBoosts
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema.Smsjobs
{
    public partial class TEligibleToJoin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += TermsUrl.GetTLLength(); // TermsUrl
            length += 4; // MonthlySentSms

            return length;
        }
    }
    public partial class TStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // RecentSent
            length += 4; // RecentSince
            length += 4; // RecentRemains
            length += 4; // TotalSent
            length += 4; // TotalSince
            if (flags.IsBitSet(1)) { length += LastGiftSlug.GetTLLength(); } // LastGiftSlug
            length += TermsUrl.GetTLLength(); // TermsUrl

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stats
{
    public partial class TBroadcastStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Period.GetLength(); // Period
            length += Followers.GetLength(); // Followers
            length += ViewsPerPost.GetLength(); // ViewsPerPost
            length += SharesPerPost.GetLength(); // SharesPerPost
            length += ReactionsPerPost.GetLength(); // ReactionsPerPost
            length += ViewsPerStory.GetLength(); // ViewsPerStory
            length += SharesPerStory.GetLength(); // SharesPerStory
            length += ReactionsPerStory.GetLength(); // ReactionsPerStory
            length += EnabledNotifications.GetLength(); // EnabledNotifications
            length += GrowthGraph.GetLength(); // GrowthGraph
            length += FollowersGraph.GetLength(); // FollowersGraph
            length += MuteGraph.GetLength(); // MuteGraph
            length += TopHoursGraph.GetLength(); // TopHoursGraph
            length += InteractionsGraph.GetLength(); // InteractionsGraph
            length += IvInteractionsGraph.GetLength(); // IvInteractionsGraph
            length += ViewsBySourceGraph.GetLength(); // ViewsBySourceGraph
            length += NewFollowersBySourceGraph.GetLength(); // NewFollowersBySourceGraph
            length += LanguagesGraph.GetLength(); // LanguagesGraph
            length += ReactionsByEmotionGraph.GetLength(); // ReactionsByEmotionGraph
            length += StoryInteractionsGraph.GetLength(); // StoryInteractionsGraph
            length += StoryReactionsByEmotionGraph.GetLength(); // StoryReactionsByEmotionGraph
            length += RecentPostsInteractions.GetLength(); // RecentPostsInteractions

            return length;
        }
    }
    public partial class TMegagroupStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Period.GetLength(); // Period
            length += Members.GetLength(); // Members
            length += Messages.GetLength(); // Messages
            length += Viewers.GetLength(); // Viewers
            length += Posters.GetLength(); // Posters
            length += GrowthGraph.GetLength(); // GrowthGraph
            length += MembersGraph.GetLength(); // MembersGraph
            length += NewMembersBySourceGraph.GetLength(); // NewMembersBySourceGraph
            length += LanguagesGraph.GetLength(); // LanguagesGraph
            length += MessagesGraph.GetLength(); // MessagesGraph
            length += ActionsGraph.GetLength(); // ActionsGraph
            length += TopHoursGraph.GetLength(); // TopHoursGraph
            length += WeekdaysGraph.GetLength(); // WeekdaysGraph
            length += TopPosters.GetLength(); // TopPosters
            length += TopAdmins.GetLength(); // TopAdmins
            length += TopInviters.GetLength(); // TopInviters
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TMessageStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ViewsGraph.GetLength(); // ViewsGraph
            length += ReactionsByEmotionGraph.GetLength(); // ReactionsByEmotionGraph

            return length;
        }
    }
    public partial class TPollStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += VotesGraph.GetLength(); // VotesGraph

            return length;
        }
    }
    public partial class TPublicForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Forwards.GetLength(); // Forwards
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStoryStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ViewsGraph.GetLength(); // ViewsGraph
            length += ReactionsByEmotionGraph.GetLength(); // ReactionsByEmotionGraph

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stickers
{
    public partial class TSuggestedShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ShortName.GetTLLength(); // ShortName

            return length;
        }
    }
}
namespace MyTelegram.Schema.Storage
{
    public partial class TFileGif
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileJpeg
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileMov
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileMp3
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileMp4
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFilePartial
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFilePdf
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFilePng
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileUnknown
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TFileWebp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stories
{
    public partial class TAlbums
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash
            length += Albums.GetLength(); // Albums

            return length;
        }
    }
    public partial class TAlbumsNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class TAllStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += State.GetTLLength(); // State
            length += PeerStories.GetLength(); // PeerStories
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += StealthMode.GetLength(); // StealthMode

            return length;
        }
    }
    public partial class TAllStoriesNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Flags
            length += State.GetTLLength(); // State
            length += StealthMode.GetLength(); // StealthMode

            return length;
        }
    }
    public partial class TCanSendStoryCount
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // CountRemains

            return length;
        }
    }
    public partial class TFoundStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Stories.GetLength(); // Stories
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TPeerStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stories.GetLength(); // Stories
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Stories.GetLength(); // Stories
            if (flags.IsBitSet(0)) { length += PinnedToTop.GetLength(); } // PinnedToTop
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStoryReactionsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += Reactions.GetLength(); // Reactions
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
    public partial class TStoryViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Views.GetLength(); // Views
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TStoryViewsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Count
            length += 4; // ViewsCount
            length += 4; // ForwardsCount
            length += 4; // ReactionsCount
            length += Views.GetLength(); // Views
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            if (flags.IsBitSet(0)) { length += NextOffset.GetTLLength(); } // NextOffset

            return length;
        }
    }
}
namespace MyTelegram.Schema.Updates
{
    public partial class TChannelDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Pts
            if (flags.IsBitSet(1)) { length += 4; } // Timeout
            length += NewMessages.GetLength(); // NewMessages
            length += OtherUpdates.GetLength(); // OtherUpdates
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TChannelDifferenceEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Pts
            if (flags.IsBitSet(1)) { length += 4; } // Timeout

            return length;
        }
    }
    public partial class TChannelDifferenceTooLong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // Timeout
            length += Dialog.GetLength(); // Dialog
            length += Messages.GetLength(); // Messages
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += NewMessages.GetLength(); // NewMessages
            length += NewEncryptedMessages.GetLength(); // NewEncryptedMessages
            length += OtherUpdates.GetLength(); // OtherUpdates
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += State.GetLength(); // State

            return length;
        }
    }
    public partial class TDifferenceEmpty
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Date
            length += 4; // Seq

            return length;
        }
    }
    public partial class TDifferenceSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += NewMessages.GetLength(); // NewMessages
            length += NewEncryptedMessages.GetLength(); // NewEncryptedMessages
            length += OtherUpdates.GetLength(); // OtherUpdates
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users
            length += IntermediateState.GetLength(); // IntermediateState

            return length;
        }
    }
    public partial class TDifferenceTooLong
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pts

            return length;
        }
    }
    public partial class TState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Pts
            length += 4; // Qts
            length += 4; // Date
            length += 4; // Seq
            length += 4; // UnreadCount

            return length;
        }
    }
}
namespace MyTelegram.Schema.Updates.LayerN
{
}
namespace MyTelegram.Schema.Upload
{
    public partial class TCdnFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TCdnFileReuploadNeeded
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += RequestToken.GetTLLength(); // RequestToken

            return length;
        }
    }
    public partial class TFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Type.GetLength(); // Type
            length += 4; // Mtime
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class TFileCdnRedirect
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId
            length += FileToken.GetTLLength(); // FileToken
            length += EncryptionKey.GetTLLength(); // EncryptionKey
            length += EncryptionIv.GetTLLength(); // EncryptionIv
            length += FileHashes.GetLength(); // FileHashes

            return length;
        }
    }
    public partial class TWebFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Size
            length += MimeType.GetTLLength(); // MimeType
            length += FileType.GetLength(); // FileType
            length += 4; // Mtime
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
}
namespace MyTelegram.Schema.Users
{
    public partial class TSavedMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class TSavedMusicNotModified
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count

            return length;
        }
    }
    public partial class TUserFull
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FullUser.GetLength(); // FullUser
            length += Chats.GetLength(); // Chats
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class TUsersSlice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Count
            length += Users.GetLength(); // Users

            return length;
        }
    }
}
namespace MyTelegram.Schema
{
    public partial class RequestDestroyAuthKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestDestroySession
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // SessionId

            return length;
        }
    }
    public partial class RequestGetFutureSalts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Num

            return length;
        }
    }
    public partial class RequestHttpWait
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MaxDelay
            length += 4; // WaitAfter
            length += 4; // MaxWait

            return length;
        }
    }
    public partial class RequestInitConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ApiId
            length += DeviceModel.GetTLLength(); // DeviceModel
            length += SystemVersion.GetTLLength(); // SystemVersion
            length += AppVersion.GetTLLength(); // AppVersion
            length += SystemLangCode.GetTLLength(); // SystemLangCode
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode
            if (flags.IsBitSet(0)) { length += Proxy.GetLength(); } // Proxy
            if (flags.IsBitSet(1)) { length += Params.GetLength(); } // Params
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeAfterMsg
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // MsgId
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeAfterMsgs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += MsgIds.GetLength(); // MsgIds
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithApnsSecret
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Nonce.GetTLLength(); // Nonce
            length += Secret.GetTLLength(); // Secret
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithBusinessConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ConnectionId.GetTLLength(); // ConnectionId
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithGooglePlayIntegrity
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Nonce.GetTLLength(); // Nonce
            length += Token.GetTLLength(); // Token
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithLayer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Layer
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithMessagesRange
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Range.GetLength(); // Range
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithoutUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithReCaptcha
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestInvokeWithTakeout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // TakeoutId
            length += Query.GetLength(); // Query

            return length;
        }
    }
    public partial class RequestPing
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PingId

            return length;
        }
    }
    public partial class RequestPingDelayDisconnect
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PingId
            length += 4; // DisconnectDelay

            return length;
        }
    }
    public partial class RequestReqDHParams
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += P.GetTLLength(); // P
            length += Q.GetTLLength(); // Q
            length += 8; // PublicKeyFingerprint
            length += EncryptedData.GetTLLength(); // EncryptedData

            return length;
        }
    }
    public partial class RequestReqPq
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce

            return length;
        }
    }
    public partial class RequestReqPqMulti
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce

            return length;
        }
    }
    public partial class RequestRpcDropAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ReqMsgId

            return length;
        }
    }
    public partial class RequestSetClientDHParams
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 16; // Nonce
            length += 16; // ServerNonce
            length += EncryptedData.GetTLLength(); // EncryptedData

            return length;
        }
    }
}
namespace MyTelegram.Schema.Account
{
    public partial class RequestAcceptAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId
            length += Scope.GetTLLength(); // Scope
            length += PublicKey.GetTLLength(); // PublicKey
            length += ValueHashes.GetLength(); // ValueHashes
            length += Credentials.GetLength(); // Credentials

            return length;
        }
    }
    public partial class RequestCancelPasswordEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestChangeAuthorizationSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash
            if (flags.IsBitSet(0)) { length += 4; } // EncryptedRequestsDisabled
            if (flags.IsBitSet(1)) { length += 4; } // CallRequestsDisabled

            return length;
        }
    }
    public partial class RequestChangePhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += PhoneCode.GetTLLength(); // PhoneCode

            return length;
        }
    }
    public partial class RequestCheckUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class RequestClearRecentEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestConfirmBotConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += BotId.GetLength(); // BotId

            return length;
        }
    }
    public partial class RequestConfirmPasswordEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Code.GetTLLength(); // Code

            return length;
        }
    }
    public partial class RequestConfirmPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += PhoneCode.GetTLLength(); // PhoneCode

            return length;
        }
    }
    public partial class RequestCreateBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Link.GetLength(); // Link

            return length;
        }
    }
    public partial class RequestCreateTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Slug.GetTLLength(); // Slug
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(2)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(3)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class RequestDeclinePasswordReset
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestDeleteAccount
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Reason.GetTLLength(); // Reason
            if (flags.IsBitSet(0)) { length += Password.GetLength(); } // Password

            return length;
        }
    }
    public partial class RequestDeleteAutoSaveExceptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestDeleteBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestDeletePasskey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetTLLength(); // Id

            return length;
        }
    }
    public partial class RequestDeleteSecureValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Types.GetLength(); // Types

            return length;
        }
    }
    public partial class RequestDeleteWebBrowserSettingsExceptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestDisablePeerConnectedBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestEditBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug
            length += Link.GetLength(); // Link

            return length;
        }
    }
    public partial class RequestFinishTakeoutSession
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestGetAccountTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAllSecureValues
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAuthorizationForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // BotId
            length += Scope.GetTLLength(); // Scope
            length += PublicKey.GetTLLength(); // PublicKey

            return length;
        }
    }
    public partial class RequestGetAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAutoDownloadSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAutoSaveSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetBotBusinessConnection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ConnectionId.GetTLLength(); // ConnectionId

            return length;
        }
    }
    public partial class RequestGetBusinessChatLinks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetChannelDefaultEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetChannelRestrictedStatusEmojis
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetChatThemes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetCollectibleEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetConnectedBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetContactSignUpNotification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetContentSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetDefaultBackgroundEmojis
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetDefaultEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetDefaultGroupPhotoEmojis
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetDefaultProfilePhotoEmojis
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetGlobalPrivacySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetMultiWallPapers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Wallpapers.GetLength(); // Wallpapers

            return length;
        }
    }
    public partial class RequestGetNotifyExceptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer

            return length;
        }
    }
    public partial class RequestGetNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetPaidMessagesRevenue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestGetPasskeys
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPasswordSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Password.GetLength(); // Password

            return length;
        }
    }
    public partial class RequestGetPrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetLength(); // Key

            return length;
        }
    }
    public partial class RequestGetReactionsNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetRecentEmojiStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedMusicIds
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedRingtones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSecureValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Types.GetLength(); // Types

            return length;
        }
    }
    public partial class RequestGetTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Format.GetTLLength(); // Format
            length += Theme.GetLength(); // Theme

            return length;
        }
    }
    public partial class RequestGetThemes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Format.GetTLLength(); // Format
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetTmpPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Password.GetLength(); // Password
            length += 4; // Period

            return length;
        }
    }
    public partial class RequestGetUniqueGiftChatThemes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Wallpaper.GetLength(); // Wallpaper

            return length;
        }
    }
    public partial class RequestGetWallPapers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetWebAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetWebBrowserSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestInitPasskeyRegistration
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestInitTakeoutSession
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(5)) { length += 8; } // FileMaxSize

            return length;
        }
    }
    public partial class RequestInstallTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Theme.GetLength(); } // Theme
            if (flags.IsBitSet(2)) { length += Format.GetTLLength(); } // Format
            if (flags.IsBitSet(3)) { length += BaseTheme.GetLength(); } // BaseTheme

            return length;
        }
    }
    public partial class RequestInstallWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Wallpaper.GetLength(); // Wallpaper
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestInvalidateSignInCodes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Codes.GetLength(); // Codes

            return length;
        }
    }
    public partial class RequestRegisterDevice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // TokenType
            length += Token.GetTLLength(); // Token
            length += 4; // AppSandbox
            length += Secret.GetTLLength(); // Secret
            length += OtherUids.GetLength(); // OtherUids

            return length;
        }
    }
    public partial class RequestRegisterPasskey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Credential.GetLength(); // Credential

            return length;
        }
    }
    public partial class RequestReorderUsernames
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReportPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Reason.GetLength(); // Reason
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class RequestReportProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += PhotoId.GetLength(); // PhotoId
            length += Reason.GetLength(); // Reason
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class RequestResendPasswordEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestResetNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetWallPapers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetWebAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestResetWebAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResolveBusinessChatLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestSaveAutoDownloadSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSaveAutoSaveSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += Peer.GetLength(); } // Peer
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSaveMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            if (flags.IsBitSet(1)) { length += AfterId.GetLength(); } // AfterId

            return length;
        }
    }
    public partial class RequestSaveRingtone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += 4; // Unsave

            return length;
        }
    }
    public partial class RequestSaveSecureValue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Value.GetLength(); // Value
            length += 8; // SecureSecretId

            return length;
        }
    }
    public partial class RequestSaveTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Theme.GetLength(); // Theme
            length += 4; // Unsave

            return length;
        }
    }
    public partial class RequestSaveWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Wallpaper.GetLength(); // Wallpaper
            length += 4; // Unsave
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSendChangePhoneCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSendConfirmPhoneCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Hash.GetTLLength(); // Hash
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSendVerifyEmailCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose
            length += Email.GetTLLength(); // Email

            return length;
        }
    }
    public partial class RequestSendVerifyPhoneCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSetAccountTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Ttl.GetLength(); // Ttl

            return length;
        }
    }
    public partial class RequestSetAuthorizationTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // AuthorizationTtlDays

            return length;
        }
    }
    public partial class RequestSetContactSignUpNotification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Silent

            return length;
        }
    }
    public partial class RequestSetContentSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestSetGlobalPrivacySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSetMainProfileTab
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tab.GetLength(); // Tab

            return length;
        }
    }
    public partial class RequestSetPrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Key.GetLength(); // Key
            length += Rules.GetLength(); // Rules

            return length;
        }
    }
    public partial class RequestSetReactionsNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestToggleConnectedBotPaused
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Paused

            return length;
        }
    }
    public partial class RequestToggleNoPaidMessagesException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestToggleSponsoredMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Username.GetTLLength(); // Username
            length += 4; // Active

            return length;
        }
    }
    public partial class RequestToggleWebBrowserSettingsException
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // OpenExternalBrowser
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class RequestUnregisterDevice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // TokenType
            length += Token.GetTLLength(); // Token
            length += OtherUids.GetLength(); // OtherUids

            return length;
        }
    }
    public partial class RequestUpdateBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Birthday.GetLength(); } // Birthday

            return length;
        }
    }
    public partial class RequestUpdateBusinessAwayMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class RequestUpdateBusinessGreetingMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Message.GetLength(); } // Message

            return length;
        }
    }
    public partial class RequestUpdateBusinessIntro
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Intro.GetLength(); } // Intro

            return length;
        }
    }
    public partial class RequestUpdateBusinessLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += GeoPoint.GetLength(); } // GeoPoint
            if (flags.IsBitSet(0)) { length += Address.GetTLLength(); } // Address

            return length;
        }
    }
    public partial class RequestUpdateBusinessWorkHours
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += BusinessWorkHours.GetLength(); } // BusinessWorkHours

            return length;
        }
    }
    public partial class RequestUpdateColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Color.GetLength(); } // Color

            return length;
        }
    }
    public partial class RequestUpdateConnectedBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Rights.GetLength(); } // Rights
            length += Bot.GetLength(); // Bot
            length += Recipients.GetLength(); // Recipients

            return length;
        }
    }
    public partial class RequestUpdateDeviceLocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Period

            return length;
        }
    }
    public partial class RequestUpdateEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += EmojiStatus.GetLength(); // EmojiStatus

            return length;
        }
    }
    public partial class RequestUpdateNotifySettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestUpdatePasswordSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Password.GetLength(); // Password
            length += NewSettings.GetLength(); // NewSettings

            return length;
        }
    }
    public partial class RequestUpdatePersonalChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestUpdateProfile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += FirstName.GetTLLength(); } // FirstName
            if (flags.IsBitSet(1)) { length += LastName.GetTLLength(); } // LastName
            if (flags.IsBitSet(2)) { length += About.GetTLLength(); } // About

            return length;
        }
    }
    public partial class RequestUpdateStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offline

            return length;
        }
    }
    public partial class RequestUpdateTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Format.GetTLLength(); // Format
            length += Theme.GetLength(); // Theme
            if (flags.IsBitSet(0)) { length += Slug.GetTLLength(); } // Slug
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(3)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class RequestUpdateUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class RequestUpdateWebBrowserSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestUploadRingtone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += File.GetLength(); // File
            length += FileName.GetTLLength(); // FileName
            length += MimeType.GetTLLength(); // MimeType

            return length;
        }
    }
    public partial class RequestUploadTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += File.GetLength(); // File
            if (flags.IsBitSet(0)) { length += Thumb.GetLength(); } // Thumb
            length += FileName.GetTLLength(); // FileName
            length += MimeType.GetTLLength(); // MimeType

            return length;
        }
    }
    public partial class RequestUploadWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += File.GetLength(); // File
            length += MimeType.GetTLLength(); // MimeType
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestVerifyEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose
            length += Verification.GetLength(); // Verification

            return length;
        }
    }
    public partial class RequestVerifyPhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += PhoneCode.GetTLLength(); // PhoneCode

            return length;
        }
    }
}
namespace MyTelegram.Schema.Account.LayerN
{
    public partial class RequestCreateTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Slug.GetTLLength(); // Slug
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(2)) { length += Document.GetLength(); } // Document
            if (flags.IsBitSet(3)) { length += Settings.GetLength(); } // Settings

            return length;
        }
    }
    public partial class RequestGetTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Format.GetTLLength(); // Format
            length += Theme.GetLength(); // Theme
            length += 8; // DocumentId

            return length;
        }
    }
    public partial class RequestInstallTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Format.GetTLLength(); } // Format
            if (flags.IsBitSet(1)) { length += Theme.GetLength(); } // Theme

            return length;
        }
    }
    public partial class RequestRegisterDevice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // TokenType
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
}
namespace MyTelegram.Schema.AiCompose
{
    public partial class RequestCreateTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // EmojiId
            length += Title.GetTLLength(); // Title
            length += Prompt.GetTLLength(); // Prompt

            return length;
        }
    }
    public partial class RequestDeleteTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetLength(); // Tone

            return length;
        }
    }
    public partial class RequestGetTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetLength(); // Tone

            return length;
        }
    }
    public partial class RequestGetToneExample
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetLength(); // Tone
            length += 4; // Num

            return length;
        }
    }
    public partial class RequestGetTones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSaveTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Tone.GetLength(); // Tone
            length += 4; // Unsave

            return length;
        }
    }
    public partial class RequestUpdateTone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Tone.GetLength(); // Tone
            if (flags.IsBitSet(0)) { length += 4; } // DisplayAuthor
            if (flags.IsBitSet(1)) { length += 8; } // EmojiId
            if (flags.IsBitSet(2)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(3)) { length += Prompt.GetTLLength(); } // Prompt

            return length;
        }
    }
}
namespace MyTelegram.Schema.Auth
{
    public partial class RequestAcceptLoginToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class RequestBindTempAuthKey
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // PermAuthKeyId
            length += 8; // Nonce
            length += 4; // ExpiresAt
            length += EncryptedMessage.GetTLLength(); // EncryptedMessage

            return length;
        }
    }
    public partial class RequestCancelCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash

            return length;
        }
    }
    public partial class RequestCheckPaidAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += 8; // FormId

            return length;
        }
    }
    public partial class RequestCheckPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Password.GetLength(); // Password

            return length;
        }
    }
    public partial class RequestCheckRecoveryPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Code.GetTLLength(); // Code

            return length;
        }
    }
    public partial class RequestDropTempAuthKeys
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ExceptAuthKeys.GetLength(); // ExceptAuthKeys

            return length;
        }
    }
    public partial class RequestExportAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // DcId

            return length;
        }
    }
    public partial class RequestExportLoginToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash
            length += ExceptIds.GetLength(); // ExceptIds

            return length;
        }
    }
    public partial class RequestFinishFirebasePnvLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += GoogleToken.GetTLLength(); // GoogleToken

            return length;
        }
    }
    public partial class RequestFinishPasskeyLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Credential.GetLength(); // Credential
            if (flags.IsBitSet(0)) { length += 4; } // FromDcId
            if (flags.IsBitSet(0)) { length += 8; } // FromAuthKeyId

            return length;
        }
    }
    public partial class RequestFirebasePnvSignUp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName

            return length;
        }
    }
    public partial class RequestImportAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Id
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class RequestImportBotAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Flags
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash
            length += BotAuthToken.GetTLLength(); // BotAuthToken

            return length;
        }
    }
    public partial class RequestImportLoginToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class RequestImportWebTokenAuthorization
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash
            length += WebAuthToken.GetTLLength(); // WebAuthToken

            return length;
        }
    }
    public partial class RequestInitFirebasePnvLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash

            return length;
        }
    }
    public partial class RequestInitPasskeyLogin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash

            return length;
        }
    }
    public partial class RequestLogOut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestRecoverPassword
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Code.GetTLLength(); // Code
            if (flags.IsBitSet(0)) { length += NewSettings.GetLength(); } // NewSettings

            return length;
        }
    }
    public partial class RequestReportMissingCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += Mnc.GetTLLength(); // Mnc

            return length;
        }
    }
    public partial class RequestRequestFirebaseSms
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            if (flags.IsBitSet(0)) { length += SafetyNetToken.GetTLLength(); } // SafetyNetToken
            if (flags.IsBitSet(2)) { length += PlayIntegrityToken.GetTLLength(); } // PlayIntegrityToken
            if (flags.IsBitSet(1)) { length += IosPushSecret.GetTLLength(); } // IosPushSecret

            return length;
        }
    }
    public partial class RequestRequestPasswordRecovery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResendCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            if (flags.IsBitSet(0)) { length += Reason.GetTLLength(); } // Reason

            return length;
        }
    }
    public partial class RequestResetAuthorizations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetLoginEmail
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash

            return length;
        }
    }
    public partial class RequestSendCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash
            length += Settings.GetLength(); // Settings

            return length;
        }
    }
    public partial class RequestSignIn
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            if (flags.IsBitSet(0)) { length += PhoneCode.GetTLLength(); } // PhoneCode
            if (flags.IsBitSet(1)) { length += EmailVerification.GetLength(); } // EmailVerification

            return length;
        }
    }
    public partial class RequestSignUp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName

            return length;
        }
    }
}
namespace MyTelegram.Schema.Auth.LayerN
{
    public partial class RequestSendCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            if (flags.IsBitSet(0)) { length += 4; } // CurrentNumber
            length += 4; // ApiId
            length += ApiHash.GetTLLength(); // ApiHash

            return length;
        }
    }
    public partial class RequestSignUp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += PhoneNumber.GetTLLength(); // PhoneNumber
            length += PhoneCodeHash.GetTLLength(); // PhoneCodeHash
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName

            return length;
        }
    }
}
namespace MyTelegram.Schema.Bots
{
    public partial class RequestAddPreviewMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += LangCode.GetTLLength(); // LangCode
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class RequestAllowSendMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestAnswerWebhookJSONQuery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += Data.GetLength(); // Data

            return length;
        }
    }
    public partial class RequestCanSendMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestCheckDownloadFileParams
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += FileName.GetTLLength(); // FileName
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class RequestCheckUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class RequestCreateBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Name.GetTLLength(); // Name
            length += Username.GetTLLength(); // Username
            length += ManagerId.GetLength(); // ManagerId

            return length;
        }
    }
    public partial class RequestDeletePreviewMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += LangCode.GetTLLength(); // LangCode
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class RequestEditAccessSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            if (flags.IsBitSet(1)) { length += AddUsers.GetLength(); } // AddUsers

            return length;
        }
    }
    public partial class RequestEditPreviewMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += LangCode.GetTLLength(); // LangCode
            length += Media.GetLength(); // Media
            length += NewMedia.GetLength(); // NewMedia

            return length;
        }
    }
    public partial class RequestExportBotToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += 4; // Revoke

            return length;
        }
    }
    public partial class RequestGetAccessSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestGetAdminedBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetBotCommands
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Scope.GetLength(); // Scope
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetBotInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Bot.GetLength(); } // Bot
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetBotMenuButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestGetBotRecommendations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestGetPopularAppBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetPreviewInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetPreviewMedias
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestGetRequestedWebViewButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += WebappReqId.GetTLLength(); // WebappReqId

            return length;
        }
    }
    public partial class RequestInvokeWebViewCustomMethod
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += CustomMethod.GetTLLength(); // CustomMethod
            length += Params.GetLength(); // Params

            return length;
        }
    }
    public partial class RequestReorderPreviewMedias
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += LangCode.GetTLLength(); // LangCode
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReorderUsernames
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestRequestWebViewButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += Button.GetLength(); // Button

            return length;
        }
    }
    public partial class RequestResetBotCommands
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Scope.GetLength(); // Scope
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestSendCustomRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += CustomMethod.GetTLLength(); // CustomMethod
            length += Params.GetLength(); // Params

            return length;
        }
    }
    public partial class RequestSetBotBroadcastDefaultAdminRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += AdminRights.GetLength(); // AdminRights

            return length;
        }
    }
    public partial class RequestSetBotCommands
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Scope.GetLength(); // Scope
            length += LangCode.GetTLLength(); // LangCode
            length += Commands.GetLength(); // Commands

            return length;
        }
    }
    public partial class RequestSetBotGroupDefaultAdminRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += AdminRights.GetLength(); // AdminRights

            return length;
        }
    }
    public partial class RequestSetBotInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(2)) { length += Bot.GetLength(); } // Bot
            length += LangCode.GetTLLength(); // LangCode
            if (flags.IsBitSet(3)) { length += Name.GetTLLength(); } // Name
            if (flags.IsBitSet(0)) { length += About.GetTLLength(); } // About
            if (flags.IsBitSet(1)) { length += Description.GetTLLength(); } // Description

            return length;
        }
    }
    public partial class RequestSetBotMenuButton
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += Button.GetLength(); // Button

            return length;
        }
    }
    public partial class RequestSetCustomVerification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Bot.GetLength(); } // Bot
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(2)) { length += CustomDescription.GetTLLength(); } // CustomDescription

            return length;
        }
    }
    public partial class RequestSetJoinChatResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class RequestToggleUserEmojiStatusPermission
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += Username.GetTLLength(); // Username
            length += 4; // Active

            return length;
        }
    }
    public partial class RequestUpdateStarRefProgram
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            length += 4; // CommissionPermille
            if (flags.IsBitSet(0)) { length += 4; } // DurationMonths

            return length;
        }
    }
    public partial class RequestUpdateUserEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += EmojiStatus.GetLength(); // EmojiStatus

            return length;
        }
    }
}
namespace MyTelegram.Schema.Channels
{
    public partial class RequestCheckSearchPostsFlood
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Query.GetTLLength(); } // Query

            return length;
        }
    }
    public partial class RequestCheckUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
    public partial class RequestConvertToGigagroup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestCreateChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            length += About.GetTLLength(); // About
            if (flags.IsBitSet(2)) { length += GeoPoint.GetLength(); } // GeoPoint
            if (flags.IsBitSet(2)) { length += Address.GetTLLength(); } // Address
            if (flags.IsBitSet(4)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class RequestDeactivateAllUsernames
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestDeleteChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestDeleteHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestDeleteMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestDeleteParticipantHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestEditAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += UserId.GetLength(); // UserId
            length += AdminRights.GetLength(); // AdminRights
            if (flags.IsBitSet(0)) { length += Rank.GetTLLength(); } // Rank

            return length;
        }
    }
    public partial class RequestEditBanned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Participant.GetLength(); // Participant
            length += BannedRights.GetLength(); // BannedRights

            return length;
        }
    }
    public partial class RequestEditLocation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += GeoPoint.GetLength(); // GeoPoint
            length += Address.GetTLLength(); // Address

            return length;
        }
    }
    public partial class RequestEditPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Photo.GetLength(); // Photo

            return length;
        }
    }
    public partial class RequestEditTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class RequestExportMessageLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetAdminedPublicChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestGetAdminLog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += Q.GetTLLength(); // Q
            if (flags.IsBitSet(0)) { length += EventsFilter.GetLength(); } // EventsFilter
            if (flags.IsBitSet(1)) { length += Admins.GetLength(); } // Admins
            length += 8; // MaxId
            length += 8; // MinId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetChannelRecommendations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Channel.GetLength(); } // Channel

            return length;
        }
    }
    public partial class RequestGetChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetFullChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestGetGroupsForDiscussion
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetInactiveChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetLeftChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset

            return length;
        }
    }
    public partial class RequestGetMessageAuthor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestGetParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Filter.GetLength(); // Filter
            length += 4; // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSendAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestInviteToChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class RequestJoinChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestLeaveChannel
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestReadHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestReadMessageContents
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestReorderUsernames
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReportAntiSpamFalsePositive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestReportSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Participant.GetLength(); // Participant
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestRestrictSponsoredMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Restricted

            return length;
        }
    }
    public partial class RequestSearchPosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Hashtag.GetTLLength(); } // Hashtag
            if (flags.IsBitSet(1)) { length += Query.GetTLLength(); } // Query
            length += 4; // OffsetRate
            length += OffsetPeer.GetLength(); // OffsetPeer
            length += 4; // OffsetId
            length += 4; // Limit
            if (flags.IsBitSet(2)) { length += 8; } // AllowPaidStars

            return length;
        }
    }
    public partial class RequestSetBoostsToUnblockRestrictions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Boosts

            return length;
        }
    }
    public partial class RequestSetDiscussionGroup
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Broadcast.GetLength(); // Broadcast
            length += Group.GetLength(); // Group

            return length;
        }
    }
    public partial class RequestSetEmojiStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class RequestSetMainProfileTab
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Tab.GetLength(); // Tab

            return length;
        }
    }
    public partial class RequestSetStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class RequestToggleAntiSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleAutotranslation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleForum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled
            length += 4; // Tabs

            return length;
        }
    }
    public partial class RequestToggleJoinRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled
            if (flags.IsBitSet(0)) { length += GuardBot.GetLength(); } // GuardBot

            return length;
        }
    }
    public partial class RequestToggleJoinToSend
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleParticipantsHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestTogglePreHistoryHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleSignatures
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestToggleSlowMode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Seconds

            return length;
        }
    }
    public partial class RequestToggleUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Username.GetTLLength(); // Username
            length += 4; // Active

            return length;
        }
    }
    public partial class RequestToggleViewForumAsMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestUpdateColor
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            if (flags.IsBitSet(2)) { length += 4; } // Color
            if (flags.IsBitSet(0)) { length += 8; } // BackgroundEmojiId

            return length;
        }
    }
    public partial class RequestUpdateEmojiStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += EmojiStatus.GetLength(); // EmojiStatus

            return length;
        }
    }
    public partial class RequestUpdatePaidMessagesPrice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += 8; // SendPaidMessagesStars

            return length;
        }
    }
    public partial class RequestUpdateUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Username.GetTLLength(); // Username

            return length;
        }
    }
}

namespace MyTelegram.Schema.Channels.LayerN
{
    public partial class RequestExportMessageLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetChannelRecommendations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestGetMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestGetSendAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
}
namespace MyTelegram.Schema.Chatlists
{
    public partial class RequestCheckChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestDeleteExportedInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestEditExportedInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Chatlist.GetLength(); // Chatlist
            length += Slug.GetTLLength(); // Slug
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += Peers.GetLength(); } // Peers

            return length;
        }
    }
    public partial class RequestExportChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist
            length += Title.GetTLLength(); // Title
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class RequestGetChatlistUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist

            return length;
        }
    }
    public partial class RequestGetExportedInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist

            return length;
        }
    }
    public partial class RequestGetLeaveChatlistSuggestions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist

            return length;
        }
    }
    public partial class RequestHideChatlistUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist

            return length;
        }
    }
    public partial class RequestJoinChatlistInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class RequestJoinChatlistUpdates
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class RequestLeaveChatlist
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Chatlist.GetLength(); // Chatlist
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
}
namespace MyTelegram.Schema.Communities
{
    public partial class RequestCreate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += About.GetTLLength(); } // About
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetJoinedCommunities
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetParticipantJoinedChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Community.GetLength(); // Community
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestGetPeerLinkRequests
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Community.GetLength(); // Community
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestToggleAllPeerLinkRequestApproval
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Community.GetLength(); // Community

            return length;
        }
    }
    public partial class RequestToggleCommunityCollapsedInDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Community.GetLength(); // Community

            return length;
        }
    }
    public partial class RequestToggleParticipantBanned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Community.GetLength(); // Community
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestTogglePeerLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Community.GetLength(); // Community
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestTogglePeerLinkRequestApproval
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Community.GetLength(); // Community
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
}
namespace MyTelegram.Schema.Contacts
{
    public partial class RequestAcceptContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestAddContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            length += FirstName.GetTLLength(); // FirstName
            length += LastName.GetTLLength(); // LastName
            length += Phone.GetTLLength(); // Phone
            if (flags.IsBitSet(1)) { length += Note.GetLength(); } // Note

            return length;
        }
    }
    public partial class RequestBlock
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestBlockFromReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestDeleteByPhones
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Phones.GetLength(); // Phones

            return length;
        }
    }
    public partial class RequestDeleteContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestEditCloseFriends
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestExportContactToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetBirthdays
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetBlocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetContactIDs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetLocated
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += GeoPoint.GetLength(); // GeoPoint
            if (flags.IsBitSet(0)) { length += 4; } // SelfExpires

            return length;
        }
    }
    public partial class RequestGetSaved
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetSponsoredPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q

            return length;
        }
    }
    public partial class RequestGetStatuses
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetTopPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestImportContacts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Contacts.GetLength(); // Contacts

            return length;
        }
    }
    public partial class RequestImportContactToken
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Token.GetTLLength(); // Token

            return length;
        }
    }
    public partial class RequestResetSaved
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestResetTopPeerRating
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Category.GetLength(); // Category
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestResolvePhone
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Phone.GetTLLength(); // Phone

            return length;
        }
    }
    public partial class RequestResolveUsername
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Username.GetTLLength(); // Username
            if (flags.IsBitSet(0)) { length += Referer.GetTLLength(); } // Referer

            return length;
        }
    }
    public partial class RequestSearch
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Q.GetTLLength(); // Q
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestSetBlocked
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestToggleTopPeers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestUnblock
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestUpdateContactNote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += Note.GetLength(); // Note

            return length;
        }
    }
}
namespace MyTelegram.Schema.Ephemeral
{
    public partial class RequestDeleteAllWelcomeMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestDeleteMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            length += ReceiverId.GetLength(); // ReceiverId
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestDeleteWelcomeMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestEditMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(7)) { length += Peer.GetLength(); } // Peer
            length += ReceiverId.GetLength(); // ReceiverId
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Message.GetTLLength(); } // Message
            if (flags.IsBitSet(3)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(4)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class RequestGetCallbackAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(1)) { length += Data.GetTLLength(); } // Data

            return length;
        }
    }
    public partial class RequestGetWelcomeMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestReportMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            length += Option.GetTLLength(); // Option
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class RequestSendMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(8)) { length += Peer.GetLength(); } // Peer
            length += ReceiverId.GetLength(); // ReceiverId
            if (flags.IsBitSet(0)) { length += 8; } // QueryId
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(3)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(4)) { length += RichMessage.GetLength(); } // RichMessage
            length += 8; // RandomId
            if (flags.IsBitSet(5)) { length += ReplyTo.GetLength(); } // ReplyTo

            return length;
        }
    }
}

namespace MyTelegram.Schema.Folders
{
    public partial class RequestEditPeerFolders
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FolderPeers.GetLength(); // FolderPeers

            return length;
        }
    }
}
namespace MyTelegram.Schema.Fragment
{
    public partial class RequestGetCollectibleInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Collectible.GetLength(); // Collectible

            return length;
        }
    }
}
namespace MyTelegram.Schema.Help
{
    public partial class RequestAcceptTermsOfService
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestDismissSuggestion
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Suggestion.GetTLLength(); // Suggestion

            return length;
        }
    }
    public partial class RequestEditUserInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += Message.GetTLLength(); // Message
            length += Entities.GetLength(); // Entities

            return length;
        }
    }
    public partial class RequestGetAppConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetAppUpdate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Source.GetTLLength(); // Source

            return length;
        }
    }
    public partial class RequestGetCdnConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetCountriesList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetDeepLinkInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Path.GetTLLength(); // Path

            return length;
        }
    }
    public partial class RequestGetInviteText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetNearestDc
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPassportConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetPeerColors
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetPeerProfileColors
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetPremiumPromo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPromoData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetRecentMeUrls
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Referer.GetTLLength(); // Referer

            return length;
        }
    }
    public partial class RequestGetSupport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetSupportName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetTermsOfServiceUpdate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetTimezonesList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetUserInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestHidePromoData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestSaveAppLog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Events.GetLength(); // Events

            return length;
        }
    }
    public partial class RequestSetBotUpdatesStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // PendingUpdatesCount
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
}
namespace MyTelegram.Schema.Langpack
{
    public partial class RequestGetDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode
            length += 4; // FromVersion

            return length;
        }
    }
    public partial class RequestGetLangPack
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetLanguage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetLanguages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangPack.GetTLLength(); // LangPack

            return length;
        }
    }
    public partial class RequestGetStrings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangPack.GetTLLength(); // LangPack
            length += LangCode.GetTLLength(); // LangCode
            length += Keys.GetLength(); // Keys

            return length;
        }
    }
}
namespace MyTelegram.Schema.Langpack.LayerN
{
    public partial class RequestGetLangPack
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetLanguages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetStrings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode
            length += Keys.GetLength(); // Keys

            return length;
        }
    }
}
namespace MyTelegram.Schema.Messages
{
    public partial class RequestAcceptEncryption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += GB.GetTLLength(); // GB
            length += 8; // KeyFingerprint

            return length;
        }
    }
    public partial class RequestAcceptUrlAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(1)) { length += 4; } // MsgId
            if (flags.IsBitSet(1)) { length += 4; } // ButtonId
            if (flags.IsBitSet(2)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(4)) { length += MatchCode.GetTLLength(); } // MatchCode

            return length;
        }
    }
    public partial class RequestAddChatUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += UserId.GetLength(); // UserId
            length += 4; // FwdLimit

            return length;
        }
    }
    public partial class RequestAddPollAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Answer.GetLength(); // Answer

            return length;
        }
    }
    public partial class RequestAppendTodoList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += List.GetLength(); // List

            return length;
        }
    }
    public partial class RequestCheckChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class RequestCheckHistoryImport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ImportHead.GetTLLength(); // ImportHead

            return length;
        }
    }
    public partial class RequestCheckHistoryImportPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestCheckQuickReplyShortcut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Shortcut.GetTLLength(); // Shortcut

            return length;
        }
    }
    public partial class RequestCheckUrlAuthMatchCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += MatchCode.GetTLLength(); // MatchCode

            return length;
        }
    }
    public partial class RequestClearAllDrafts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestClearRecentReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestClearRecentStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestClickSponsoredMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += RandomId.GetTLLength(); // RandomId

            return length;
        }
    }
    public partial class RequestComposeMessageWithAI
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Text.GetLength(); // Text
            if (flags.IsBitSet(1)) { length += TranslateToLang.GetTLLength(); } // TranslateToLang
            if (flags.IsBitSet(2)) { length += Tone.GetLength(); } // Tone

            return length;
        }
    }
    public partial class RequestComposeRichMessageWithAI
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += Text.GetLength(); } // Text
            if (flags.IsBitSet(1)) { length += TranslateToLang.GetTLLength(); } // TranslateToLang
            if (flags.IsBitSet(2)) { length += Tone.GetLength(); } // Tone

            return length;
        }
    }
    public partial class RequestCreateChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Users.GetLength(); // Users
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += 4; } // TtlPeriod

            return length;
        }
    }
    public partial class RequestCreateForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Title.GetTLLength(); // Title
            if (flags.IsBitSet(0)) { length += 4; } // IconColor
            if (flags.IsBitSet(3)) { length += 8; } // IconEmojiId
            length += 8; // RandomId
            if (flags.IsBitSet(2)) { length += SendAs.GetLength(); } // SendAs

            return length;
        }
    }
    public partial class RequestDeclineUrlAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url

            return length;
        }
    }
    public partial class RequestDeleteChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class RequestDeleteChatUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // ChatId
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestDeleteExportedChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Link.GetTLLength(); // Link

            return length;
        }
    }
    public partial class RequestDeleteFactCheck
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestDeleteHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId
            if (flags.IsBitSet(2)) { length += 4; } // MinDate
            if (flags.IsBitSet(3)) { length += 4; } // MaxDate

            return length;
        }
    }
    public partial class RequestDeleteMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestDeleteParticipantReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestDeleteParticipantReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestDeletePhoneCallHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestDeletePollAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Option.GetTLLength(); // Option

            return length;
        }
    }
    public partial class RequestDeleteQuickReplyMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestDeleteQuickReplyShortcut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId

            return length;
        }
    }
    public partial class RequestDeleteRevokedExportedChatInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += AdminId.GetLength(); // AdminId

            return length;
        }
    }
    public partial class RequestDeleteSavedHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId
            if (flags.IsBitSet(2)) { length += 4; } // MinDate
            if (flags.IsBitSet(3)) { length += 4; } // MaxDate

            return length;
        }
    }
    public partial class RequestDeleteScheduledMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestDeleteTopicHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // TopMsgId

            return length;
        }
    }
    public partial class RequestDiscardEncryption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ChatId

            return length;
        }
    }
    public partial class RequestEditChatAbout
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += About.GetTLLength(); // About

            return length;
        }
    }
    public partial class RequestEditChatAdmin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += UserId.GetLength(); // UserId
            length += 4; // IsAdmin

            return length;
        }
    }
    public partial class RequestEditChatCreator
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += UserId.GetLength(); // UserId
            length += Password.GetLength(); // Password

            return length;
        }
    }
    public partial class RequestEditChatDefaultBannedRights
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += BannedRights.GetLength(); // BannedRights

            return length;
        }
    }
    public partial class RequestEditChatParticipantRank
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Participant.GetLength(); // Participant
            length += Rank.GetTLLength(); // Rank

            return length;
        }
    }
    public partial class RequestEditChatPhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += Photo.GetLength(); // Photo

            return length;
        }
    }
    public partial class RequestEditChatTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class RequestEditExportedChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Link.GetTLLength(); // Link
            if (flags.IsBitSet(0)) { length += 4; } // ExpireDate
            if (flags.IsBitSet(1)) { length += 4; } // UsageLimit
            if (flags.IsBitSet(3)) { length += 4; } // RequestNeeded
            if (flags.IsBitSet(4)) { length += Title.GetTLLength(); } // Title

            return length;
        }
    }
    public partial class RequestEditFactCheck
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Text.GetLength(); // Text

            return length;
        }
    }
    public partial class RequestEditForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // TopicId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += 8; } // IconEmojiId
            if (flags.IsBitSet(2)) { length += 4; } // Closed
            if (flags.IsBitSet(3)) { length += 4; } // Hidden

            return length;
        }
    }
    public partial class RequestEditInlineBotMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            if (flags.IsBitSet(11)) { length += Message.GetTLLength(); } // Message
            if (flags.IsBitSet(14)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(23)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class RequestEditMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(11)) { length += Message.GetTLLength(); } // Message
            if (flags.IsBitSet(14)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(15)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(18)) { length += 4; } // ScheduleRepeatPeriod
            if (flags.IsBitSet(17)) { length += 4; } // QuickReplyShortcutId
            if (flags.IsBitSet(23)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class RequestEditQuickReplyShortcut
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // ShortcutId
            length += Shortcut.GetTLLength(); // Shortcut

            return length;
        }
    }
    public partial class RequestExportChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // ExpireDate
            if (flags.IsBitSet(1)) { length += 4; } // UsageLimit
            if (flags.IsBitSet(4)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(5)) { length += SubscriptionPricing.GetLength(); } // SubscriptionPricing

            return length;
        }
    }
    public partial class RequestFaveSticker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += 4; // Unfave

            return length;
        }
    }
    public partial class RequestForwardMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += FromPeer.GetLength(); // FromPeer
            length += Id.GetLength(); // Id
            length += RandomId.GetLength(); // RandomId
            length += ToPeer.GetLength(); // ToPeer
            if (flags.IsBitSet(9)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(22)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(24)) { length += 4; } // ScheduleRepeatPeriod
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(18)) { length += 8; } // Effect
            if (flags.IsBitSet(20)) { length += 4; } // VideoTimestamp
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars
            if (flags.IsBitSet(23)) { length += SuggestedPost.GetLength(); } // SuggestedPost

            return length;
        }
    }
    public partial class RequestGetAdminsWithInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetAllDrafts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAllStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetArchivedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetAttachedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class RequestGetAttachMenuBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestGetAttachMenuBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetAvailableEffects
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetAvailableReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetBotApp
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += App.GetLength(); // App
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetBotCallbackAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            if (flags.IsBitSet(0)) { length += Data.GetTLLength(); } // Data
            if (flags.IsBitSet(2)) { length += Password.GetLength(); } // Password

            return length;
        }
    }
    public partial class RequestGetChatInviteImporters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += Link.GetTLLength(); } // Link
            if (flags.IsBitSet(2)) { length += Q.GetTLLength(); } // Q
            length += 4; // OffsetDate
            length += OffsetUser.GetLength(); // OffsetUser
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetCommonChats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += 8; // MaxId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetCustomEmojiDocuments
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += DocumentId.GetLength(); // DocumentId

            return length;
        }
    }
    public partial class RequestGetDefaultHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetDefaultTagReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetDhConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Version
            length += 4; // RandomLength

            return length;
        }
    }
    public partial class RequestGetDialogFilters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += 4; } // FolderId
            length += 4; // OffsetDate
            length += 4; // OffsetId
            length += OffsetPeer.GetLength(); // OffsetPeer
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetDialogUnreadMarks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += ParentPeer.GetLength(); } // ParentPeer

            return length;
        }
    }
    public partial class RequestGetDiscussionMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetDocumentByHash
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Sha256.GetTLLength(); // Sha256
            length += 8; // Size
            length += MimeType.GetTLLength(); // MimeType

            return length;
        }
    }
    public partial class RequestGetEmojiGameInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetEmojiGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetEmojiKeywords
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetEmojiKeywordsDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode
            length += 4; // FromVersion

            return length;
        }
    }
    public partial class RequestGetEmojiKeywordsLanguages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCodes.GetLength(); // LangCodes

            return length;
        }
    }
    public partial class RequestGetEmojiProfilePhotoGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetEmojiStatusGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetEmojiStickerGroups
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetEmojiStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetEmojiURL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += LangCode.GetTLLength(); // LangCode

            return length;
        }
    }
    public partial class RequestGetExportedChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Link.GetTLLength(); // Link

            return length;
        }
    }
    public partial class RequestGetExportedChatInvites
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += AdminId.GetLength(); // AdminId
            if (flags.IsBitSet(2)) { length += 4; } // OffsetDate
            if (flags.IsBitSet(2)) { length += OffsetLink.GetTLLength(); } // OffsetLink
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetExtendedMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetFactCheck
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += MsgId.GetLength(); // MsgId

            return length;
        }
    }
    public partial class RequestGetFavedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetFeaturedEmojiStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetFeaturedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetForumTopics
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Q.GetTLLength(); } // Q
            length += 4; // OffsetDate
            length += 4; // OffsetId
            length += 4; // OffsetTopic
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetForumTopicsByID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Topics.GetLength(); // Topics

            return length;
        }
    }
    public partial class RequestGetFullChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class RequestGetFutureChatCreatorAfterLeave
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetGameHighScores
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestGetHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // OffsetId
            length += 4; // OffsetDate
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetInlineBotResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += GeoPoint.GetLength(); } // GeoPoint
            length += Query.GetTLLength(); // Query
            length += Offset.GetTLLength(); // Offset

            return length;
        }
    }
    public partial class RequestGetInlineGameHighScores
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestGetMaskStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetMessageEditData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetMessageReactionsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Reaction.GetLength(); } // Reaction
            if (flags.IsBitSet(1)) { length += Offset.GetTLLength(); } // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetMessageReadParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetMessagesReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetMessagesViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id
            length += 4; // Increment

            return length;
        }
    }
    public partial class RequestGetMyStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetOldFeaturedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetOnlines
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetOutboxReadDate
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetPaidReactionPrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPeerDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peers.GetLength(); // Peers

            return length;
        }
    }
    public partial class RequestGetPeerSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetPersonalChannelHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetPinnedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // FolderId

            return length;
        }
    }
    public partial class RequestGetPinnedSavedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPollResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // PollHash

            return length;
        }
    }
    public partial class RequestGetPollVotes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Option.GetTLLength(); } // Option
            if (flags.IsBitSet(1)) { length += Offset.GetTLLength(); } // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetPreparedInlineMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += Id.GetTLLength(); // Id

            return length;
        }
    }
    public partial class RequestGetQuickReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetQuickReplyMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // ShortcutId
            if (flags.IsBitSet(0)) { length += Id.GetLength(); } // Id
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetRecentLocations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetRecentReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetRecentStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 4; // OffsetId
            length += 4; // OffsetDate
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetSavedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += 4; // OffsetDate
            length += 4; // OffsetId
            length += OffsetPeer.GetLength(); // OffsetPeer
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedDialogsByID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += Ids.GetLength(); // Ids

            return length;
        }
    }
    public partial class RequestGetSavedGifs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += Peer.GetLength(); // Peer
            length += 4; // OffsetId
            length += 4; // OffsetDate
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedReactionTags
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetScheduledHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetScheduledMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetSearchCounters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(2)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += Filters.GetLength(); // Filters

            return length;
        }
    }
    public partial class RequestGetSearchResultsCalendar
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(2)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += Filter.GetLength(); // Filter
            length += 4; // OffsetId
            length += 4; // OffsetDate

            return length;
        }
    }
    public partial class RequestGetSearchResultsPositions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(2)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += Filter.GetLength(); // Filter
            length += 4; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetSplitRanges
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetSponsoredMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // MsgId

            return length;
        }
    }
    public partial class RequestGetStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetSuggestedDialogFilters
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetTopReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetUnreadMentions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += 4; // OffsetId
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId

            return length;
        }
    }
    public partial class RequestGetUnreadPollVotes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += 4; // OffsetId
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId

            return length;
        }
    }
    public partial class RequestGetUnreadReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            length += 4; // OffsetId
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId

            return length;
        }
    }
    public partial class RequestGetWebPage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Url.GetTLLength(); // Url
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetWebPagePreview
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities

            return length;
        }
    }
    public partial class RequestHideAllChatJoinRequests
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += Link.GetTLLength(); } // Link

            return length;
        }
    }
    public partial class RequestHideChatJoinRequest
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestHidePeerSettingsBar
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestImportChatInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Hash.GetTLLength(); // Hash

            return length;
        }
    }
    public partial class RequestInitHistoryImport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += File.GetLength(); // File
            length += 4; // MediaCount

            return length;
        }
    }
    public partial class RequestInstallStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += 4; // Archived

            return length;
        }
    }
    public partial class RequestMarkDialogUnread
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += ParentPeer.GetLength(); } // ParentPeer
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestMigrateChat
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // ChatId

            return length;
        }
    }
    public partial class RequestProlongWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Bot.GetLength(); // Bot
            length += 8; // QueryId
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs

            return length;
        }
    }
    public partial class RequestRateTranscribedAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 8; // TranscriptionId
            length += 4; // Good

            return length;
        }
    }
    public partial class RequestReadDiscussion
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 4; // ReadMaxId

            return length;
        }
    }
    public partial class RequestReadEncryptedHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MaxDate

            return length;
        }
    }
    public partial class RequestReadFeaturedStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestReadHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestReadMentions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId

            return length;
        }
    }
    public partial class RequestReadMessageContents
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestReadPollVotes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId

            return length;
        }
    }
    public partial class RequestReadReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId

            return length;
        }
    }
    public partial class RequestReadSavedHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ParentPeer.GetLength(); // ParentPeer
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestReceivedMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestReceivedQueue
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MaxQts

            return length;
        }
    }
    public partial class RequestReorderPinnedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // FolderId
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReorderPinnedForumTopics
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReorderPinnedSavedDialogs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReorderQuickReplies
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReorderStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id
            length += Option.GetTLLength(); // Option
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class RequestReportEncryptedSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestReportMessagesDelivery
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestReportMusicListen
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += 4; // ListenedDuration

            return length;
        }
    }
    public partial class RequestReportReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            length += ReactionPeer.GetLength(); // ReactionPeer

            return length;
        }
    }
    public partial class RequestReportReadMetrics
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Metrics.GetLength(); // Metrics

            return length;
        }
    }
    public partial class RequestReportSpam
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestReportSponsoredMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += RandomId.GetTLLength(); // RandomId
            length += Option.GetTLLength(); // Option

            return length;
        }
    }
    public partial class RequestRequestAppWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += App.GetLength(); // App
            if (flags.IsBitSet(1)) { length += StartParam.GetTLLength(); } // StartParam
            if (flags.IsBitSet(2)) { length += ThemeParams.GetLength(); } // ThemeParams
            length += Platform.GetTLLength(); // Platform

            return length;
        }
    }
    public partial class RequestRequestChatJoinWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            if (flags.IsBitSet(0)) { length += ThemeParams.GetLength(); } // ThemeParams
            length += Platform.GetTLLength(); // Platform

            return length;
        }
    }
    public partial class RequestRequestEncryption
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += 4; // RandomId
            length += GA.GetTLLength(); // GA

            return length;
        }
    }
    public partial class RequestRequestMainWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Bot.GetLength(); // Bot
            if (flags.IsBitSet(1)) { length += StartParam.GetTLLength(); } // StartParam
            if (flags.IsBitSet(0)) { length += ThemeParams.GetLength(); } // ThemeParams
            length += Platform.GetTLLength(); // Platform

            return length;
        }
    }
    public partial class RequestRequestSimpleWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            if (flags.IsBitSet(3)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(4)) { length += StartParam.GetTLLength(); } // StartParam
            if (flags.IsBitSet(0)) { length += ThemeParams.GetLength(); } // ThemeParams
            length += Platform.GetTLLength(); // Platform

            return length;
        }
    }
    public partial class RequestRequestUrlAuth
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(1)) { length += 4; } // MsgId
            if (flags.IsBitSet(1)) { length += 4; } // ButtonId
            if (flags.IsBitSet(2)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(3)) { length += InAppOrigin.GetTLLength(); } // InAppOrigin

            return length;
        }
    }
    public partial class RequestRequestWebView
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Bot.GetLength(); // Bot
            if (flags.IsBitSet(1)) { length += Url.GetTLLength(); } // Url
            if (flags.IsBitSet(3)) { length += StartParam.GetTLLength(); } // StartParam
            if (flags.IsBitSet(2)) { length += ThemeParams.GetLength(); } // ThemeParams
            length += Platform.GetTLLength(); // Platform
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs

            return length;
        }
    }
    public partial class RequestSaveDefaultSendAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += SendAs.GetLength(); // SendAs

            return length;
        }
    }
    public partial class RequestSaveDraft
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(4)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += Peer.GetLength(); // Peer
            length += Message.GetTLLength(); // Message
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(5)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(7)) { length += 8; } // Effect
            if (flags.IsBitSet(8)) { length += SuggestedPost.GetLength(); } // SuggestedPost
            if (flags.IsBitSet(9)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class RequestSaveGif
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += 4; // Unsave

            return length;
        }
    }
    public partial class RequestSavePreparedInlineMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Result.GetLength(); // Result
            length += UserId.GetLength(); // UserId
            if (flags.IsBitSet(0)) { length += PeerTypes.GetLength(); } // PeerTypes

            return length;
        }
    }
    public partial class RequestSaveRecentSticker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            length += 4; // Unsave

            return length;
        }
    }
    public partial class RequestSearch
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Q.GetTLLength(); // Q
            if (flags.IsBitSet(0)) { length += FromId.GetLength(); } // FromId
            if (flags.IsBitSet(2)) { length += SavedPeerId.GetLength(); } // SavedPeerId
            if (flags.IsBitSet(3)) { length += SavedReaction.GetLength(); } // SavedReaction
            if (flags.IsBitSet(1)) { length += 4; } // TopMsgId
            length += Filter.GetLength(); // Filter
            length += 4; // MinDate
            length += 4; // MaxDate
            length += 4; // OffsetId
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSearchCustomEmoji
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Emoticon.GetTLLength(); // Emoticon
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSearchEmojiStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Q.GetTLLength(); // Q
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSearchGlobal
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 4; } // FolderId
            if (flags.IsBitSet(4)) { length += Community.GetLength(); } // Community
            length += Q.GetTLLength(); // Q
            length += Filter.GetLength(); // Filter
            length += 4; // MinDate
            length += 4; // MaxDate
            length += 4; // OffsetRate
            length += OffsetPeer.GetLength(); // OffsetPeer
            length += 4; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestSearchSentMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Q.GetTLLength(); // Q
            length += Filter.GetLength(); // Filter
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestSearchStickers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Q.GetTLLength(); // Q
            length += Emoticon.GetTLLength(); // Emoticon
            length += LangCode.GetLength(); // LangCode
            length += 4; // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSearchStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Q.GetTLLength(); // Q
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestSendBotRequestedPeer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // MsgId
            if (flags.IsBitSet(1)) { length += WebappReqId.GetTLLength(); } // WebappReqId
            length += 4; // ButtonId
            length += RequestedPeers.GetLength(); // RequestedPeers

            return length;
        }
    }
    public partial class RequestSendEncrypted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // RandomId
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class RequestSendEncryptedFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 8; // RandomId
            length += Data.GetTLLength(); // Data
            length += File.GetLength(); // File

            return length;
        }
    }
    public partial class RequestSendEncryptedService
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // RandomId
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class RequestSendInlineBotResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += 8; // RandomId
            length += 8; // QueryId
            length += Id.GetTLLength(); // Id
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars

            return length;
        }
    }
    public partial class RequestSendMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += Media.GetLength(); // Media
            length += Message.GetTLLength(); // Message
            length += 8; // RandomId
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(24)) { length += 4; } // ScheduleRepeatPeriod
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(18)) { length += 8; } // Effect
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars
            if (flags.IsBitSet(22)) { length += SuggestedPost.GetLength(); } // SuggestedPost

            return length;
        }
    }
    public partial class RequestSendMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += Message.GetTLLength(); // Message
            length += 8; // RandomId
            if (flags.IsBitSet(2)) { length += ReplyMarkup.GetLength(); } // ReplyMarkup
            if (flags.IsBitSet(3)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(24)) { length += 4; } // ScheduleRepeatPeriod
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(18)) { length += 8; } // Effect
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars
            if (flags.IsBitSet(22)) { length += SuggestedPost.GetLength(); } // SuggestedPost
            if (flags.IsBitSet(23)) { length += RichMessage.GetLength(); } // RichMessage

            return length;
        }
    }
    public partial class RequestSendMultiMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += ReplyTo.GetLength(); } // ReplyTo
            length += MultiMedia.GetLength(); // MultiMedia
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(18)) { length += 8; } // Effect
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars

            return length;
        }
    }
    public partial class RequestSendPaidReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += 4; // Count
            length += 8; // RandomId
            if (flags.IsBitSet(0)) { length += Private.GetLength(); } // Private

            return length;
        }
    }
    public partial class RequestSendQuickReplyMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // ShortcutId
            length += Id.GetLength(); // Id
            length += RandomId.GetLength(); // RandomId

            return length;
        }
    }
    public partial class RequestSendReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            if (flags.IsBitSet(0)) { length += Reaction.GetLength(); } // Reaction

            return length;
        }
    }
    public partial class RequestSendScheduledMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestSendScreenshotNotification
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += ReplyTo.GetLength(); // ReplyTo
            length += 8; // RandomId

            return length;
        }
    }
    public partial class RequestSendVote
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Options.GetLength(); // Options

            return length;
        }
    }
    public partial class RequestSendWebViewData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += 8; // RandomId
            length += ButtonText.GetTLLength(); // ButtonText
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class RequestSendWebViewResultMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += BotQueryId.GetTLLength(); // BotQueryId
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class RequestSetBotCallbackAnswer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            if (flags.IsBitSet(0)) { length += Message.GetTLLength(); } // Message
            if (flags.IsBitSet(2)) { length += Url.GetTLLength(); } // Url
            length += 4; // CacheTime

            return length;
        }
    }
    public partial class RequestSetBotGuestChatResult
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // QueryId
            length += Result.GetLength(); // Result

            return length;
        }
    }
    public partial class RequestSetBotPrecheckoutResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            if (flags.IsBitSet(0)) { length += Error.GetTLLength(); } // Error

            return length;
        }
    }
    public partial class RequestSetBotShippingResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            if (flags.IsBitSet(0)) { length += Error.GetTLLength(); } // Error
            if (flags.IsBitSet(1)) { length += ShippingOptions.GetLength(); } // ShippingOptions

            return length;
        }
    }
    public partial class RequestSetChatAvailableReactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += AvailableReactions.GetLength(); // AvailableReactions
            if (flags.IsBitSet(0)) { length += 4; } // ReactionsLimit
            if (flags.IsBitSet(1)) { length += 4; } // PaidEnabled

            return length;
        }
    }
    public partial class RequestSetChatTheme
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Theme.GetLength(); // Theme

            return length;
        }
    }
    public partial class RequestSetChatWallPaper
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Wallpaper.GetLength(); } // Wallpaper
            if (flags.IsBitSet(2)) { length += Settings.GetLength(); } // Settings
            if (flags.IsBitSet(1)) { length += 4; } // Id

            return length;
        }
    }
    public partial class RequestSetDefaultHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Period

            return length;
        }
    }
    public partial class RequestSetDefaultReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class RequestSetEncryptedTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Typing

            return length;
        }
    }
    public partial class RequestSetGameScore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            length += UserId.GetLength(); // UserId
            length += 4; // Score

            return length;
        }
    }
    public partial class RequestSetHistoryTTL
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Period

            return length;
        }
    }
    public partial class RequestSetInlineBotResults
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // QueryId
            length += Results.GetLength(); // Results
            length += 4; // CacheTime
            if (flags.IsBitSet(2)) { length += NextOffset.GetTLLength(); } // NextOffset
            if (flags.IsBitSet(3)) { length += SwitchPm.GetLength(); } // SwitchPm
            if (flags.IsBitSet(4)) { length += SwitchWebview.GetLength(); } // SwitchWebview

            return length;
        }
    }
    public partial class RequestSetInlineGameScore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Id.GetLength(); // Id
            length += UserId.GetLength(); // UserId
            length += 4; // Score

            return length;
        }
    }
    public partial class RequestSetTyping
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            length += Action.GetLength(); // Action

            return length;
        }
    }
    public partial class RequestStartBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Bot.GetLength(); // Bot
            length += Peer.GetLength(); // Peer
            length += 8; // RandomId
            length += StartParam.GetTLLength(); // StartParam

            return length;
        }
    }
    public partial class RequestStartHistoryImport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // ImportId

            return length;
        }
    }
    public partial class RequestSummarizeText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += ToLang.GetTLLength(); } // ToLang
            if (flags.IsBitSet(2)) { length += Tone.GetTLLength(); } // Tone

            return length;
        }
    }
    public partial class RequestToggleBotInAttachMenu
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Bot.GetLength(); // Bot
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleDialogFilterTags
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Enabled

            return length;
        }
    }
    public partial class RequestToggleDialogPin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestToggleNoForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Enabled
            if (flags.IsBitSet(0)) { length += 4; } // RequestMsgId

            return length;
        }
    }
    public partial class RequestTogglePaidReactionPrivacy
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Private.GetLength(); // Private

            return length;
        }
    }
    public partial class RequestTogglePeerTranslations
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestToggleSavedDialogPin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestToggleStickerSets
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stickersets.GetLength(); // Stickersets

            return length;
        }
    }
    public partial class RequestToggleSuggestedPostApproval
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            if (flags.IsBitSet(0)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(2)) { length += RejectComment.GetTLLength(); } // RejectComment

            return length;
        }
    }
    public partial class RequestToggleTodoCompleted
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId
            length += Completed.GetLength(); // Completed
            length += Incompleted.GetLength(); // Incompleted

            return length;
        }
    }
    public partial class RequestTranscribeAudio
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestTranslateRichMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(0)) { length += Id.GetLength(); } // Id
            if (flags.IsBitSet(1)) { length += Text.GetLength(); } // Text
            length += ToLang.GetTLLength(); // ToLang
            if (flags.IsBitSet(2)) { length += Tone.GetTLLength(); } // Tone

            return length;
        }
    }
    public partial class RequestTranslateText
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Peer.GetLength(); } // Peer
            if (flags.IsBitSet(0)) { length += Id.GetLength(); } // Id
            if (flags.IsBitSet(1)) { length += Text.GetLength(); } // Text
            length += ToLang.GetTLLength(); // ToLang
            if (flags.IsBitSet(2)) { length += Tone.GetTLLength(); } // Tone

            return length;
        }
    }
    public partial class RequestUninstallStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class RequestUnpinAllMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(1)) { length += SavedPeerId.GetLength(); } // SavedPeerId

            return length;
        }
    }
    public partial class RequestUpdateDialogFilter
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Filter.GetLength(); } // Filter

            return length;
        }
    }
    public partial class RequestUpdateDialogFiltersOrder
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestUpdatePinnedForumTopic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // TopicId
            length += 4; // Pinned

            return length;
        }
    }
    public partial class RequestUpdatePinnedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestUpdateSavedReactionTag
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Reaction.GetLength(); // Reaction
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title

            return length;
        }
    }
    public partial class RequestUploadEncryptedFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += File.GetLength(); // File

            return length;
        }
    }
    public partial class RequestUploadImportedMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // ImportId
            length += FileName.GetTLLength(); // FileName
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class RequestUploadMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += BusinessConnectionId.GetTLLength(); } // BusinessConnectionId
            length += Peer.GetLength(); // Peer
            length += Media.GetLength(); // Media

            return length;
        }
    }
    public partial class RequestViewSponsoredMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += RandomId.GetTLLength(); // RandomId

            return length;
        }
    }
}
namespace MyTelegram.Schema.Messages.LayerN
{
    public partial class RequestForwardMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += FromPeer.GetLength(); // FromPeer
            length += Id.GetLength(); // Id
            length += RandomId.GetLength(); // RandomId
            length += ToPeer.GetLength(); // ToPeer
            if (flags.IsBitSet(9)) { length += 4; } // TopMsgId
            if (flags.IsBitSet(22)) { length += ReplyTo.GetLength(); } // ReplyTo
            if (flags.IsBitSet(10)) { length += 4; } // ScheduleDate
            if (flags.IsBitSet(24)) { length += 4; } // ScheduleRepeatPeriod
            if (flags.IsBitSet(13)) { length += SendAs.GetLength(); } // SendAs
            if (flags.IsBitSet(17)) { length += QuickReplyShortcut.GetLength(); } // QuickReplyShortcut
            if (flags.IsBitSet(20)) { length += 4; } // VideoTimestamp
            if (flags.IsBitSet(21)) { length += 8; } // AllowPaidStars
            if (flags.IsBitSet(23)) { length += SuggestedPost.GetLength(); } // SuggestedPost

            return length;
        }
    }
    public partial class RequestGetHistory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // OffsetId
            length += 4; // OffsetDate
            length += 4; // AddOffset
            length += 4; // Limit
            length += 4; // MaxId
            length += 4; // MinId

            return length;
        }
    }
    public partial class RequestGetMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class RequestUnpinAllMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestUploadMedia
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Media.GetLength(); // Media

            return length;
        }
    }
}
namespace MyTelegram.Schema.Payments
{
    public partial class RequestApplyGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestAssignAppStoreTransaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Receipt.GetTLLength(); // Receipt
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class RequestAssignPlayMarketTransaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Receipt.GetLength(); // Receipt
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class RequestBotCancelStarsSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += UserId.GetLength(); // UserId
            length += ChargeId.GetTLLength(); // ChargeId

            return length;
        }
    }
    public partial class RequestCanPurchaseStore
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class RequestChangeStarsSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += SubscriptionId.GetTLLength(); // SubscriptionId
            if (flags.IsBitSet(0)) { length += 4; } // Canceled

            return length;
        }
    }
    public partial class RequestCheckCanSendGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId

            return length;
        }
    }
    public partial class RequestCheckGiftCode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestClearSavedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestConnectStarRefBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestConvertStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestCraftStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestCreateStarGiftCollection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Title.GetTLLength(); // Title
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestDeleteStarGiftCollection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // CollectionId

            return length;
        }
    }
    public partial class RequestEditConnectedStarRefBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Link.GetTLLength(); // Link

            return length;
        }
    }
    public partial class RequestExportInvoice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += InvoiceMedia.GetLength(); // InvoiceMedia

            return length;
        }
    }
    public partial class RequestFulfillStarsSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += SubscriptionId.GetTLLength(); // SubscriptionId

            return length;
        }
    }
    public partial class RequestGetBankCardData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Number.GetTLLength(); // Number

            return length;
        }
    }
    public partial class RequestGetConnectedStarRefBot
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Bot.GetLength(); // Bot

            return length;
        }
    }
    public partial class RequestGetConnectedStarRefBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(2)) { length += 4; } // OffsetDate
            if (flags.IsBitSet(2)) { length += OffsetLink.GetTLLength(); } // OffsetLink
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetCraftStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetGiveawayInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetPaymentForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Invoice.GetLength(); // Invoice
            if (flags.IsBitSet(0)) { length += ThemeParams.GetLength(); } // ThemeParams

            return length;
        }
    }
    public partial class RequestGetPaymentReceipt
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetPremiumGiftCodeOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += BoostPeer.GetLength(); } // BoostPeer

            return length;
        }
    }
    public partial class RequestGetResaleStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += 8; } // AttributesHash
            length += 8; // GiftId
            if (flags.IsBitSet(3)) { length += Attributes.GetLength(); } // Attributes
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetSavedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetSavedStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestGetSavedStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(6)) { length += 4; } // CollectionId
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStarGiftActiveAuctions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetStarGiftAuctionAcquiredGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId

            return length;
        }
    }
    public partial class RequestGetStarGiftAuctionState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Auction.GetLength(); // Auction
            length += 4; // Version

            return length;
        }
    }
    public partial class RequestGetStarGiftCollections
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetStarGifts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hash

            return length;
        }
    }
    public partial class RequestGetStarGiftUpgradeAttributes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId

            return length;
        }
    }
    public partial class RequestGetStarGiftUpgradePreview
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // GiftId

            return length;
        }
    }
    public partial class RequestGetStarGiftWithdrawalUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift
            length += Password.GetLength(); // Password

            return length;
        }
    }
    public partial class RequestGetStarsGiftOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += UserId.GetLength(); } // UserId

            return length;
        }
    }
    public partial class RequestGetStarsGiveawayOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetStarsRevenueAdsAccountUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetStarsRevenueStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetStarsRevenueWithdrawalUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += 8; } // Amount
            length += Password.GetLength(); // Password

            return length;
        }
    }
    public partial class RequestGetStarsStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetStarsSubscriptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Offset.GetTLLength(); // Offset

            return length;
        }
    }
    public partial class RequestGetStarsTopupOptions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetStarsTransactions
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(3)) { length += SubscriptionId.GetTLLength(); } // SubscriptionId
            length += Peer.GetLength(); // Peer
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStarsTransactionsByID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetSuggestedStarRefBots
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetUniqueStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestGetUniqueStarGiftValueInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Slug.GetTLLength(); // Slug

            return length;
        }
    }
    public partial class RequestLaunchPrepaidGiveaway
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // GiveawayId
            length += Purpose.GetLength(); // Purpose

            return length;
        }
    }
    public partial class RequestRefundStarsCharge
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += ChargeId.GetTLLength(); // ChargeId

            return length;
        }
    }
    public partial class RequestReorderStarGiftCollections
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestResolveStarGiftOffer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // OfferMsgId

            return length;
        }
    }
    public partial class RequestSaveStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestSendPaymentForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 8; // FormId
            length += Invoice.GetLength(); // Invoice
            if (flags.IsBitSet(0)) { length += RequestedInfoId.GetTLLength(); } // RequestedInfoId
            if (flags.IsBitSet(1)) { length += ShippingOptionId.GetTLLength(); } // ShippingOptionId
            length += Credentials.GetLength(); // Credentials
            if (flags.IsBitSet(2)) { length += 8; } // TipAmount

            return length;
        }
    }
    public partial class RequestSendStarGiftOffer
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Slug.GetTLLength(); // Slug
            length += Price.GetLength(); // Price
            length += 4; // Duration
            length += 8; // RandomId
            if (flags.IsBitSet(0)) { length += 8; } // AllowPaidStars

            return length;
        }
    }
    public partial class RequestSendStarsForm
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // FormId
            length += Invoice.GetLength(); // Invoice

            return length;
        }
    }
    public partial class RequestToggleChatStarGiftNotifications
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestToggleStarGiftsPinnedToTop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestTransferStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift
            length += ToId.GetLength(); // ToId

            return length;
        }
    }
    public partial class RequestUpdateStarGiftCollection
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // CollectionId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += DeleteStargift.GetLength(); } // DeleteStargift
            if (flags.IsBitSet(2)) { length += AddStargift.GetLength(); } // AddStargift
            if (flags.IsBitSet(3)) { length += Order.GetLength(); } // Order

            return length;
        }
    }
    public partial class RequestUpdateStarGiftPrice
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stargift.GetLength(); // Stargift
            length += ResellAmount.GetLength(); // ResellAmount

            return length;
        }
    }
    public partial class RequestUpgradeStarGift
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stargift.GetLength(); // Stargift

            return length;
        }
    }
    public partial class RequestValidateRequestedInfo
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Invoice.GetLength(); // Invoice
            length += Info.GetLength(); // Info

            return length;
        }
    }
}
namespace MyTelegram.Schema.Phone
{
    public partial class RequestAcceptCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += GB.GetTLLength(); // GB
            length += Protocol.GetLength(); // Protocol

            return length;
        }
    }
    public partial class RequestCheckGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Sources.GetLength(); // Sources

            return length;
        }
    }
    public partial class RequestConfirmCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += GA.GetTLLength(); // GA
            length += 8; // KeyFingerprint
            length += Protocol.GetLength(); // Protocol

            return length;
        }
    }
    public partial class RequestCreateConferenceCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // RandomId
            if (flags.IsBitSet(3)) { length += 32; } // PublicKey
            if (flags.IsBitSet(3)) { length += Block.GetTLLength(); } // Block
            if (flags.IsBitSet(3)) { length += Params.GetLength(); } // Params

            return length;
        }
    }
    public partial class RequestCreateGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // RandomId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += 4; } // ScheduleDate

            return length;
        }
    }
    public partial class RequestDeclineConferenceCallInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestDeleteConferenceCallParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += Ids.GetLength(); // Ids
            length += Block.GetTLLength(); // Block

            return length;
        }
    }
    public partial class RequestDeleteGroupCallMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += Messages.GetLength(); // Messages

            return length;
        }
    }
    public partial class RequestDeleteGroupCallParticipantMessages
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += Participant.GetLength(); // Participant

            return length;
        }
    }
    public partial class RequestDiscardCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Duration
            length += Reason.GetLength(); // Reason
            length += 8; // ConnectionId

            return length;
        }
    }
    public partial class RequestDiscardGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestEditGroupCallParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += Participant.GetLength(); // Participant
            if (flags.IsBitSet(0)) { length += 4; } // Muted
            if (flags.IsBitSet(1)) { length += 4; } // Volume
            if (flags.IsBitSet(2)) { length += 4; } // RaiseHand
            if (flags.IsBitSet(3)) { length += 4; } // VideoStopped
            if (flags.IsBitSet(4)) { length += 4; } // VideoPaused
            if (flags.IsBitSet(5)) { length += 4; } // PresentationPaused

            return length;
        }
    }
    public partial class RequestEditGroupCallTitle
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class RequestExportGroupCallInvite
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestGetCallConfig
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetGroupCallChainBlocks
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // SubChainId
            length += 4; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetGroupCallJoinAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetGroupCallStars
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestGetGroupCallStreamChannels
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestGetGroupCallStreamRtmpUrl
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Revoke

            return length;
        }
    }
    public partial class RequestGetGroupParticipants
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Ids.GetLength(); // Ids
            length += Sources.GetLength(); // Sources
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestInviteConferenceCallParticipant
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
    public partial class RequestInviteToGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Users.GetLength(); // Users

            return length;
        }
    }
    public partial class RequestJoinGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += JoinAs.GetLength(); // JoinAs
            if (flags.IsBitSet(1)) { length += InviteHash.GetTLLength(); } // InviteHash
            if (flags.IsBitSet(3)) { length += 32; } // PublicKey
            if (flags.IsBitSet(3)) { length += Block.GetTLLength(); } // Block
            length += Params.GetLength(); // Params

            return length;
        }
    }
    public partial class RequestJoinGroupCallPresentation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Params.GetLength(); // Params

            return length;
        }
    }
    public partial class RequestLeaveGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // Source

            return length;
        }
    }
    public partial class RequestLeaveGroupCallPresentation
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestReceivedCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestRequestCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += UserId.GetLength(); // UserId
            length += 4; // RandomId
            length += GAHash.GetTLLength(); // GAHash
            length += Protocol.GetLength(); // Protocol

            return length;
        }
    }
    public partial class RequestSaveCallDebug
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Debug.GetLength(); // Debug

            return length;
        }
    }
    public partial class RequestSaveCallLog
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += File.GetLength(); // File

            return length;
        }
    }
    public partial class RequestSaveDefaultGroupCallJoinAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += JoinAs.GetLength(); // JoinAs

            return length;
        }
    }
    public partial class RequestSaveDefaultSendAs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += SendAs.GetLength(); // SendAs

            return length;
        }
    }
    public partial class RequestSendConferenceCallBroadcast
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += Block.GetTLLength(); // Block

            return length;
        }
    }
    public partial class RequestSendGroupCallEncryptedMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += EncryptedMessage.GetTLLength(); // EncryptedMessage

            return length;
        }
    }
    public partial class RequestSendGroupCallMessage
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            length += 8; // RandomId
            length += Message.GetLength(); // Message
            if (flags.IsBitSet(0)) { length += 8; } // AllowPaidStars
            if (flags.IsBitSet(1)) { length += SendAs.GetLength(); } // SendAs

            return length;
        }
    }
    public partial class RequestSendSignalingData
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Data.GetTLLength(); // Data

            return length;
        }
    }
    public partial class RequestSetCallRating
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Rating
            length += Comment.GetTLLength(); // Comment

            return length;
        }
    }
    public partial class RequestStartScheduledGroupCall
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call

            return length;
        }
    }
    public partial class RequestToggleGroupCallRecord
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            if (flags.IsBitSet(1)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(2)) { length += 4; } // VideoPortrait

            return length;
        }
    }
    public partial class RequestToggleGroupCallSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Call.GetLength(); // Call
            if (flags.IsBitSet(0)) { length += 4; } // JoinMuted
            if (flags.IsBitSet(2)) { length += 4; } // MessagesEnabled
            if (flags.IsBitSet(3)) { length += 8; } // SendPaidMessagesStars

            return length;
        }
    }
    public partial class RequestToggleGroupCallStartSubscription
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Call.GetLength(); // Call
            length += 4; // Subscribed

            return length;
        }
    }
}
namespace MyTelegram.Schema.Photos
{
    public partial class RequestDeletePhotos
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetUserPhotos
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += UserId.GetLength(); // UserId
            length += 4; // Offset
            length += 8; // MaxId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestUpdateProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(1)) { length += Bot.GetLength(); } // Bot
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestUploadContactProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += UserId.GetLength(); // UserId
            if (flags.IsBitSet(0)) { length += File.GetLength(); } // File
            if (flags.IsBitSet(1)) { length += Video.GetLength(); } // Video
            if (flags.IsBitSet(2)) { length += 8; } // VideoStartTs
            if (flags.IsBitSet(5)) { length += VideoEmojiMarkup.GetLength(); } // VideoEmojiMarkup

            return length;
        }
    }
    public partial class RequestUploadProfilePhoto
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(5)) { length += Bot.GetLength(); } // Bot
            if (flags.IsBitSet(0)) { length += File.GetLength(); } // File
            if (flags.IsBitSet(1)) { length += Video.GetLength(); } // Video
            if (flags.IsBitSet(2)) { length += 8; } // VideoStartTs
            if (flags.IsBitSet(4)) { length += VideoEmojiMarkup.GetLength(); } // VideoEmojiMarkup

            return length;
        }
    }
}
namespace MyTelegram.Schema.Premium
{
    public partial class RequestApplyBoost
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Slots.GetLength(); } // Slots
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetBoostsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetBoostsStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetMyBoosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetUserBoosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += UserId.GetLength(); // UserId

            return length;
        }
    }
}
namespace MyTelegram.Schema.Smsjobs
{
    public partial class RequestFinishJob
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += JobId.GetTLLength(); // JobId
            if (flags.IsBitSet(0)) { length += Error.GetTLLength(); } // Error

            return length;
        }
    }
    public partial class RequestGetSmsJob
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += JobId.GetTLLength(); // JobId

            return length;
        }
    }
    public partial class RequestGetStatus
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestIsEligibleToJoin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestJoin
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestLeave
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestUpdateSettings
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stats
{
    public partial class RequestGetBroadcastStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestGetMegagroupStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel

            return length;
        }
    }
    public partial class RequestGetMessagePublicForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Channel.GetLength(); // Channel
            length += 4; // MsgId
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetMessageStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetPollStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // MsgId

            return length;
        }
    }
    public partial class RequestGetStoryPublicForwards
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStoryStats
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestLoadAsyncGraph
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Token.GetTLLength(); // Token
            if (flags.IsBitSet(0)) { length += 8; } // X

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stickers
{
    public partial class RequestAddStickerToSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += Sticker.GetLength(); // Sticker

            return length;
        }
    }
    public partial class RequestChangeSticker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Sticker.GetLength(); // Sticker
            if (flags.IsBitSet(0)) { length += Emoji.GetTLLength(); } // Emoji
            if (flags.IsBitSet(1)) { length += MaskCoords.GetLength(); } // MaskCoords
            if (flags.IsBitSet(2)) { length += Keywords.GetTLLength(); } // Keywords

            return length;
        }
    }
    public partial class RequestChangeStickerPosition
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Sticker.GetLength(); // Sticker
            length += 4; // Position

            return length;
        }
    }
    public partial class RequestCheckShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += ShortName.GetTLLength(); // ShortName

            return length;
        }
    }
    public partial class RequestCreateStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += UserId.GetLength(); // UserId
            length += Title.GetTLLength(); // Title
            length += ShortName.GetTLLength(); // ShortName
            if (flags.IsBitSet(2)) { length += Thumb.GetLength(); } // Thumb
            length += Stickers.GetLength(); // Stickers
            if (flags.IsBitSet(3)) { length += Software.GetTLLength(); } // Software

            return length;
        }
    }
    public partial class RequestDeleteStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset

            return length;
        }
    }
    public partial class RequestRemoveStickerFromSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Sticker.GetLength(); // Sticker

            return length;
        }
    }
    public partial class RequestRenameStickerSet
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Stickerset.GetLength(); // Stickerset
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
    public partial class RequestReplaceSticker
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Sticker.GetLength(); // Sticker
            length += NewSticker.GetLength(); // NewSticker

            return length;
        }
    }
    public partial class RequestSetStickerSetThumb
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Stickerset.GetLength(); // Stickerset
            if (flags.IsBitSet(0)) { length += Thumb.GetLength(); } // Thumb
            if (flags.IsBitSet(1)) { length += 8; } // ThumbDocumentId

            return length;
        }
    }
    public partial class RequestSuggestShortName
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Title.GetTLLength(); // Title

            return length;
        }
    }
}
namespace MyTelegram.Schema.Stories
{
    public partial class RequestActivateStealthMode
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags

            return length;
        }
    }
    public partial class RequestCanSendStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestCreateAlbum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Title.GetTLLength(); // Title
            length += Stories.GetLength(); // Stories

            return length;
        }
    }
    public partial class RequestDeleteAlbum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // AlbumId

            return length;
        }
    }
    public partial class RequestDeleteStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestEditStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Media.GetLength(); } // Media
            if (flags.IsBitSet(3)) { length += MediaAreas.GetLength(); } // MediaAreas
            if (flags.IsBitSet(1)) { length += Caption.GetTLLength(); } // Caption
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            if (flags.IsBitSet(2)) { length += PrivacyRules.GetLength(); } // PrivacyRules
            if (flags.IsBitSet(4)) { length += Music.GetLength(); } // Music

            return length;
        }
    }
    public partial class RequestExportStoryLink
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Id

            return length;
        }
    }
    public partial class RequestGetAlbums
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetAlbumStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // AlbumId
            length += 4; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetAllReadPeerStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetAllStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += State.GetTLLength(); } // State

            return length;
        }
    }
    public partial class RequestGetChatsToSend
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
    public partial class RequestGetPeerMaxIDs
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetPeerStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer

            return length;
        }
    }
    public partial class RequestGetPinnedStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStoriesArchive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // OffsetId
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStoriesByID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetStoriesViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetStoryReactionsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // Id
            if (flags.IsBitSet(0)) { length += Reaction.GetLength(); } // Reaction
            if (flags.IsBitSet(1)) { length += Offset.GetTLLength(); } // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetStoryViewsList
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(1)) { length += Q.GetTLLength(); } // Q
            length += 4; // Id
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestIncrementStoryViews
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestReadStories
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // MaxId

            return length;
        }
    }
    public partial class RequestReorderAlbums
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Order.GetLength(); // Order

            return length;
        }
    }
    public partial class RequestReport
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id
            length += Option.GetTLLength(); // Option
            length += Message.GetTLLength(); // Message

            return length;
        }
    }
    public partial class RequestSearchPosts
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            if (flags.IsBitSet(0)) { length += Hashtag.GetTLLength(); } // Hashtag
            if (flags.IsBitSet(1)) { length += Area.GetLength(); } // Area
            if (flags.IsBitSet(2)) { length += Peer.GetLength(); } // Peer
            length += Offset.GetTLLength(); // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestSendReaction
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // StoryId
            length += Reaction.GetLength(); // Reaction

            return length;
        }
    }
    public partial class RequestSendStory
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += Media.GetLength(); // Media
            if (flags.IsBitSet(5)) { length += MediaAreas.GetLength(); } // MediaAreas
            if (flags.IsBitSet(0)) { length += Caption.GetTLLength(); } // Caption
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            length += PrivacyRules.GetLength(); // PrivacyRules
            length += 8; // RandomId
            if (flags.IsBitSet(3)) { length += 4; } // Period
            if (flags.IsBitSet(6)) { length += FwdFromId.GetLength(); } // FwdFromId
            if (flags.IsBitSet(6)) { length += 4; } // FwdFromStory
            if (flags.IsBitSet(8)) { length += Albums.GetLength(); } // Albums
            if (flags.IsBitSet(9)) { length += Music.GetLength(); } // Music

            return length;
        }
    }
    public partial class RequestStartLive
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            if (flags.IsBitSet(0)) { length += Caption.GetTLLength(); } // Caption
            if (flags.IsBitSet(1)) { length += Entities.GetLength(); } // Entities
            length += PrivacyRules.GetLength(); // PrivacyRules
            length += 8; // RandomId
            if (flags.IsBitSet(6)) { length += 4; } // MessagesEnabled
            if (flags.IsBitSet(7)) { length += 8; } // SendPaidMessagesStars

            return length;
        }
    }
    public partial class RequestToggleAllStoriesHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 4; // Hidden

            return length;
        }
    }
    public partial class RequestTogglePeerStoriesHidden
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += 4; // Hidden

            return length;
        }
    }
    public partial class RequestTogglePinned
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id
            length += 4; // Pinned

            return length;
        }
    }
    public partial class RequestTogglePinnedToTop
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Peer.GetLength(); // Peer
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestUpdateAlbum
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Peer.GetLength(); // Peer
            length += 4; // AlbumId
            if (flags.IsBitSet(0)) { length += Title.GetTLLength(); } // Title
            if (flags.IsBitSet(1)) { length += DeleteStories.GetLength(); } // DeleteStories
            if (flags.IsBitSet(2)) { length += AddStories.GetLength(); } // AddStories
            if (flags.IsBitSet(3)) { length += Order.GetLength(); } // Order

            return length;
        }
    }
}

namespace MyTelegram.Schema.Updates
{
    public partial class RequestGetChannelDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Channel.GetLength(); // Channel
            length += Filter.GetLength(); // Filter
            length += 4; // Pts
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Pts
            if (flags.IsBitSet(1)) { length += 4; } // PtsLimit
            if (flags.IsBitSet(0)) { length += 4; } // PtsTotalLimit
            length += 4; // Date
            length += 4; // Qts
            if (flags.IsBitSet(2)) { length += 4; } // QtsLimit

            return length;
        }
    }
    public partial class RequestGetState
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;

            return length;
        }
    }
}
namespace MyTelegram.Schema.Updates.LayerN
{
    public partial class RequestGetDifference
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += 4; // Pts
            if (flags.IsBitSet(0)) { length += 4; } // PtsTotalLimit
            length += 4; // Date
            length += 4; // Qts

            return length;
        }
    }
}
namespace MyTelegram.Schema.Upload
{
    public partial class RequestGetCdnFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FileToken.GetTLLength(); // FileToken
            length += 8; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetCdnFileHashes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FileToken.GetTLLength(); // FileToken
            length += 8; // Offset

            return length;
        }
    }
    public partial class RequestGetFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            var flags = CalculateFlags();
            length += 4; // Flags
            length += Location.GetLength(); // Location
            length += 8; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestGetFileHashes
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Location.GetLength(); // Location
            length += 8; // Offset

            return length;
        }
    }
    public partial class RequestGetWebFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Location.GetLength(); // Location
            length += 4; // Offset
            length += 4; // Limit

            return length;
        }
    }
    public partial class RequestReuploadCdnFile
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += FileToken.GetTLLength(); // FileToken
            length += RequestToken.GetTLLength(); // RequestToken

            return length;
        }
    }
    public partial class RequestSaveBigFilePart
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // FileId
            length += 4; // FilePart
            length += 4; // FileTotalParts
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
    public partial class RequestSaveFilePart
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += 8; // FileId
            length += 4; // FilePart
            length += Bytes.GetTLLength(); // Bytes

            return length;
        }
    }
}
namespace MyTelegram.Schema.Users
{
    public partial class RequestGetFullUser
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetRequirementsToContact
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestGetSavedMusic
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += 4; // Offset
            length += 4; // Limit
            length += 8; // Hash

            return length;
        }
    }
    public partial class RequestGetSavedMusicByID
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += Documents.GetLength(); // Documents

            return length;
        }
    }
    public partial class RequestGetUsers
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id

            return length;
        }
    }
    public partial class RequestSetSecureValueErrors
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += Errors.GetLength(); // Errors

            return length;
        }
    }
    public partial class RequestSuggestBirthday
    {
        public int GetLength()
        {
            // ConstructorId length = 4 
            var length = 4;
            length += Id.GetLength(); // Id
            length += Birthday.GetLength(); // Birthday

            return length;
        }
    }
}
