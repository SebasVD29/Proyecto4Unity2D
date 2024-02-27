using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
  
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Asigna el animator al iniciar el nivel 
        animator = GetComponent<Animator>();
        //Obtiene el nivel actual 
        string actualLevel = SceneManager.GetActiveScene().name;
        
    }
     public void ReachedCheckPoint(string level, float x, float y)
    {
        PlayerPrefs.SetString("checkPointLevel", level);
        PlayerPrefs.SetFloat("checkPointX", x);
        PlayerPrefs.SetFloat("checkPointY", y);
    }
      public void PlayerDeath()
    {
        animator.Play("Death");
        Invoke("LoadLevel", 1);
    }

    public void PlayerDameged()
    {

        animator.Play("Hurt");
        //Invoke("LoadLevel", 1);

    }

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

