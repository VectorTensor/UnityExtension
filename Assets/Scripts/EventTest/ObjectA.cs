using System;
using EventManager;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.EventTest
{
    public struct TestEvent : IEvent
    {
        public string message;
    }
    public class ObjectA : MonoBehaviour
    {
        public const string eventName = "TestEvent";
        public Button btn;
        private void Awake()
        {
            IEvent x = new TestEvent();
            
            CustomEventBus.CreateEvent(eventName, new EventBinding<IEvent>());
            btn.onClick.AddListener(btnListener);

            
        }
        private void btnListener()
        {
            CustomEventBus.InvokeEvent(eventName, new TestEvent(){message = $"Hello World from {gameObject.name}"});
        }

        private void Start()
        {
            
            
        }
    }
}