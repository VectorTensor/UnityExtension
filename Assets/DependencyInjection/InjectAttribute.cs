using System;
using UnityEngine;

namespace DependencyInjection
{
    [AttributeUsage(AttributeTargets.Field| AttributeTargets.Method) ]
    public sealed class InjectAttribute : Attribute 
    {
        public InjectAttribute(){}
    }
    
    
}