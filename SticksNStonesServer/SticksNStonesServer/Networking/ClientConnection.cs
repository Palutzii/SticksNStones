using System;
using System.Net.Sockets;
using StickNStonesShared.StickNStonesShared.Messages;
using StickNStonesShared.StickNStonesShared.Model;
using StickNStonesShared.StickNStonesShared.Networking;
using SticksNStonesServer.Adapter;
using SticksNStonesServer.Interfaces;
using SticksNStonesServer.Model;

namespace SticksNStonesServer.Networking;

/// <summary>
/// Contains all code for one player's connection
/// The class waits for incoming Messages on a separate Thread.
/// </summary>
public class ClientConnection{
    readonly SticksNStonesMatch _match;
    readonly PlayerInfo _playerInfo;
    readonly _iDatabase<PlayerData> _playerDataBase;
    public Connection Connection{ get; }
    
    
    public ClientConnection(TcpClient client, SticksNStonesMatch match, PlayerInfo playerInfo, _iDatabase<PlayerData> playerDataBase){
        
        Connection = new Connection(new ConsoleLogger(), new DotNetJson(), client);
        _match = match;
        _playerInfo = playerInfo;
        _playerDataBase = playerDataBase;
        Connection.Broker.Subscribe<LoginMessage>(OnLoginReceived);
        Connection.Broker.Subscribe<GainCoinMessage>(OnGainScoreReceived);
        Connection.Broker.Subscribe<MatchInfoMessage>(OnMatchInfoMessageReceived);
        Connection.Broker.Subscribe<PositionUpdateMessage>(OnPositionUpdateMessageReceived);
    }

    void OnPositionUpdateMessageReceived(PositionUpdateMessage obj){
        _playerInfo.data.positionX = obj.x;
        _playerInfo.data.positionY = obj.y;
        _playerInfo.data.positionZ = obj.z;
        // TODO: Switch to vector 3
        _match.DistributeMatchInfo();
    }

    void OnMatchInfoMessageReceived(MatchInfoMessage obj){
        // Need matchInfoMessage from client to update server position value
    }

    void OnGainScoreReceived(GainCoinMessage gainScore){
        _playerInfo.data.score++;
        _playerDataBase.Update(_playerInfo.data.name, _playerInfo.data);
        _match.DistributeMatchInfo();
    }

    void OnLoginReceived(LoginMessage loginMessage){
        Console.WriteLine($"[#{_match.Id}] Player '{loginMessage.playerName}' logged in.");
        Connection.PlayerName = loginMessage.playerName;
        _playerInfo.data = _playerDataBase.ReadOrCreate(loginMessage.playerName);
        _playerInfo.data.name = loginMessage.playerName; 
        _playerInfo.isReady = true;
        _match.DistributeMatchInfo();
    }
    
    
}