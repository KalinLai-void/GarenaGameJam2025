using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int healthPoint;
    private int baseAttackPower;

    public void Initialize()
    {
        healthPoint = 5;
        baseAttackPower = 2;
    }

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(1);
        }
    }

    public void Move(int dist)
    {
        transform.position += new Vector3(1, 0, 0) * dist;
    }

    public void Attack()
    {
        Debug.Log("Player Attack Damage:" + baseAttackPower);
    }

    public void Pass()
    {
        Debug.Log("Player Pass");
    }

    public void MakeEnemySurrender()
    {
        Debug.Log("Make Enemy Surrender");
    }
}
