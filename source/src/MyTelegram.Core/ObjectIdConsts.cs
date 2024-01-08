namespace MyTelegram.Core;

public partial class ObjectIdConsts
{
    public const uint BindTempAuthKey = 0xcdd42a05;
    public const uint GetFileObjectId = 0xb15a9afc;
    public const uint GetFileObjectIdLayer143 = 0xbe5335be;
    public const uint SaveFilePartObjectId = 0xb304a621;
    public const uint GzipPackedId = 0x3072cfa1;
    public const uint InvokeWithLayer = 0xda9b0d0d;
    public const uint MsgAcks = 0x62d6b459;
    public const uint MsgContainer = 0x73f1f8dc;
    public const uint PingDelayId = 0xf3427b8c;
    public const uint PingId = 0x7abe77ec;
    public static readonly Dictionary<uint, string> NotNeedAckObjectIdToNames = new()
    {
        { 0xf3427b8c, "PingDelayDisconnectHandler" },
        { 0x7abe77ec, "PingHandler" },
        { 0x73f1f8dc, "MsgContainerHandler" },
        { 0xcdd42a05, "BindTempAuthKeyHandler" },
        {0xf2f2330a,"GetLangPackHandler"},
        {0xcd984aa5,"Langpack.GetDifferenceHandler"},
        {0x6a596502,"GetLanguageHandler"},
        {0x42c6978f,"GetLanguagesHandler"},
        {0xefea3803,"GetStringsHandler"},
    };
}
