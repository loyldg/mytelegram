namespace MyTelegram.Core;

public class ObjectIdConsts
{
    public static readonly Dictionary<uint, string> NotNeedAckObjectIdToNames = new()
    {
        { 0xf3427b8c, "PingDelayDisconnectHandler" },
        { 0x7abe77ec, "PingHandler" },
        { 0x73f1f8dc, "MsgContainerHandler" },
        { 0xcdd42a05, "BindTempAuthKeyHandler" },
    };
}
