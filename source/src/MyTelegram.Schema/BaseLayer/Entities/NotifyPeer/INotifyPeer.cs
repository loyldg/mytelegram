// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines the set of users and/or groups that generate notifications.
/// See <a href="https://corefork.telegram.org/constructor/NotifyPeer" />
///</summary>
[JsonDerivedType(typeof(TNotifyPeer), nameof(TNotifyPeer))]
[JsonDerivedType(typeof(TNotifyUsers), nameof(TNotifyUsers))]
[JsonDerivedType(typeof(TNotifyChats), nameof(TNotifyChats))]
[JsonDerivedType(typeof(TNotifyBroadcasts), nameof(TNotifyBroadcasts))]
[JsonDerivedType(typeof(TNotifyForumTopic), nameof(TNotifyForumTopic))]
public interface INotifyPeer : IObject
{

}
