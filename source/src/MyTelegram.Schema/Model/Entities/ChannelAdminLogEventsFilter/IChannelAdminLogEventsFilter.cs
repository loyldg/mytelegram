// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChannelAdminLogEventsFilter : IObject
{
    BitArray Flags { get; set; }
    bool Join { get; set; }
    bool Leave { get; set; }
    bool Invite { get; set; }
    bool Ban { get; set; }
    bool Unban { get; set; }
    bool Kick { get; set; }
    bool Unkick { get; set; }
    bool Promote { get; set; }
    bool Demote { get; set; }
    bool Info { get; set; }
    bool Settings { get; set; }
    bool Pinned { get; set; }
    bool Edit { get; set; }
    bool Delete { get; set; }
    bool GroupCall { get; set; }
    bool Invites { get; set; }
    bool Send { get; set; }
    bool Forums { get; set; }

}
