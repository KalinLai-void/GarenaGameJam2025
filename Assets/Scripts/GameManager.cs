using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject cardPrefab;
    private int CardsInHandCount;
    private Vector3 CardDefultPos;
    

    public void Initialize()
    {
        CardsInHandCount = 3;
        CardDefultPos = new Vector3(0, -3, 0);
    }

    public void Awake()
    {
        Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        TurnProcess();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TurnProcess() //回合流程
    {
        DrawCards();
    }

    private void DrawCards()
    {
        for (int i = 0; i < CardsInHandCount; i++)
        {
            Vector3 cardPos = new Vector3(i * 3, 0, 0) + CardDefultPos;
            GenerateCards(cardPos);
        }
    }

    private void PlayerMove()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
    }

    private void GenerateCards(Vector3 cardPos)
    {
        Instantiate(cardPrefab, cardPos, transform.rotation);
    }
}
