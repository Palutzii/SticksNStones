using System;
using System.Collections.Generic;

namespace StickNStonesShared.StickNStonesShared.Networking{
    public class Broker{
                
        readonly Dictionary<Type, Delegate> _listeners = new();

        public event Action<MessageBase> AnyMessageReceived;
                
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

        public void InvokeSubscribers(Type type, MessageBase data){
            if (_listeners.TryGetValue(type,out var listener)){
                listener.DynamicInvoke(data);
            }
            this.AnyMessageReceived?.Invoke(data);
        }
    }
}