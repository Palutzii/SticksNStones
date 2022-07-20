using System;
using System.Collections.Generic;
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
                readonly Dictionary<Type, Delegate> _listeners = new();

                TcpClient Client{ get;}
                public string PlayerName{ get; set; }

                public Connection(ILogger logger, IJson json, TcpClient client) {
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
                                string? json = streamReader.ReadLine();
                                if (json == null){
                                        continue;
                                }
                                var holder = _json.Deserialize<MessageBase>(json);

                                if (holder == null){
                                        _logger.Log($"Invalid message received: {json}");
                                        continue;
                                }
                                
                                var type = AppDomain.CurrentDomain.GetAssemblies()
                                        .Select(assembly => assembly.GetType(holder.type))
                                        .SingleOrDefault(type => type != null);
                                if (type == null){
                                        _logger.Log($"Unsupported Message of Type {holder.type} received. Ignoring.");
                                        continue;
                                }
                                
                                var objectHolder = _json.Deserialize(json, type) as MessageBase;
                                if (_listeners.TryGetValue(type,out var listener)){
                                        listener.DynamicInvoke(objectHolder);
                                }
                        }
                }

                public void Subscribe<TMessage>(Action<TMessage> onMessageReceived) 
                        where TMessage : MessageBase
                {
                        if (_listeners.TryGetValue(typeof(TMessage),out var del)){
                                _listeners[typeof(TMessage)] = Delegate.Combine(del,onMessageReceived);
                        }
                        else{
                                
                                _listeners[typeof(TMessage)] = onMessageReceived;
                        }
                        
                }
                public void UnSubscribe<TMessage>(Action<TMessage> onMessageReceived) 
                        where TMessage : MessageBase
                {
                        if (_listeners.TryGetValue(typeof(TMessage),out var del)){
                                _listeners[typeof(TMessage)] = Delegate.Remove(del,onMessageReceived);
                        }
                        
                }
        }
}
