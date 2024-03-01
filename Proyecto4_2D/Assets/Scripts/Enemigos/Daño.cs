using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Daño : MonoBehaviour
{
    Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            collision.gameObject.GetComponent<PlayerRespawn>().PlayerDameged();
        }
    }
    
   
}
