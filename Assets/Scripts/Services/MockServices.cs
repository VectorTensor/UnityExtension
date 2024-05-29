using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Services
{
    public class MockServices
    {
        
        public interface ILocalization
        {

            string GetLocalizedWord(string key);

        }

        public class MockLocalization : ILocalization
        {
            readonly List<string> words = new List<string>() { "hund", "katt", "fisk", "bil", "hus" };
            private readonly System.Random random = new System.Random();
            public string GetLocalizedWord(string key)
            {

                return words[random.Next()];


            }
        }
        
        public interface ISerializer
        {
            void Serialize();
            
        }
        
        public class MockSerializer: ISerializer
        {
            public void Serialize()
            {
                
                Debug.Log("MockSerializer.Serialize");
                
            }
        }
        
        public interface IAudioService
        {

            void Play();

        }

        public class MockAudioService : IAudioService
        {
            public void Play()
            {
                
                Debug.Log("MockAudioSerice.Play");
                
            }
        }

        public interface IGameService
        {

            void Start();

        }

        public class MockGameService : IGameService
        {
            public void Start()
            {
                Debug.Log("MockGameService.StartGame");
            }
        }
        
        public class MockMapService: IGameService 
        {
            public void Start()
            {
                Debug.Log("MockMapService.StartGame");
            }
        }
        
        
        
    }
}