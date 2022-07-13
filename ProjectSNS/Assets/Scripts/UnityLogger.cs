using UnityEngine;
using ILogger = StickNStonesShared.StickNStonesShared.Interfaces.ILogger;

public class UnityLogger : ILogger {
    public void Log(string message){
        Debug.Log(message);
    }
}