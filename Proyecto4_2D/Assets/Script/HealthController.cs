using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public Image HealthBar;
    public float vidaActual;
    public float vidaMaxima;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = vidaActual / vidaMaxima;
        
    }
}
