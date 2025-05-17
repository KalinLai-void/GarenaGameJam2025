using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int healthPoint;
    private int baseAttackPower;
    private GameManager gameManager;

    public void Initialize()
    {
        healthPoint = 5;
        baseAttackPower = 2;
        gameManager = FindObjectOfType<GameManager>();
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

    public bool Move(int dist)
    {
        if (IsVaildMove(dist))
        {
            gameManager.allPositions[transform.position] = false;
            transform.position += new Vector3(1, 0, 0) * dist;
            gameManager.allPositions[transform.position] = true;
            return true;
        }
        return false;   
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

    private bool IsVaildMove(int dist)
    {
        Vector3 target = new Vector3(dist, 0, 0) + transform.position;
        if (gameManager.allPositions.ContainsKey(target))
        {
            return !gameManager.allPositions[target];
        }
        else
        {
            return true;
        }
    }
}
