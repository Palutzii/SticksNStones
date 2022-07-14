using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace StickNStonesShared.StickNStonesShared.Networking{
        public class Connection{
                public event Action<string> MessageReceived;
                
                readonly ILogger _logger;
                readonly IJson _json; 
                readonly StreamWriter _streamWriter;
                TcpClient Client{ get;}
                public string PlayerName{ get; set; }

                public Connection(ILogger logger, IJson json, TcpClient client) {
                        _logger = logger;
                        _json = json;
                        Client = client;
                        this._streamWriter = new StreamWriter(client.GetStream());
                        new Thread(ReceiveMessages).Start();
                }
                public void SendMessage<T>(T message){
                        _streamWriter.WriteLine(_json.Serialize(message));
                        _streamWriter.Flush();
                }
        
                void ReceiveMessages(){
        
                        var streamReader = new StreamReader(Client.GetStream());
                        while (true){
                                //block the reading thread until a whole line of information has arrived.
                                string? json = streamReader.ReadLine();
                                if (json != null){
                                        MessageReceived?.Invoke(json);
                                }
                        }
                }
        }
}
