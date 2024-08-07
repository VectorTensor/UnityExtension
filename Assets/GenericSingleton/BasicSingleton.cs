﻿using UnityEngine;

namespace GenericSingleton
{
    public class BasicSingleton<T>:MonoBehaviour where T : Component
    {

        protected static T instance;
        public static bool HasInstance => instance != null;

        public static T TryGetInstance() => HasInstance ? instance : null;

        public static T Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {

                        var go = new GameObject(typeof(T).Name + " [AutoGenerated]");
                        instance = go.AddComponent<T>();

                    }
                    

                }

                return instance;
            }
            
        }
        protected virtual void Awake()
        {
            InitializeSingleton();
            
        }

        protected virtual void InitializeSingleton()
        {

            if (!Application.isPlaying) return;
            instance = this as T;

        }




    }
}