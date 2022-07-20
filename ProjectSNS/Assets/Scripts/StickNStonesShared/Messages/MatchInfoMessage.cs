using StickNStonesShared.StickNStonesShared.Model;

namespace StickNStonesShared.StickNStonesShared.Messages{
    //Distributed from Server to clients to keep them synchronized.

    [System.Serializable]
    public class MatchInfoMessage{
        public MatchInfo matchInfo;
    }
}