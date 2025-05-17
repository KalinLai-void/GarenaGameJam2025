using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{

    private int enemyHealthPoint;
    private int enemyBaseAttackPower;
    private void Initialize()
    {
        enemyHealthPoint = 5;
        enemyBaseAttackPower = 2;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Awake()
    {
        Initialize();
    }

    public void EnemyAction()
    {
        Move(1);
    }
    private void Move(int dist)
    {
        transform.position += new Vector3(1, 0, 0) * dist;
    }
    private void Attack()
    {
        Debug.Log("Enemy Attack" + enemyBaseAttackPower);
    }
}
