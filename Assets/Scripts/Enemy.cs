using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    private int posionCount;
    private float maxHealthPoint;
    [SerializeField] private float enemyHealthPoint;
    [SerializeField] private int startPosition = 5;
    private int enemyBaseAttackPower;
    private bool facing; //方向 left: false right: true
    private Player player;
    private GameManager gameManager;
    private List<EnemyActionData> enemyActionDatasDefult;
    private List<EnemyActionData> enemyActionDatas;
    private CharacterAnimController characterAnimController;

    private bool isTurnEnd = false;
    public bool IsTurnEnd
    {
        set { isTurnEnd = value; }
        get { return isTurnEnd; }
    }

    [Header("Art")]
    [SerializeField] private float offsetForFlip = 0.25f;

    private void Initialize()
    {
        posionCount = 0;
        enemyHealthPoint = 5;
        enemyBaseAttackPower = 1;
        facing = false;
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.allPositions[startPosition] = gameObject;
        gameManager.enemyPositions[startPosition] = gameObject;
        gameManager.enemyList.Add(this);
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
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        characterAnimController = GetComponent<CharacterAnimController>();
    }

    private void Awake()
    {
        Initialize();
    }

    public void SetEnemyHealthPoint(int healthPoint)
    {
        enemyHealthPoint = healthPoint;
    }

    public void Hitten(int damage)
    {
        enemyHealthPoint -= damage;
    }

    public void Posion()
    {
        posionCount = 2;

    }

    private void TriggerPosion()
    {
        int posionDamage = Random.Range(1, 4);
        if (posionCount > 0)
        {
            posionCount--;
            enemyHealthPoint -= posionDamage;
        }
    }

    public void EnemyAction()
    {
        TriggerPosion();
        Debug.Log("enemy action");
        if (enemyHealthPoint <= 0)
        {
            if (enemyHealthPoint < -1000)
            {
                player.GetNewPower();
            }
            gameManager.enemyList.Remove(this);
            gameManager.enemyPositions[startPosition] = null;
            Destroy(gameObject);
            return;
        }
        if (IsFacingPlayer())
        {
            Invoke("TurnEnd", 1f);
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
                        if (facing) ChangeFacingDirection();
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
                        if (!facing) ChangeFacingDirection();
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
                    ChangeFacingDirection();
                    Invoke("TurnEnd", 1f);
                    break;
                }
            }
            enemyActionDatas = enemyActionDatasDefult;
        }
    }
    private void Move(int dist)
    {
        gameManager.allPositions[startPosition] = null;
        gameManager.enemyPositions[startPosition] = null;
        //transform.position += new Vector3(1, 0, 0) * dist;
        StartCoroutine("Moving", dist);
        startPosition += dist;
        gameManager.enemyPositions[startPosition] = gameObject;
        gameManager.allPositions[startPosition] = gameObject;
    }
    private IEnumerator Moving(int dist)
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

    private void Attack()
    {
        characterAnimController.TriggerAttacking();
        player.healthPoint -= enemyBaseAttackPower;
        player.healthBar.value = player.healthPoint;
        //Debug.Log("Enemy Attack" + enemyBaseAttackPower);
        Invoke("TurnEnd", 1f);
    }

    private bool IsFacingPlayer()
    {
        int dir = facing ? 1 : -1;
        int target = dir + startPosition;
        return target == player.GetPlayerPosition();
    }

    private bool IsVaildMove(int dist)
    {
        if (gameManager.allPositions.ContainsKey(startPosition + dist))
        {
            return !gameManager.allPositions[startPosition + dist];
        }
        else
        {
            return true;
        }
    }

    public float EnemyAdditionRate()
    {
        //return 100 - 100 * (maxHealthPoint - enemyHealthPoint);
        return 100 * (1 - (maxHealthPoint - enemyHealthPoint) / maxHealthPoint);
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

    public int GetEnemyPosition()
    {
        return startPosition;
    }
}
