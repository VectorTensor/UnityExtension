using System;

namespace EventManager
{
    public interface IEvent
    {
    }

    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
        
        public void Add(Action action);
        public void Add(Action<T> action);
        public void Remove(Action<T> action);
        public void Remove(Action action);
    }

    public class EventBinding<T> : IEventBinding<T> 
    {
        Action<T> onEvent;
        Action onEventNoArgs;
        Action<T> IEventBinding<T>.OnEvent { get=> onEvent; set=> onEvent = value; }
        Action IEventBinding<T>.OnEventNoArgs { get=> onEventNoArgs; set=> onEventNoArgs = value; }

        public void Add(Action action)
        {
            onEventNoArgs += action;
        }
        public void Add(Action<T> action)
        {
            onEvent += action;
        }

        public void Remove(Action<T> action)
        {
            onEvent += action;
        }

        public void Remove(Action action)
        {
            onEventNoArgs += action;
        }
        
        
        
        
        
    }
}