using DefaultNamespace.Services;
using DependencyInjection;
using UnityEngine;

namespace DefaultNamespace
{
    public class classA:MonoBehaviour
    {
        private ServiceA _serviceA;

        [Inject]
        public void Init(ServiceA s)
        {
            _serviceA = s;
        }
        
    }
}