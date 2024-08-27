using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace EventManager
{
    public static class EventBus
    {
        
        private static Dictionary<string, IEventBinding<IEvent>> _eventsStore= new Dictionary<string, IEventBinding<IEvent>>();

        public static void CreateEvent(string key, EventBinding<IEvent> eventBinding)
        {
            
            if (!_eventsStore.ContainsKey(key))
            {
                _eventsStore.Add(key, eventBinding);
                Debug.Log($"Successfully created event {key}");
            }
        }


        public static void SubscribeEvent(string key, Action<IEvent> action, Action onEventNotFound = null)
        {
            
            if (_eventsStore.TryGetValue(key, out IEventBinding<IEvent> eventBinding))
            {
                eventBinding.Add(action);
                Debug.Log($"Successfully subscribed to event {key}");
                return;
            }
            onEventNotFound?.Invoke();
            
            
            
            
            
        }
        
        public static void SubsribeEventNoArgs(string key, Action action,Action onEventNotFound = null)
        {
            
            if (_eventsStore.TryGetValue(key, out IEventBinding<IEvent> eventBinding))
            {
                eventBinding.Add(action);
                Debug.Log($"Successfully subscribed to event {key}");
                return;
            }
            
            onEventNotFound?.Invoke();
            
            
        }
        
        public static void UnsubscribeEvent(string key, Action<IEvent> action, Action onEventNotFound = null)
        {
            
            if (_eventsStore.TryGetValue(key, out IEventBinding<IEvent> eventBinding))
            {
                eventBinding.Remove(action);
                Debug.Log($"Successfully unsubscribed from event {key}");
                return;
            }
            onEventNotFound?.Invoke();
            
            
        }
        public static void UnsubscribeEventNoArgs(string key, Action action, Action onEventNotFound)
        {
            
            if (_eventsStore.TryGetValue(key, out IEventBinding<IEvent> eventBinding))
            {
                eventBinding.Remove(action);
                Debug.Log($"Successfully unsubscribed from event {key}");
                return;
            }
            
            onEventNotFound?.Invoke();
            
        }
        
    }
}