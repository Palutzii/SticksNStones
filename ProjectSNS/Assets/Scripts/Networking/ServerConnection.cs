﻿using System.Net;
using System.Net.Sockets;
using Adapter;
using StickNStonesShared.StickNStonesShared.Messages;
using StickNStonesShared.StickNStonesShared.Networking;
using UnityEngine;

namespace Networking{
    public class ServerConnection{
        static ServerConnection _instance;

        public Connection Connection;

        public static ServerConnection Instance{
            get{
                _instance ??= new ServerConnection();
                return _instance;
            }
        }
    
        public void Connect(string playerName){
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, 12244);
            this.Connection = new Connection(new UnityLogger(), new UnityJson(), client);
            this.Connection.MessageReceived += OnMessageReceived;
            this.Connection.PlayerName = playerName;
            this.Connection.SendMessage(new LoginMessage{
                playerName = playerName
            });
        }

        void OnMessageReceived(ObjectHolder holder){
            if (holder is ObjectHolder<MatchInfoMessage> matchInfoHolder){
                var matchInfo = matchInfoHolder.obj;
                Debug.Log(matchInfo);
            }
        }
    
    
    }
}