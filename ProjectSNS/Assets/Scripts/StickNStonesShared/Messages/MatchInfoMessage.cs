using StickNStonesShared.StickNStonesShared.Model;
using StickNStonesShared.StickNStonesShared.Networking;

namespace StickNStonesShared.StickNStonesShared.Messages{
    //Distributed from Server to clients to keep them synchronized.

    [System.Serializable]
    public class MatchInfoMessage : MessageBase{
        public MatchInfo matchInfo;
    }
}