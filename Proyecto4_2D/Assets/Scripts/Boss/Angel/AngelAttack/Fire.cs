using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float timeLife = 3f;
    private void Awake()
    {
        Destroy(gameObject, timeLife);
    }

    
}
