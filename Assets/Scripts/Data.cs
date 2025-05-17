using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Move, Attack, Pass, MakeEnemySurrender
}

public enum EnemyAction
{
    MoveLeft, MoveRight, ChangeDirection
}

[System.Serializable]
public struct CardTypeData
{
    public CardType cardType;
}

[System.Serializable]
public struct EnemyActionData
{
    public EnemyAction enemyAction;
}

public struct Deck
{
    public List<CardType> deckCards;
}