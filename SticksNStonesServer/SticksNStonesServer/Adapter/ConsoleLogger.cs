using System;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace SticksNStonesServer.Adapter;

public class ConsoleLogger : ILogger{
    public void Log(string message){
        Console.WriteLine(message);
    }
}