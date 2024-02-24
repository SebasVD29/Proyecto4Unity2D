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
    
    /*
    public void PlayerDameged()
    {
        animator.Play("Hit");
        Invoke("LoadLevel", 1);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
