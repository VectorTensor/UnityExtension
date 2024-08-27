using System;
using EventManager;
using UnityEngine;

namespace DefaultNamespace.EventTest
{
    public class ObjectB : MonoBehaviour
    {
        private void Start()
        {
            
            CustomEventBus.SubscribeEvent(ObjectA.eventName,eventHndler);
        }

        private void eventHndler(IEvent e)
        {
            Debug.Log(((TestEvent )e).message);
        }

        private void OnDestroy()
        {
            
            CustomEventBus.UnsubscribeEvent(ObjectA.eventName,eventHndler);
            
        }
    }
}