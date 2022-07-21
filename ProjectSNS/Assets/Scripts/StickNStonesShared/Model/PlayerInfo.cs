using SticksNStonesServer.Model;

namespace StickNStonesShared.StickNStonesShared.Model{
    /// <summary>
    /// Contains all important information for one PLayer of a Match.
    /// </summary>
    [System.Serializable]
    public class PlayerInfo{
        public bool isReady;
        public PlayerData data;
    }
}