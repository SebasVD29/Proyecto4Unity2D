using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{ 
    public GameObject CambioNiveles;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            CambioNiveles.SetActive(true);
            Invoke("ChangeLevel", 2);
        }
    }
    void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
