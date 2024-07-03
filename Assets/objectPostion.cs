using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPostion : MonoBehaviour
{
    // Start is called before the first frame update
    public Material material;
    void Start()
    {
        Vector3 objectPosition = transform.position;
        material.SetVector("_ObjectPosition", objectPosition);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
