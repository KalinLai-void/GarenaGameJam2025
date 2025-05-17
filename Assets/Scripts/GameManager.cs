using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Player player;
    public Dictionary<Vector3, bool> allPositions = new Dictionary<Vector3, bool>();
    public List<CardTypeData> hands;
    private int takeAbilitySuccessRate;
    private int additionSuccessRate;
    private int MP;
    private int currCardId;
    public List<Card> allCards;
    private Enemy[] enemyList;  
    public GameObject cardPrefab;
    private int cardsInHandCount;
    private Vector3 cardDefultPos;


    public void Initialize()
    {
        currCardId = 0;
        cardsInHandCount = 3;
        takeAbilitySuccessRate = 10;
        additionSuccessRate = 0;

        cardDefultPos = new Vector3(0, -3, 0);
        enemyList = FindObjectsOfType<Enemy>();  
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
        foreach (Enemy enemy in enemyList)
        {
            enemy.EnemyAction();
        }
        TurnProcess();
    }

    private void GenerateCards(Vector3 cardPos)
    {
        cardPrefab.GetComponent<Card>().cardId = currCardId;
        GameObject card = Instantiate(cardPrefab, cardPos, transform.rotation);
        Card cardcom = card.GetComponent<Card>();
        allCards.Add(cardcom);
        currCardId++;
    }
}
