using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float playerSalud = 100;
    public float playerDamage = 20;
    public int playerCuras = 1;
}
