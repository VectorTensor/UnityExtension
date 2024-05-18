using DefaultNamespace.Services;
using UnityEngine;
using DependencyInjection;

namespace DefaultNamespace
{

    public class classA
    {
        private ServiceA _serviceA;

        [Inject]
        public void Init(ServiceA s)
        {
            _serviceA = s;
        }
        
    }
    
    public class classB
    {
        [Inject] private ServiceA _serviceA;
        [Inject] private ServiceB _serviceB;
        private FactoryA _factoryA;

        [Inject]
        public void Init(FactoryA factoryA)
        {
            _factoryA = factoryA;

        }

    }
}