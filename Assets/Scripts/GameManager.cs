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
    private SkillUIGenerator skillUIGenerator;
    private int cardsInHandCount;
    private Vector3 cardDefultPos;


    public void Initialize()
    {
        currCardId = 0;
        cardsInHandCount = 3;
        takeAbilitySuccessRate = 10;
        additionSuccessRate = 0;
        MP = 3;
        skillUIGenerator = GetComponent<SkillUIGenerator>();

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
        CardTypeData data;
        CardType[] values = (CardType[])System.Enum.GetValues(typeof(CardType)); //暫定 之後程式邏輯會改
        int index = UnityEngine.Random.Range(0, values.Length);
        data.cardType = values[index];
        data.moveBlock = UnityEngine.Random.Range(-3, 4);
        while (data.moveBlock == 0)
        {
            data.moveBlock = Random.Range(-3, 4);
        }
        skillUIGenerator.GenerateSkill(data, currCardId);
        currCardId++;
    }
    public void PushCard(Card card)
    {
        allCards.Add(card);
    }

    public List<Card> GetAllCards()
    {
        return allCards;
    }

    private void GameOver()
    {

    }
}
