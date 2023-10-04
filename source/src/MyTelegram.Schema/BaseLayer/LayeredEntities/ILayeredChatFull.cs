// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredChatFull : MyTelegram.Schema.IChatFull
{
    MyTelegram.Schema.IChatParticipants Participants { get; set; }
    ///<summary>
    ///See <a href="https://core.telegram.org/type/Photo" />
    ///</summary>
    public MyTelegram.Schema.IPhoto? ChatPhoto { get; set; }

    ///<summary>
    ///See <a href="https://core.telegram.org/type/ExportedChatInvite" />
    ///</summary>
    public MyTelegram.Schema.IExportedChatInvite? ExportedInvite { get; set; }
}