using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Obtiene el nivel y la posicion en x-y 
            string level = SceneManager.GetActiveScene().name;
            float x = collision.transform.position.x;
            float y = collision.transform.position.y;
            collision.GetComponent<PlayerRespawn>().ReachedCheckkPoint(level, x, y);

            //Aplica la animacion del checkpoint 
            GetComponent<Animator>().Play("FlagOut");
        }
    }
}
