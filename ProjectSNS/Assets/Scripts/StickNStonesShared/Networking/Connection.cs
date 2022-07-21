using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using StickNStonesShared.StickNStonesShared.Interfaces;

namespace StickNStonesShared.StickNStonesShared.Networking{

        public class MessageBase{
                public string type;

                public MessageBase(){
                        this.type = GetType().FullName;
                }
        }


        public class Connection{
                
                readonly ILogger _logger;
                readonly IJson _json; 
                readonly StreamWriter _streamWriter;

                TcpClient Client{ get;}
                public string PlayerName{ get; set; }
                public Broker Broker{ get; }

                public Connection(ILogger logger, IJson json, TcpClient client){
                        Broker = new Broker();
                        _logger = logger;
                        _json = json;
                        Client = client;
                        this._streamWriter = new StreamWriter(client.GetStream());
                        new Thread(ReceiveMessages).Start();
                }
                public void SendMessage<TMessage>(TMessage message)
                where TMessage : MessageBase
                {
                        _streamWriter.WriteLine(_json.Serialize(message));
                        _streamWriter.Flush();
                }
        
                void ReceiveMessages(){
        
                        var streamReader = new StreamReader(Client.GetStream());
                        while (true){
                                //block the reading thread until a whole line of information has arrived.
                                try{
                                        string? json = streamReader.ReadLine();
                                        if (json == null){
                                                continue;
                                        }

                                        var holder = _json.Deserialize<MessageBase>(json);

                                        if (holder == null){
                                                _logger.Log($"[{Client.Client.RemoteEndPoint}] Invalid message received: {json}");
                                                continue;
                                        }

                                        var type = AppDomain.CurrentDomain.GetAssemblies()
                                                .Select(assembly => assembly.GetType(holder.type))
                                                .SingleOrDefault(type => type != null);
                                        if (type == null){
                                                _logger.Log(
                                                        $"[{Client.Client.RemoteEndPoint}] Unsupported Message of Type {holder.type} received. Ignoring.");
                                                continue;
                                        }

                                        var message = _json.Deserialize(json, type) as MessageBase;
                                        Broker.InvokeSubscribers(type, message);
                                }
                                catch (IOException e){
                                        _logger.Log($"[{Client.Client.RemoteEndPoint}] {e}");
                                        // player disconnected
                                        // flag them as disconnected
                                        // after a while : win per default for the other player
                                }
                                
                                
                        }
                }

                
        }
}
