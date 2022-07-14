using System;

namespace StickNStonesShared.StickNStonesShared.Messages{
    
    [Serializable]
    public class LoginMessage{
        public string playerName;
        public string id;
        public int score;
    }
}