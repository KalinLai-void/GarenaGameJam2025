using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId;
    private CardTypeData cardTypeData;
    private Player player;
    private GameManager gameManager;


    private void Initialize()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
        cardTypeData.cardType = GetRandomCardType();
        gameManager = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
        if (gameManager.hands.Count > 0)
        {
            cardTypeData = gameManager.hands[0];
            gameManager.hands.RemoveAt(0);
        }
        ApplyColor();
        Debug.Log("Hand: " + cardTypeData.cardType);
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();

    }

    private void OnMouseDown()
    {
        if (cardTypeData.cardType == CardType.Move)
        {
            player.Move(1);
        }
        if (cardTypeData.cardType == CardType.Attack)
        {
            player.Attack();
        }
        if (cardTypeData.cardType == CardType.Pass)
        {
            player.Pass();
        }
        if (cardTypeData.cardType == CardType.MakeEnemySurrender)
        {
            player.MakeEnemySurrender();
        }

        Card[] allCards = FindObjectsOfType<Card>();

        foreach (Card card in allCards)
        {
            if (card.cardId != cardId)
            {
                gameManager.hands.Add(card.cardTypeData);
            }
            Destroy(card.gameObject);
        }
        gameManager.EnemyMove();
    }

    private CardType GetRandomCardType()
    {
        CardType[] values = (CardType[])System.Enum.GetValues(typeof(CardType)); //暫定 之後程式邏輯會改
        int index = UnityEngine.Random.Range(0, values.Length);
        return values[index];
    }

    private Color GetColorByType()
    {
        if (cardTypeData.cardType == CardType.Move)
        {
            return Color.black;
        }
        if (cardTypeData.cardType == CardType.Attack)
        {
            return Color.red;
        }
        if (cardTypeData.cardType == CardType.Pass)
        {
            return Color.yellow;
        }
        if (cardTypeData.cardType == CardType.MakeEnemySurrender)
        {
            return Color.blue;
        }
        return Color.white;
    }

    private void ApplyColor()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = GetColorByType();
    }
}
