using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId;
    public CardTypeData cardTypeData;
    private Player player;
    private GameManager gameManager;


    private void Initialize()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
        cardTypeData = GetRandomCardType();
        gameManager = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
        if (gameManager.hands.Count > 0)
        {
            cardTypeData = gameManager.hands[0];
            gameManager.hands.RemoveAt(0);
        }
        ApplyColor();
        //Debug.Log("Hand: " + cardTypeData.cardType);
    }

    void Awake()
    {
        Initialize();
    }

    private void OnMouseDown()
    {
        if (cardTypeData.cardType == CardType.Move)
        {
            if (!player.Move(cardTypeData.moveBlock))
            {
                return;
            }
        }
        if (cardTypeData.cardType == CardType.Attack)
        {
            player.Attack();
        }
        if (cardTypeData.cardType == CardType.Pass)
        {
            player.Pass();
        }
        if (cardTypeData.cardType == CardType.takeAbility)
        {
            player.TakeAbility();
        }



        for (int i = 0; i < gameManager.allCards.Count; i++)
        {
            if (gameManager.allCards[i].cardId != cardId)
            {
                gameManager.hands.Add(gameManager.allCards[i].cardTypeData);
            }

            Destroy(gameManager.allCards[i].gameObject);
        }
        while (gameManager.allCards.Count > 0)
        {
            gameManager.allCards.RemoveAt(0);
        }
        
        gameManager.EnemyMove();
    }

    private CardTypeData GetRandomCardType()
    {
        CardTypeData data;
        CardType[] values = (CardType[])System.Enum.GetValues(typeof(CardType)); //暫定 之後程式邏輯會改
        int index = UnityEngine.Random.Range(0, values.Length);
        data.cardType = values[index];
        data.moveBlock = UnityEngine.Random.Range(1, 4);
        while (data.moveBlock == 0)
        {
            data.moveBlock = Random.Range(-3, 4);
        }
        return data;
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
        if (cardTypeData.cardType == CardType.takeAbility)
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
