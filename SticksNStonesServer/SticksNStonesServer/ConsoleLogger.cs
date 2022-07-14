using System;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace SticksNStonesServer;

public class ConsoleLogger : ILogger{
    public void Log(string message){
        Console.WriteLine(message);
    }
}