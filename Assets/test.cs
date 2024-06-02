using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Services;
using UnityEngine;

public class test : MonoBehaviour
{
    private MockServices.IAudioService audio;

    private void Awake()
    {
        
        ServiceLocator.ServiceLocator.Global.Register<MockServices.IAudioService>(new MockServices.MockAudioService());
    }

    // Start is called before the first frame update
    void Start()
    {

        ServiceLocator.ServiceLocator.Global.TryGetService(out audio);
        audio.Play();

    }

 
}
