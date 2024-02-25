using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossGeneralManager : MonoBehaviour
{
    public GameObject bossHealthPanel;
    public Animator bossBackgroundAnimator;

    public static BossGeneralManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        bossHealthPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossActivator()
    {
        bossHealthPanel.SetActive(true);
    }
}
