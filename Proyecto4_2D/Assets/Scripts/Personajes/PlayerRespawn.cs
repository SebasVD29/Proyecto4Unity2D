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
        //Obtien las variables del checkpoint 
        string level = PlayerPrefs.GetString("checkPointLevel");
        float x = PlayerPrefs.GetFloat("checkPointX");
        float y = PlayerPrefs.GetFloat("checkPointY");
        //Valida si el nivel actual es el mismo del ultimo checkpoint 
        if (actualLevel == level)
        {
            transform.position = new Vector2(x, y);
        }
    }

    public void ReachedCheckkPoint(string level, float x, float y)
    {
        PlayerPrefs.SetString("checkPointLevel", level);
        PlayerPrefs.SetFloat("checkPointX", x);
        PlayerPrefs.SetFloat("checkPointY", y);
    }

    public void PlayerDameged()
    {
        animator.Play("Idle");
        Invoke("LoadLevel", 1);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
