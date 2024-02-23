using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float timeLife = 3f;
    private void Awake()
    {
        Destroy(gameObject, timeLife);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DañoPlayer");
            //Destroy(this.gameObject);
        }
    }
}
