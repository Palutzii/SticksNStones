using System;

namespace StickNStonesShared.StickNStonesShared.Model
{
    /// <summary>
    ///     Contains all important information for one Match.
    /// </summary>
    [Serializable]
    public class MatchInfo
    {
        public bool isStarted;
        public PlayerInfo player1 = new();
        public PlayerInfo player2 = new();

        //array of bullets,
    }
}