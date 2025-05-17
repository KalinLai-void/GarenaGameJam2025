using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Player player;
    public List<CardTypeData> hands;
    private int currCardId;

    public GameObject cardPrefab;
    private int cardsInHandCount;
    private Vector3 cardDefultPos;
    

    public void Initialize()
    {
        currCardId = 0;
        cardsInHandCount = 3;
        cardDefultPos = new Vector3(0, -3, 0);
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
        for (int i = 0; i < cardsInHandCount; i++)
        {
            Vector3 cardPos = new Vector3(i * 3, 0, 0) + cardDefultPos;
            GenerateCards(cardPos);
        }
        Debug.Log("handsCount: " + hands.Count);
    }

    public void EnemyMove()
    {
        Debug.Log("Enemy Move");
        TurnProcess();
    }

    private void GenerateCards(Vector3 cardPos)
    {
        cardPrefab.GetComponent<Card>().cardId = currCardId;
        Instantiate(cardPrefab, cardPos, transform.rotation);
        currCardId++;
    }
}
