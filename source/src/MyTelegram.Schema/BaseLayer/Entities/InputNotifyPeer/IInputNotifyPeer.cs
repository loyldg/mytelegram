// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines the set of users and/or groups that generate notifications.
/// See <a href="https://corefork.telegram.org/constructor/InputNotifyPeer" />
///</summary>
[JsonDerivedType(typeof(TInputNotifyPeer), nameof(TInputNotifyPeer))]
[JsonDerivedType(typeof(TInputNotifyUsers), nameof(TInputNotifyUsers))]
[JsonDerivedType(typeof(TInputNotifyChats), nameof(TInputNotifyChats))]
[JsonDerivedType(typeof(TInputNotifyBroadcasts), nameof(TInputNotifyBroadcasts))]
[JsonDerivedType(typeof(TInputNotifyForumTopic), nameof(TInputNotifyForumTopic))]
public interface IInputNotifyPeer : IObject
{

}
