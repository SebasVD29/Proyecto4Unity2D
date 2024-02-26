using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossGeneralManager : MonoBehaviour
{
    public GameObject bossHealthPanel;
    public Animator bossBackgroundAnimator;

    public static BossGeneralManager instance;

    [Header("Boss")]
    [SerializeField] GameObject bossAngel;
    [SerializeField] GameObject bossDevil;

    public float bossHealthTotal;
    public float currentAngelHealth;
    public float currentDevilHealth;

    public Image bossHealthBar;
    public Text bossName;

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
        bossHealthTotal = bossAngel.GetComponentInChildren<BossEnemy>().bossHealth + bossDevil.GetComponentInChildren<BossEnemy>().bossHealth;
        bossHealthPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBoss();
    }

    public void BossActivator()
    {
        bossHealthPanel.SetActive(true);
    }

    void ChangeBoss()
    {
        
        //bossHealthBar.fillAmount = currentBossHealth / bossHealthTotal;
    }

    



}
