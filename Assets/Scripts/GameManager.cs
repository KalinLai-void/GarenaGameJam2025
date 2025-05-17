using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Player player;
    public Dictionary<Vector3, bool> allPositions = new Dictionary<Vector3, bool>();
    public List<CardTypeData> hands;
    public int takeAbilitySuccessRate;
    public int additionSuccessRate;
    private int MP;
    private int currCardId;
    public List<Card> allCards;
    public List<Enemy> enemyList;
    public GameObject cardPrefab;
    private int cardsInHandCount;
    private Vector3 cardDefultPos;


    public void Initialize()
    {
        currCardId = 0;
        cardsInHandCount = 3;
        takeAbilitySuccessRate = 10;
        additionSuccessRate = 0;
        MP = 3;

        cardDefultPos = new Vector3(0, -3, 0);
    }

    public void Awake()
    {
        Initialize();
    }

    void Start()
    {
        TurnProcess();
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
    }

    public void EnemyMove()
    {
        Debug.Log("Enemy Move");
        Debug.Log(enemyList.Count);
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            enemyList[i].EnemyAction();
        }
        if (player.healthPoint <= 0)
        {
            GameOver();
            return;
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

    public List<Card> GetAllCards()
    {
        return allCards;
    }

    private void GameOver()
    {
        
    }
}
