using System;
using StickNStonesShared.StickNStonesShared.Networking;

namespace StickNStonesShared.StickNStonesShared.Messages
{
    [Serializable]
    public class PositionUpdateMessage : MessageBase
    {
        public string playerName;
        public float x, y, z;

        public PositionUpdateMessage(string playerName, float x, float y, float z){
            this.playerName = playerName;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}