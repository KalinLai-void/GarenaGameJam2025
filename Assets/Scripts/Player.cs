using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool facing; //left: false right: true
    private int healthPoint;
    private int baseAttackPower;
    private GameManager gameManager;

    public void Initialize()
    {
        facing = true;
        healthPoint = 5;
        baseAttackPower = 1;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Awake()
    {
        Initialize();
    }

    public bool Move(int dist)
    {
        if (IsVaildMove(dist))
        {
            if (dist > 0)
            {
                facing = true;
            }
            else
            {
                facing = false;
            }
            gameManager.allPositions[transform.position] = false;
            transform.position += new Vector3(1, 0, 0) * dist;
            gameManager.allPositions[transform.position] = true;
            return true;
        }
        return false;
    }

    public void Attack()
    {
        Vector3 dir = new Vector3(facing ? 1 : -1, 0, 0);
        if (IsEnemyInPos(dir + transform.position))
        {
            AttackEnemy(0, dir + transform.position);
        }
        else if (IsEnemyInPos(transform.position - dir))
        {
            AttackEnemy(0, transform.position - dir);
            facing = !facing;
        }
        //Debug.Log("Player Attack Damage:" + baseAttackPower);
    }

    public void Pass()
    {
        //Debug.Log("Player Pass");
    }

    public void TakeAbility()
    {
        int dice = Random.Range(1, 101);
        if (dice < gameManager.takeAbilitySuccessRate + gameManager.additionSuccessRate)
        {
            Vector3 dir = new Vector3(facing ? 1 : -1, 0, 0);
            if (IsEnemyInPos(dir + transform.position))
            {
                AttackEnemy(2000, dir + transform.position);

            }
            else if (IsEnemyInPos(transform.position - dir))
            {
                AttackEnemy(2000, transform.position - dir);
                facing = !facing;
            }
        }
        //Debug.Log("Make Enemy Surrender");
    }
    public void GetNewPower()
    {
        Debug.Log("Player Get New Power");
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

    private bool IsEnemyInPos(Vector3 pos)
    {
        Debug.Log("IsEnemyInPos");
        foreach (Enemy enemy in gameManager.enemyList)
        {
            if (enemy.transform.position == pos)
            {
                return true;
            }
        }
        return false;
    }

    private void AttackEnemy(int extraPower, Vector3 pos)
    {
        Debug.Log("attackEnemy");
        foreach (Enemy enemy in gameManager.enemyList)
        {
            if (enemy.transform.position == pos)
            {
                enemy.Hitten(baseAttackPower + extraPower);
                Debug.Log("enemy hitten");
            }
        }
    }
}
