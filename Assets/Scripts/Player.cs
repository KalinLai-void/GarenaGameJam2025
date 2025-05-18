using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool facing; //left: false right: true
    public int healthPoint;
    private int baseAttackPower;
    [SerializeField] private GameManager gameManager;
    private CharacterAnimController characterAnimController;

    private bool isTurnEnd = false;
    public bool IsTurnEnd { 
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
        baseAttackPower = 1;
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
            gameManager.allPositions[transform.position] = false;
            //transform.position += new Vector3(1, 0, 0) * dist;
            StartCoroutine("PlayerMoving", dist);
            gameManager.allPositions[transform.position] = true;
            return true;
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
        Vector3 dir = new Vector3(facing ? 1 : -1, 0, 0);
        if (IsEnemyInPos(dir + transform.position))
        {
            AttackEnemy(0, dir + transform.position);
        }
        else if (IsEnemyInPos(transform.position - dir))
        {
            AttackEnemy(0, transform.position - dir);
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
        Vector3 dir = new Vector3(facing ? 1 : -1, 0, 0);
        if (IsEnemyInPos(dir + transform.position))
        {
            foreach (Enemy enemy in gameManager.enemyList)
            {
                if (enemy.transform.position == dir + transform.position && dice < gameManager.additionSuccessRate + gameManager.takeAbilitySuccessRate + enemy.EnemyAdditionRate())
                {
                    AttackEnemy(2000, transform.position + dir);
                }
            }
            if (gameManager.additionSuccessRate > 0)
            {
                gameManager.additionSuccessRate = 0;
            }
        }
        else if (IsEnemyInPos(transform.position - dir))
        {
            foreach (Enemy enemy in gameManager.enemyList)
            {
                if (enemy.transform.position == dir + transform.position && dice < gameManager.additionSuccessRate + gameManager.takeAbilitySuccessRate + enemy.EnemyAdditionRate())
                {
                    AttackEnemy(2000, transform.position - dir);
                }
            }
        }
        Debug.Log("Make Enemy Surrender");
        Invoke("TurnEnd", 1f);
    }

    public void GoblinTogetherStrong()
    {
        Debug.Log("GoblinTogetherStrong");
        gameManager.additionSuccessRate += 50;
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
}
