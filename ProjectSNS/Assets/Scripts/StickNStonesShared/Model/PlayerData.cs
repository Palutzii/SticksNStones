using System;

namespace SticksNStonesServer.Model
{
    [Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
        public float positionX;
        public float positionY;
        public float positionZ;
        public bool isBulletShot;
        public float bulletPositionX;
        public float bulletPositionY;
        public float bulletPositionZ;
    }
}