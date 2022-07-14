using System.Text.Json;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace SticksNStonesServer;

public class DotNetJson : IJson{
    
    readonly JsonSerializerOptions options = new(){
        IncludeFields = true
    };
    
    public T Deserialize<T>(string json){
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public string Serialize<T>(T data){
        return JsonSerializer.Serialize(data, options);
    }
}