using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardId;
    public CardTypeData cardTypeData;
    private float playerMoveTime = 1f;
    private Player player;
    private GameManager gameManager;


    private void Initialize()
    {
        player = FindFirstObjectByType<Player>().GetComponent<Player>();
        gameManager = FindObjectOfType<GameManager>();
        //Debug.Log("Hand: " + cardTypeData.cardType);
    }

    void Awake()
    {
        Initialize();
    }

    public void OnButtonClick()
    {
        if (gameManager.GetMP() < cardTypeData.cost)
        {
            return;
        }
        else
        {
            gameManager.CostMP(cardTypeData.cost);
        }
        Invoke("PlayerMove", playerMoveTime);
        EnemyMove();
    }

    private void PlayerMove()
    {
        Debug.Log("player move");
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

        gameManager.AddToDiscardCards(cardTypeData);

        for (int i = 0; i < gameManager.allCards.Count; i++)
        {
            if (gameManager.allCards[i].cardId != cardId)
            {
                gameManager.hands.Add(gameManager.allCards[i].cardTypeData);
            }
        }
        while (gameManager.allCards.Count > 0)
        {
            gameManager.allCards.RemoveAt(0);
        }
        
    }

    private void EnemyMove()
    {
        Debug.Log("enemy move");
        gameManager.EnemyMove();
    }

/*
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
    */
}
