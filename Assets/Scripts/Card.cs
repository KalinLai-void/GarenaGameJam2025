using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardType
    {
        Move, Attack, Pass, MakeEnemySurrender
    }

    public struct CardTypeData
    {
        public CardType cardType;
    }

    public struct Deck
    {
        public List<CardType> deckCards;
    }

    private Player player;
    private CardTypeData cardTypeData;


    private void Initialize()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
        cardTypeData.cardType = GetRandomCardType();
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
    }

    private CardType GetRandomCardType()
    {
        CardType[] values = (CardType[])System.Enum.GetValues(typeof(CardType)); //暫定 之後程式邏輯會改
        int index = UnityEngine.Random.Range(0, values.Length);
        return values[index];
    }
}
