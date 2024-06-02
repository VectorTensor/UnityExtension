using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator
{
    public class ServiceLocator
    {

        static ServiceLocator global;
        static Dictionary<Scene,ServiceLocator> sceneContainer;

        public readonly ServiceManager services = new ServiceManager();


        private const string GLOBAL_NAME = "Service Locator (global)";
        private const string SCENE_NAME = "Service Locator (scene";

        public static ServiceLocator Global
        {
            get
            {
                if (global != null)
                {
                    return global;
                }
                else
                {
                    
                    global = new ServiceLocator();
                    return global ;

                }
            }
        }

        public static ServiceLocator ForScene(MonoBehaviour mb)
        {
            Scene scene = mb.gameObject.scene;
            if (sceneContainer.TryGetValue(scene, out ServiceLocator container) )
            {
                return container;
            }

            sceneContainer[scene] = new ServiceLocator();

            return sceneContainer[scene];


        }

        public static void  RemoveSceneLocator(Scene scene)
        {

            if (sceneContainer.ContainsKey(scene))
            {

                sceneContainer.Remove(scene);

            }
            else
            {
                Debug.Log("No locator for this scene ");
            }
            

        }

        public ServiceLocator Register<T>(T service)
        {

            services.Register(service);
            return this;

        }

        public ServiceLocator Register(Type type, object service)
        {
            services.Register(type, service);
            return this;

        }

        public bool TryGetService<T>(out T service) where T : class
        {

            return services.TryGet(out service);

        }


        public T Get<T>() where T : class
        {

            return services.Get<T>();

        }
        
    }
}