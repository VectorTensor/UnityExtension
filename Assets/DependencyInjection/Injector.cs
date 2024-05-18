using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using System.Linq;

namespace DependencyInjection
{
    public class Injector : MonoBehaviour
    {
        private const BindingFlags k_bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();

        protected void Awake()
        {
            var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
                
            }
            

        }

        void RegisterProvider(IDependencyProvider provider)
        {

            var methods = provider.GetType().GetMethods(k_bindingFlags); // Gets all the methods satisfying the binding flags 

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;// We take only the methods having the provide attribute
                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, null);
                if (providedInstance != null)
                {
                    registry.Add(returnType, providedInstance);
                    Debug.Log($"Registered {returnType.Name} from {provider.GetType().Name}");
                    
                }
                else
                {

                    throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");

                }

            }

        }

        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);

        }
    }
    
    public interface IDependencyProvider{}
}