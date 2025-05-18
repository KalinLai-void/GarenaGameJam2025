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
            gameManager.UseInvalidCard();
        }
        else
        {
            gameManager.CostMP(cardTypeData.cost);
            Invoke("PlayerMove", playerMoveTime);
            gameManager.PlayerTurn();
            EnemyMove();
        }
    }

    private bool TriggerCardAction()
    {
        bool addToDiscard = true;
        Debug.Log("player move");
        if (cardTypeData.cardType == CardType.Move)
        {
            if (!player.Move(cardTypeData.moveBlock))
            {
                gameManager.UseInvalidCard();
                addToDiscard = false;
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
        if (cardTypeData.cardType == CardType.GoblinTogetherStrong)
        {
            player.GoblinTogetherStrong();
        }
        if (cardTypeData.cardType == CardType.DoubleDamage)
        {
            player.DoubleDamage();
        }
        if (cardTypeData.cardType == CardType.CorrosiveVenom)
        {
            player.CorrosiveVenom();
        }
        return addToDiscard;
    }

    private void PlayerMove()
    {
        bool addToDiscard;
        if (gameManager.IsTriggerCardValid())
        {
            addToDiscard = TriggerCardAction();
        }
        else
        {
            addToDiscard = true;
        }
        
        if (addToDiscard)
        {
            gameManager.AddToDiscardCards(cardTypeData);
        }

        for (int i = 0; i < gameManager.allCards.Count; i++)
        {
            if (!addToDiscard)
            {
                gameManager.hands.Add(gameManager.allCards[i].cardTypeData);
            }
            else if (gameManager.allCards[i].cardId != cardId)
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
