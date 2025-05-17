using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum CardType
{
    Move, Attack, Pass, MakeEnemySurrender
}
[System.Serializable]
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