using System;
using StickNStonesShared.StickNStonesShared.Networking;

namespace StickNStonesShared.StickNStonesShared.Messages{
    
    [Serializable]
    public class LoginMessage : MessageBase{
        public string playerName;
    }
}