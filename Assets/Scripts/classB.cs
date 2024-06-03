using DefaultNamespace.Services;
using DependencyInjection;
using UnityEngine;

namespace DefaultNamespace
{
    public class classB:MonoBehaviour
    {
        [Inject] private ServiceA _serviceA;
        [Inject] private ServiceB _serviceB;
        private FactoryA _factoryA;

        [Inject]
        public void Init(FactoryA factoryA)
        {
            _factoryA = factoryA;

        }

        void Start()
        {
            _serviceA.Initalize("Service A initialize from class B");
            _serviceB.Initalize("Service B initialize from class B");
            _factoryA.CreateServiceA().Initalize("Service A initialize from Factory A ");
        }

    }
}