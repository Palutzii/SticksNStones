using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using StickNStonesShared.StickNStonesShared.Interfaces;
using StickNStonesShared.StickNStonesShared.Messages;

namespace StickNStonesShared.StickNStonesShared.Networking{
        public class Connection{
                readonly ILogger _logger;
                readonly IJson _json;

                public event Action<MatchInfoMessage> matchInfoMessageReceived;
                static Connection _instance;
                StreamWriter _streamWriter;

                public Connection(ILogger logger, IJson json){
                        _logger = logger;
                        _json = json;
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
                        _streamWriter.WriteLine(_json.Serialize(message));
                        _streamWriter.Flush();
                }
        
                void ReadPlayer(){
        
                        var streamReader = new StreamReader(Client.GetStream());
                        while (true){
                                //block the reading thread until a whole line of information has arrived.
                                string? json = streamReader.ReadLine();
                                var matchInfo = _json.Deserialize<MatchInfoMessage>(json);
                                matchInfoMessageReceived?.Invoke(matchInfo);
                                _logger.Log(json);
                        }
                }
        }
}
