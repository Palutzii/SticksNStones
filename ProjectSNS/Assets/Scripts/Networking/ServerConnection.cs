using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Adapter;
using StickNStonesShared.StickNStonesShared.Messages;
using StickNStonesShared.StickNStonesShared.Networking;
using UnityEngine;

namespace Networking{
    public class ServerConnection : MonoBehaviour{
        static ServerConnection _instance;

        Connection _connection;

        public Broker Broker{ get; } = new();

        Queue<MessageBase> messageQueue = new();

        public static ServerConnection Instance{
            get{
                _instance ??= CreatInstance();
                return _instance;
            }
        }

        static ServerConnection CreatInstance(){
            var go = new GameObject("ServerConnection");
            DontDestroyOnLoad(go);
            var connection = go.AddComponent<ServerConnection>();
            return connection;
        }
    
        public void Connect(string playerName){
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, 12244);
            this._connection = new Connection(new UnityLogger(), new UnityJson(), client);
            this._connection.Broker.AnyMessageReceived += OnAnyMessageReceived;
            this._connection.PlayerName = playerName;
            this._connection.SendMessage(new LoginMessage{
                playerName = playerName
            });
        }

        void OnAnyMessageReceived(MessageBase message){
            messageQueue.Enqueue(message);
        }

        void Update(){
            while (messageQueue.Count > 0){
                var message = messageQueue.Dequeue();
                this.Broker.InvokeSubscribers(message.GetType(), message);
            }
        }
    }
}