namespace SticksNStonesServer.Interfaces;

public interface _iDatabase<T>{
    T Create(string id);
    T ReadOrCreate(string id);
    T Read(string id);
    void Update(string id, T data);
    void Delete(string id);
}