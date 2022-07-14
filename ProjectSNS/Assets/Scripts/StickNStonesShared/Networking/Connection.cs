using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using StickNStonesShared.StickNStonesShared.Interfaces;
using StickNStonesShared.StickNStonesShared.Messages;

namespace StickNStonesShared.StickNStonesShared.Networking{

        public class ObjectHolder{
                public string type;

                public ObjectHolder(string type){
                        this.type = type;
                }
        }

        public class ObjectHolder<T> : ObjectHolder{
                public T obj;

                public ObjectHolder(T obj) : base(typeof(ObjectHolder<T>).FullName){
                        this.obj = obj;
                }
        }
        public class Connection{
                public event Action<ObjectHolder> MessageReceived;
                
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
                        _streamWriter.WriteLine(_json.Serialize(new ObjectHolder<T>(message)));
                        _streamWriter.Flush();
                }
        
                void ReceiveMessages(){
        
                        var streamReader = new StreamReader(Client.GetStream());
                        while (true){
                                //block the reading thread until a whole line of information has arrived.
                                string? json = streamReader.ReadLine();
                                if (json == null){
                                        continue;
                                }
                                var holder = _json.Deserialize<ObjectHolder>(json);
                                var type = AppDomain.CurrentDomain.GetAssemblies()
                                        .Select(assembly => assembly.GetType(holder.type))
                                        .Single(type => type != null);
                                var objectHolder = _json.Deserialize(json, type) as ObjectHolder;
                                MessageReceived?.Invoke(objectHolder);
                        }
                }
        }
}
