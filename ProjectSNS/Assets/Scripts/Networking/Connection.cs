using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Messages;
using UnityEngine;

public class Connection{

        public event Action<MatchInfoMessage> matchInfoMessageReceived;
        static Connection _instance;
        StreamWriter _streamWriter;

        public static Connection Instance{
                get{
                        _instance ??= new Connection();
                        return _instance;
                }
        }

        public void Init(TcpClient client, string playerName){
                this.Client = client;
                this.PlayerName = playerName;
                this._streamWriter = new StreamWriter(client.GetStream());
                new StreamReader(client.GetStream());
                new Thread(ReadPlayer).Start();
                SendMessage(new LoginMessage{
                        playerName = playerName
                });
        }

        TcpClient Client{ get; set; }
        string PlayerName{ get; set; }

        public void SendMessage<T>(T message){
                _streamWriter.WriteLine(JsonUtility.ToJson(message));
                _streamWriter.Flush();
        }
        
        void ReadPlayer(){
        
                var streamReader = new StreamReader(Client.GetStream());
                while (true){
                        //block the reading thread until a whole line of information has arrived.
                        string? json = streamReader.ReadLine(); 
                        var matchInfo = JsonUtility.FromJson<MatchInfoMessage>(json);
                        matchInfoMessageReceived?.Invoke(matchInfo);
                        Debug.Log(json);
                }
        }
}
