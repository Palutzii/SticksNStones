

using System;
using StickNStonesShared.StickNStonesShared.Model;

namespace StickNStonesShared.StickNStonesShared.Messages{
    //Distributed from Server to clients to keep them synchronized.

    [Serializable]
    public class MatchInfoMessage{
        public MatchInfo matchInfo;
    }
}