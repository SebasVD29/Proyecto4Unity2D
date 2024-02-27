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
    [SerializeField] GameObject boss;
    public float bossHealth;
    public float currentBossHealth;

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
       
        bossHealthPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        BossDamage();
    }

    public void BossActivator()
    {
        bossHealthPanel.SetActive(true);
    }

    void BossDamage()
    {
        bossName.text = boss.GetComponentInChildren<BossEnemy>().bossName;
        currentBossHealth = boss.GetComponentInChildren<BossEnemy>().bossHealth;
        bossHealthBar.fillAmount = currentBossHealth / bossHealth;
    }

    void ChangeBoss()
    {

    }



}
