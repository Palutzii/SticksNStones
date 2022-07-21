using System.Collections.Generic;
using StickNStonesShared.StickNStonesShared.Model;

namespace SticksNStonesServer.Model;

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