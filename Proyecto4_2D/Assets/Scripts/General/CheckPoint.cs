using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //OBTIENE EL NIVEL Y LA POSICION EN X - Y
            string level = SceneManager.GetActiveScene().name;
            float x = collision.transform.position.x;
            float y = collision.transform.position.y;
            collision.GetComponent<PlayerRespawn>().ReachedCheckPoint(level, x, y);
            //APLICA LA ANIMACION DEL CHECK POINT
            GetComponent<Animator>().Play("FlagOut");
        }
    }
}
