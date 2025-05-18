using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int goblinTogetherStrongHPCost = 1;
    private bool facing; //left: false right: true
    private bool doubleDamageBuff = false;
    public int healthPoint;

    public Slider healthBar;
    private int baseAttackPower;
    [SerializeField] private int startPosition;
    [SerializeField] private GameManager gameManager;
    private CharacterAnimController characterAnimController;

    private bool isTurnEnd = false;
    public bool IsTurnEnd
    {
        set { isTurnEnd = value; }
        get { return isTurnEnd; }
    }

    [Header("Art")]
    [SerializeField] private float offsetForFlip = 0.25f;

    public void Initialize()
    {
        Debug.Log("Initialize");
        facing = true;
        healthPoint = 5;
        healthBar.value = 5;
        baseAttackPower = 3;
        startPosition = 0;
        //gameManager = FindObjectOfType<GameManager>();
        characterAnimController = GetComponent<CharacterAnimController>();
    }

    void Awake()
    {
        Initialize();
    }

    public bool Move(int dist)
    {
        Debug.Log("playermove");
        if (IsVaildMove(dist))
        {
            if (dist > 0)
            {
                if (!facing) ChangeFacingDirection();
            }
            else
            {
                if (facing) ChangeFacingDirection();
            }
            gameManager.allPositions[startPosition] = null;
            //transform.position += new Vector3(1, 0, 0) * dist;
            StartCoroutine("PlayerMoving", dist);
            startPosition += dist;
            gameManager.allPositions[startPosition] = gameObject;
            return true;
        }
        else
        {
            Invoke("TurnEnd", 1f);
        }
        return false;
    }

    private IEnumerator PlayerMoving(int dist)
    {
        Debug.Log(dist);
        characterAnimController.SetIsMoveing(true);
        Vector3 targetPos = transform.position + new Vector3(1, 0, 0) * dist;
        while (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        characterAnimController.SetIsMoveing(false);
        Invoke("TurnEnd", 1f);
    }

    public void Attack()
    {
        int dir = facing ? 1 : -1;
        if (IsEnemyInPos(dir + startPosition))
        {
            AttackEnemy(0, dir + startPosition);
        }
        else if (IsEnemyInPos(startPosition - dir))
        {
            AttackEnemy(0, startPosition - dir);
            ChangeFacingDirection();
        }

        characterAnimController.TriggerAttacking();
        Debug.Log("Player Attack Damage:" + baseAttackPower);

        Invoke("TurnEnd", 1f);

    }

    public void Pass()
    {
        Debug.Log("Player Pass");
        Invoke("TurnEnd", 1f);
    }

    public void TakeAbility()
    {
        int dice = UnityEngine.Random.Range(1, 101);
        int dir = facing ? 1 : -1;
        if (IsEnemyInPos(dir + startPosition))
        {
            foreach (Enemy enemy in gameManager.enemyList)
            {
                if (enemy.GetEnemyPosition() == dir + startPosition && dice < gameManager.additionSuccessRate + gameManager.takeAbilitySuccessRate + enemy.EnemyAdditionRate())
                {
                    AttackEnemy(2000, startPosition + dir);
                }
            }
            if (gameManager.additionSuccessRate > 0)
            {
                gameManager.additionSuccessRate = 0;
            }
        }
        else if (IsEnemyInPos(startPosition - dir))
        {
            foreach (Enemy enemy in gameManager.enemyList)
            {
                if (enemy.GetEnemyPosition() == startPosition - dir && dice < gameManager.additionSuccessRate + gameManager.takeAbilitySuccessRate + enemy.EnemyAdditionRate())
                {
                    AttackEnemy(2000, startPosition - dir);
                }
            }
        }
        Debug.Log("Make Enemy Surrender");
        Invoke("TurnEnd", 1f);
    }

    public void GoblinTogetherStrong()
    {
        Debug.Log("GoblinTogetherStrong");
        healthPoint -= goblinTogetherStrongHPCost;
        healthBar.value = healthPoint;
        gameManager.additionSuccessRate += 50;
    }

    public void DoubleDamage()
    {
        doubleDamageBuff = true;
    }

    public void CorrosiveVenom()
    {
        int attackRange = 3;
        int dir = facing ? 1 : -1;
        for (int i = 1; i <= attackRange; i++)
        {
            if (IsEnemyInPos(dir * i + startPosition))
            {
                CorrsiveVenomEnemy(0, dir * i + startPosition);
                return;
            }
            if (IsEnemyInPos(startPosition - dir * i))
            {
                CorrsiveVenomEnemy(0, startPosition - dir * i);
                facing = !facing;
                return;
            }
        }
    }

    private int UseDoubleDamageBuff()
    {
        if (doubleDamageBuff)
        {
            doubleDamageBuff = false;
            return 2;
        }
        return 1;
    }

    public void GetNewPower()
    {
        Debug.Log("Player Get New Power");
    }

    private bool IsVaildMove(int dist)
    {
        int distAbs = (dist < 0) ? -dist : dist;
        int dir = dist / distAbs;
        for (int i = 1; i <= distAbs; i++)
        {
            int target = dir * i + startPosition;
            if (IsEnemyInPos(target))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsEnemyInPos(int pos)
    {
        Debug.Log("IsEnemyInPos");
        foreach (Enemy enemy in gameManager.enemyList)
        {
            if (enemy.GetEnemyPosition() == pos)
            {
                return true;
            }
        }
        return false;
    }

    private void AttackEnemy(int extraPower, int pos)
    {
        Debug.Log("attackEnemy");
        foreach (Enemy enemy in gameManager.enemyList)
        {
            if (enemy.GetEnemyPosition() == pos)
            {
                enemy.Hitten((baseAttackPower + extraPower) * UseDoubleDamageBuff());
                Debug.Log("enemy hitten");
            }
        }
    }



    private void CorrsiveVenomEnemy(int extraPower, int pos)
    {
        foreach (Enemy enemy in gameManager.enemyList)
        {
            if (enemy.GetEnemyPosition() == pos)
            {
                enemy.Posion();
                Debug.Log("enemy hitten");
            }
        }
    }

    private void ChangeFacingDirection()
    {
        facing = !facing;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (facing) transform.position += new Vector3(offsetForFlip, 0, 0);
        else transform.position += new Vector3(-offsetForFlip, 0, 0);
    }

    private void TurnEnd()
    {
        isTurnEnd = true;
    }

    public int GetPlayerPosition()
    {
        return startPosition;
    }
}
