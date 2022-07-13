namespace StickNStonesShared.StickNStonesShared.Interfaces{
    public interface IJson{
        T Deserialize<T>(string json);
        string Serialize<T>(T data);
    }
}