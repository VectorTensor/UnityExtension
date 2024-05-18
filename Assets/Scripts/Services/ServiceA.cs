using UnityEngine;

namespace DefaultNamespace.Services
{
    public class ServiceA
    {
        public void Initalize(string message = null)
        {
            
            Debug.Log($"ServiceA.Initalize({message})");
            
        }
        
    }
    
    public class ServiceB
    {
        public void Initalize(string message = null)
        {
            
            Debug.Log($"ServiceB.Initalize({message})");
            
        }
        
    }

    public class FactoryA
    {
        private ServiceA cacheServiceA;

        public ServiceA CreateServiceA()
        {
            if (cacheServiceA == null)
            {
                cacheServiceA = new ServiceA();
            }

            return cacheServiceA;
        }
        
    }
}