using System;
using System.Text.Json;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace SticksNStonesServer.Adapter;

public class DotNetJson : IJson{
    
    readonly JsonSerializerOptions options = new(){
        IncludeFields = true
    };
    
    public T Deserialize<T>(string json){
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public object Deserialize(string json, Type type){
        return JsonSerializer.Deserialize(json, type, options);
    }

    public string Serialize<T>(T data){
        return JsonSerializer.Serialize(data, options);
    }
}