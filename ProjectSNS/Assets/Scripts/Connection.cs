using System.IO;
using System.Net.Sockets;
using Messages;
using UnityEngine;

public class Connection{
        static Connection _instance;
        StreamReader _streamReader;
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
                this._streamReader = new StreamReader(client.GetStream());
                SendMessage(new LoginMessage{
                        playerName = playerName
                });
        }

        public TcpClient Client{ get; private set; }
        public string PlayerName{ get; private set; }

        public void SendMessage<T>(T message){
                _streamWriter.WriteLine(JsonUtility.ToJson(message));
                _streamWriter.Flush();
        }
}
