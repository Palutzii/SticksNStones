using StickNStonesShared.StickNStonesShared.Networking;

public class ConnectionSingleton{
    static ConnectionSingleton _instance;

    public Connection Connection = new Connection(new UnityLogger(), new UnityJson());

    public static ConnectionSingleton Instance{
        get{
            _instance ??= new ConnectionSingleton();
            return _instance;
        }
    }
    
    
}