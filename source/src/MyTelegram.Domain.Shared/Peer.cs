// ReSharper disable once CheckNamespace

namespace MyTelegram;

public partial record Peer(PeerType PeerType, long PeerId, long AccessHash = 0);