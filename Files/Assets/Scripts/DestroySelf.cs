using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float LifeTime = 5f;
    public bool onStart = true;
    void Start()
    {
        if(onStart){
            Destroy(gameObject, LifeTime);
        }
    }
    public void Destroy(float LifeTime)
    {
        Destroy(gameObject, LifeTime);
    }
}
