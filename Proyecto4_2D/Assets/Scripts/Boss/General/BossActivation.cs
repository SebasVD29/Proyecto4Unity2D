using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private void Start()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossGeneralManager.instance.BossActivator();
            StartCoroutine(FreezePlayer());
            
        }
    }

    IEnumerator FreezePlayer()
    {
        float speed = 0; //Player.instance.runSpeed;
        //Player.instance.runSpeed = 0 ;
        boss.SetActive(true);
        yield return new WaitForSeconds(3f);
        //Player.instance.runSpeed = speed;
        Destroy(gameObject);
    }

}
