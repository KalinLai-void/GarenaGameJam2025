using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.WebSockets;
using System.Reflection;
using UnityEditor.Rendering;
using UnityEngine;

//
public class GameManager : MonoBehaviour
{
    public Player player;
    public Dictionary<int, GameObject> allPositions = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> enemyPositions = new Dictionary<int, GameObject>();
    public List<CardTypeData> hands;
    public int takeAbilitySuccessRate;
    public int additionSuccessRate;
    private bool isInvalidUseCard;
    [SerializeField] private int MP;
    private int currCardId;
    private float enemyActionTime = 2.1f;
    private float turnProcessTime = 3.1f;
    public List<Card> allCards;
    public List<Enemy> enemyList;
    public GameObject cardPrefab;
    private SkillUIGenerator skillUIGenerator;
    private int cardsInHandCount;
    private Vector3 cardDefultPos;
    [SerializeField] private Deck deck;

    private bool isPlayerTurn; // false: enemy, true: player
    private bool doTurnChecking = false;

    public void Initialize()
    {
        isInvalidUseCard = true;
        currCardId = 0;
        cardsInHandCount = 3;
        takeAbilitySuccessRate = 10;
        additionSuccessRate = 0;
        MP = 3;
        skillUIGenerator = GetComponent<SkillUIGenerator>();
        DeckInitialize();

        cardDefultPos = new Vector3(0, -3, 0);
    }

    private void DeckInitialize()
    {
        CardType[] values = (CardType[])System.Enum.GetValues(typeof(CardType));
        deck.deckCards = new List<CardTypeData>();
        deck.discardCards = new List<CardTypeData>();

        foreach (CardType type in values)
        {
            CardTypeData data;
            data.cardType = type;
            data.cost = 0;
            data.moveBlock = 0;
            if (type == CardType.Move)
            {
                for (int i = -3; i <= 3; i++)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    data.moveBlock = i;
                    deck.deckCards.Add(data);
                }
            }
            else if (type == CardType.takeAbility)
            {
                data.cost = 1;
                deck.deckCards.Add(data);
            }
            else if (type == CardType.GoblinTogetherStrong)
            {
                data.cost = 1;
            }
            else if (type == CardType.DoubleDamage)
            {
                data.cost = 1;
            }
            else if (type == CardType.CorrosiveVenom)
            {
                data.cost = 2;
            }
            else
            {
                data.cost = 0;
                deck.deckCards.Add(data);
            }
        }

        Shuffle();
    }

    private void Shuffle()
    {
        for (int i = 0; i < deck.deckCards.Count; i++)
        {
            int swapIdx = Random.Range(0, deck.deckCards.Count);
            CardTypeData tempData = deck.deckCards[i];
            deck.deckCards[i] = deck.deckCards[swapIdx];
            deck.deckCards[swapIdx] = tempData;
        }
    }

    public void Awake()
    {
        Initialize();
    }

    void Start()
    {
        TurnProcess();
    }

    private void Update()
    {
        if (!doTurnChecking) return;

        if (isPlayerTurn) WaitForPlayerTurnEnd();
        else WaitForEnemyTurnEnd();
    }

    private void TurnProcess() //回合流程
    {
        isInvalidUseCard = true;
        DrawCards();
    }

    private void DrawCards()
    {
        for (int i = 0; i < cardsInHandCount; i++)
        {
            if (hands.Count > 0)
            {
                skillUIGenerator.GenerateSkill(hands[0], currCardId);
                hands.RemoveAt(0);
                currCardId++;
            }
            else
            {
                GenerateCards();
            }
            
        }
    }

    public void EnemyMove()
    {
        Invoke("EnemyAction", enemyActionTime);
        Invoke("TurnProcess", turnProcessTime);
    }
    private void EnemyAction()
    {
        doTurnChecking = true;
        if (isInvalidUseCard)
        {
            isInvalidUseCard = false;
            return;
        }
        Debug.Log("Enemy Move");
        Debug.Log(enemyList.Count);

        isPlayerTurn = false;
        doTurnChecking = true;
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            enemyList[i].EnemyAction();
        }
        if (player.healthPoint <= 0)
        {
            GameOver();
            return;
        }
    }

    private void GenerateCards()
    {
        CardTypeData data;

        if (deck.deckCards.Count > 0)
        {
            data = deck.deckCards[0];
            deck.deckCards.RemoveAt(0);
        }
        else
        {
            deck.deckCards = deck.discardCards;
            Shuffle();
            deck.discardCards = new List<CardTypeData>();
            data = deck.deckCards[0];
            deck.deckCards.RemoveAt(0);
        }
        skillUIGenerator.GenerateSkill(data, currCardId);
        currCardId++;
    }

    public void AddToDiscardCards(CardTypeData data)
    {
        deck.discardCards.Add(data);
    }
    public void PushCard(Card card)
    {
        allCards.Add(card);
    }

    public List<Card> GetAllCards()
    {
        return allCards;
    }

    public int GetMP()
    {
        return MP;
    }

    public void CostMP(int cost)
    {
        MP -= cost;
    }

    public void PlayerTurn()
    {
        doTurnChecking = true;
        isPlayerTurn = true;
    }

    private void WaitForPlayerTurnEnd()
    {
        //Debug.Log(player.IsTurnEnd);
        if (!player.IsTurnEnd) return; // wait for player's turn end
        doTurnChecking = false;

        Invoke("EnemyAction", 0.5f);
    }

    private void WaitForEnemyTurnEnd()
    {
        // wait for enemies's turn end
        foreach (Enemy enemy in enemyList)
        {
            if (!enemy.IsTurnEnd)
            {
                return;
            }
        }

        ResetTurn();        
        Invoke("TurnProcess", 0.5f);
    }

    private void ResetTurn()
    {
        doTurnChecking = false;
        isPlayerTurn = true;
        player.IsTurnEnd = false;
        foreach (Enemy enemy in enemyList)
        {
            enemy.IsTurnEnd = false;
        }
    }

    public void UseInvalidCard()
    {
        isInvalidUseCard = true;
    }

    public bool IsTriggerCardValid()
    {
        return isInvalidUseCard;
    }

    private void GameOver()
    {

    }
}
