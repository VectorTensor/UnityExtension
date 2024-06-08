using DefaultNamespace.Services;
using DependencyInjection;
using GenericSingleton;
using UnityEngine;

namespace DefaultNamespace
{
    public class classA:PersistentSingleton<classA>
    {
        private ServiceA _serviceA;

        [Inject]
        public void Init(ServiceA s)
        {
            _serviceA = s;
        }
        
    }
}