using UnityEngine;
using ILogger = StickNStonesShared.StickNStonesShared.Interfaces.ILogger;

namespace Adapter{
    public class UnityLogger : ILogger {
        public void Log(string message){
            Debug.Log(message);
        }
    }
}