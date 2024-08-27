using System;
using System.Collections.Generic;

namespace EventManager
{
    public class EventBus
    {
        
        private static Dictionary<string, Action> events = new Dictionary<string, Action>();
        
    }
}