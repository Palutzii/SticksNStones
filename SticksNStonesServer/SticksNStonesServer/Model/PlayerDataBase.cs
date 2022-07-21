using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using StickNStonesShared.StickNStonesShared.Interfaces;
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
    readonly ILogger _logger;
    readonly IJson _json;
    readonly Dictionary<string, PlayerData> _playerDatas = new ();

    public PlayerDataBase(ILogger logger,IJson json){
        _logger = logger;
        _json = json;
        if (File.Exists("players.json")){
            
            var jsonText = File.ReadAllText("players.json");
            _playerDatas = json.Deserialize<Dictionary<string, PlayerData>>(jsonText);
            _logger.Log($"Found existing database with {_playerDatas.Count} player entries.");
        }
        else{
            _logger.Log("Found no existing database. Creating new one.");
            Save();
        }
    }

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

    public void UpdatePlayer(PlayerData playerData){
        _playerDatas[playerData.name] = playerData;
        File.WriteAllText($"players/{playerData.name}.json",_json.Serialize(playerData));
    }

    void Save(){
        var json = _json.Serialize(_playerDatas);
        File.WriteAllText("players.json", json);
    }
}