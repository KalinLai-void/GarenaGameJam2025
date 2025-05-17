using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    private int enemyHealthPoint;
    private int enemyBaseAttackPower;
    private bool facing; //方向 left: false right: true
    private Player player;
    private GameManager gameManager;
    private List<EnemyActionData> enemyActionDatasDefult;
    private List<EnemyActionData> enemyActionDatas;

    private void Initialize()
    {
        enemyHealthPoint = 1;
        enemyBaseAttackPower = 1;
        facing = false;
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.allPositions[transform.position] = true;
        enemyActionDatas = new List<EnemyActionData>();
        enemyActionDatasDefult = new List<EnemyActionData>();
        EnemyAction[] values = (EnemyAction[])System.Enum.GetValues(typeof(CardType));
        
        foreach (EnemyAction act in values)
        {
            EnemyActionData actData;
            actData.enemyAction = act;
            enemyActionDatasDefult.Add(actData);
            enemyActionDatas.Add(actData);
        }
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Awake()
    {
        Initialize();
    }

    public void EnemyAction()
    {
        if (IsFacingPlayer())
        {
            Attack();
        }
        else
        {
            while (true)
            {
                int idx = Random.Range(0, enemyActionDatas.Count);
                if (enemyActionDatas[idx].enemyAction == global::EnemyAction.MoveLeft)
                {
                    if (IsVaildMove(-1))
                    {
                        Move(-1);
                        break;
                    }
                    else
                    {
                        enemyActionDatas.RemoveAt(idx);
                    }
                }
                if (enemyActionDatas[idx].enemyAction == global::EnemyAction.MoveRight)
                {
                    if (IsVaildMove(1))
                    {
                        Move(1);
                        break;
                    }
                    else
                    {
                        enemyActionDatas.RemoveAt(idx);
                    }
                }
                if (enemyActionDatas[idx].enemyAction == global::EnemyAction.ChangeDirection)
                {
                    facing = !facing;
                    break;
                }
            }
            enemyActionDatas = enemyActionDatasDefult;
        }
    }
    private void Move(int dist)
    {
        gameManager.allPositions[transform.position] = false;
        transform.position += new Vector3(1, 0, 0) * dist;
        gameManager.allPositions[transform.position] = true;
    }
    private void Attack()
    {
        Debug.Log("Enemy Attack" + enemyBaseAttackPower);
    }

    private bool IsFacingPlayer()
    {
        int dir = facing ? 1 : -1;
        Vector3 target = new Vector3(dir, 0, 0) + transform.position;
        return target == player.transform.position;
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
