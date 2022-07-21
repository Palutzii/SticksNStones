﻿using System;
using System.Net.Sockets;
using StickNStonesShared.StickNStonesShared.Messages;
using StickNStonesShared.StickNStonesShared.Model;
using StickNStonesShared.StickNStonesShared.Networking;
using SticksNStonesServer.Adapter;
using SticksNStonesServer.Model;

namespace SticksNStonesServer.Networking;

/// <summary>
/// Contains all code for one player's connection
/// The class waits for incoming Messages on a separate Thread.
/// </summary>
public class ClientConnection{
    readonly SticksNStonesMatch _match;
    readonly PlayerInfo _playerInfo;
    readonly PlayerDataBase _playerDataBase;
    public Connection Connection{ get; }
    
    
    public ClientConnection(TcpClient client, SticksNStonesMatch match, PlayerInfo playerInfo, PlayerDataBase playerDataBase){
        Connection = new Connection(new ConsoleLogger(), new DotNetJson(), client);
        _match = match;
        _playerInfo = playerInfo;
        _playerDataBase = playerDataBase;
        Connection.Broker.Subscribe<LoginMessage>(OnLoginReceived);
        Connection.Broker.Subscribe<GainCoinMessage>(OnGainScoreReceived);
    }

    void OnGainScoreReceived(GainCoinMessage gainScore){
        _playerInfo.data.score++;
        _match.DistributeMatchInfo();
    }

    void OnLoginReceived(LoginMessage loginMessage){
        Console.WriteLine($"[#{_match.Id}] Player '{loginMessage.playerName}' logged in.");
        Connection.PlayerName = loginMessage.playerName;
        _playerInfo.data = _playerDataBase.GetOrCreatePlayer(loginMessage.playerName);
        _playerInfo.data.name = loginMessage.playerName; 
        _playerInfo.isReady = true;
        _match.DistributeMatchInfo();
    }
}