using StickNStonesShared.StickNStonesShared.Interfaces;
using UnityEngine;

namespace Adapter{
    public class UnityJson : IJson{
        public T Deserialize<T>(string json){
            return JsonUtility.FromJson<T>(json);
        }

        public string Serialize<T>(T data){
            return JsonUtility.ToJson(data);
        }
    }
}