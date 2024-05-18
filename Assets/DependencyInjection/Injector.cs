using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Linq;
using Debug = UnityEngine.Debug;

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

            // Search all Monobehaviour where you have field injection possible
            var injectables = FindMonoBehaviours().Where(IsInjectable);
            
            foreach (var injectable in injectables)
            {
                // Inject dependency to the monobehaviour
                Inject(injectable);
                
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

        void Inject(object instance)
        {
            var type = instance.GetType();
            // Get all the injectable fields
            var injectableFields = type.GetFields(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                var fieldType = injectableField.FieldType;
                // get the instance of the dependency
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                {
                    // If some problem occured while trying to resolve dependency

                    throw new Exception($"Failed to inject {fieldType.Name} into {type.Name}");

                }
                // We set the field to the dependency
                injectableField.SetValue(instance, resolvedInstance);
                Debug.Log($"Field Inject {fieldType.Name} into {type.Name}");
            }

            var injectableMethods = type.GetMethods(k_bindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {

                var requiredParameters = injectableMethod.GetParameters().Select(parameter => parameter.ParameterType)
                    .ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new Exception($"Failed to inject {type.Name}.{injectableMethod.Name}");

                }

                injectableMethod.Invoke(instance, resolvedInstances);
                Debug.Log($"Method Injected {type.Name}.{injectableMethod.Name}");


            }
            
            

        }

        bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        }

        object Resolve(Type type)
        {
            // get instance from the registry
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;


        }

        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);

        }
    }
    
    public interface IDependencyProvider{}
}