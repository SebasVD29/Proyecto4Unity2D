using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float timeLife;
    private void Awake()
    {
        Destroy(gameObject, timeLife);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Toco player");
            Destroy(gameObject, timeLife);
        }
    }
}
