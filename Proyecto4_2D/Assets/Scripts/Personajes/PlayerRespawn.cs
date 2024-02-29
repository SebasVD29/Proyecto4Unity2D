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
        //ASIGNA EL ANIMATOR AL INICIAR EL NIVEL
        animator = GetComponent<Animator>();

        //OBTIENE EL NIVEL ACTUAL
        string actualLevel = SceneManager.GetActiveScene().name;
        //OBTIENE LAS VARIABLES DEL CHECK POINT
        string level = PlayerPrefs.GetString("checkPointLevel");
        float x = PlayerPrefs.GetFloat("checkPointX");
        float y = PlayerPrefs.GetFloat("checkPointY");
        //VALIDA SI EL NIVEL ACTUAL ES EL MISMO DEL ULTIMO CHECK POINT
        if (actualLevel == level)
        {
            transform.position = new Vector2(x, y);
        }
        
    }
     public void ReachedCheckPoint(string level, float x, float y)
    {
        PlayerPrefs.SetString("checkPointLevel", level);
        PlayerPrefs.SetFloat("checkPointX", x);
        PlayerPrefs.SetFloat("checkPointY", y);
    }
      public void PlayerDeath()
    {
     // animator.Play("Death");
       Invoke("LoadLevel", 1);
    }
    public void PlayerDameged()
    {

        animator.SetTrigger("Hurt");
        //Invoke("LoadLevel", 1);

    }

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

