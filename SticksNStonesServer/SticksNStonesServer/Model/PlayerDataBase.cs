using System.Collections.Generic;
using StickNStonesShared.StickNStonesShared.Model;

namespace SticksNStonesServer.Model;

/// <summary>
///  Simple DataBase
///  It can store Entities (Data Objects that are Identifiable, that have an ID)
///  They are compared by ID (e.g. Player("Pavel", 200) == Player("Pavel", 300) because the id (the name) are the same)
///  Value Objects are compared by Value( e.g. Vector(2,2) == Vector(2,2), because all field are the same)
///
///  It can also retrieve Entities
///  It-Memory Database (therefore stored in RAM)
///  Therefore, this Database is non-persistent
/// </summary>
public class PlayerDataBase{
    readonly Dictionary<string, PlayerData> _playerDatas = new ();

    public PlayerData GetOrCreatePlayer(string playerName){
        if (!_playerDatas.TryGetValue(playerName, out var data)){
            data = new PlayerData{
                name = playerName,
                score = 0
            };
            _playerDatas[playerName] = data;
        }
        return data;
    }
}