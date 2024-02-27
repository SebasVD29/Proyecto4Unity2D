using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossGeneralManager : MonoBehaviour
{
    [SerializeField] GameObject bossHealthPanel;
    [SerializeField] Animator bossBackgroundAnimator;
   // [SerializeField] GameObject Player;

    public static BossGeneralManager instance;

    [Header("Boss")]
    [SerializeField] GameObject bossAngel;
    [SerializeField] GameObject bossDevil;
  

    public float bossHealthTotal;
    public float angelHealthTotal;
    public float devilHealthTotal;
    
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
        angelHealthTotal = bossAngel.GetComponentInChildren<BossEnemy>().bossHealth;
        devilHealthTotal = bossDevil.GetComponentInChildren<BossEnemy>().bossHealth;
        bossHealthPanel.SetActive(false);
        bossBackgroundAnimator.SetBool("Normal", true);

    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(ChangeBoss());
    }

    public void BossActivator()
    {
        bossHealthPanel.SetActive(true);
        bossAngel.SetActive(true);
    }

    IEnumerator ChangeBoss()
    {
        bossName.text = bossAngel.GetComponentInChildren<BossEnemy>().bossName;
        currentAngelHealth = bossAngel.GetComponentInChildren<BossEnemy>().bossHealth;
        currentDevilHealth = bossDevil.GetComponentInChildren<BossEnemy>().bossHealth;
        
        bossHealthBar.fillAmount = currentAngelHealth / angelHealthTotal;
     
        if (currentAngelHealth <= 0)
        {
            bossAngel.SetActive(false);
            bossBackgroundAnimator.SetBool("Normal", false);
            bossName.text = bossDevil.GetComponentInChildren<BossEnemy>().bossName;
            yield return new WaitForSeconds(1.5f);

            bossDevil.SetActive(true);
      
 
            bossBackgroundAnimator.SetBool("Normal", true);
            bossHealthBar.fillAmount = currentDevilHealth / devilHealthTotal;

            if (currentDevilHealth <= 0 )
            {
                bossDevil.SetActive(false);
                bossHealthPanel.SetActive(false);
            }
        }
    }

    



}
