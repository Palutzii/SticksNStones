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
/// Use <see cref="SendMessage{T}"/> to send a Message to this Player.
/// The class waits for incoming Messages on a seperate Thread.
/// </summary>
public class ClientConnection{
    readonly SticksNStonesMatch _match;
    readonly PlayerInfo _playerInfo;
    public Connection Connection{ get; }


    // see, what code can be shared between client and server
    // and  what code is individual to each other
    public ClientConnection(TcpClient client, SticksNStonesMatch match, PlayerInfo playerInfo){
        Connection = new Connection(new ConsoleLogger(), new DotNetJson(), client);
        _match = match;
        _playerInfo = playerInfo;
        Connection.Broker.Subscribe<LoginMessage>(OnMessageReceived);
    }

    void OnMessageReceived(LoginMessage loginMessage){
        Console.WriteLine($"[#{_match.Id}] Player '{loginMessage.playerName}' logged in.");
        Connection.PlayerName = loginMessage.playerName;
        _playerInfo.name = loginMessage.playerName; 
        _playerInfo.isReady = true;
        _match.DistributeMatchInfo();
    }
}