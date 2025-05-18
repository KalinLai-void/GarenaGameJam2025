using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum CardType
{
    Move, Attack, Pass, takeAbility, GoblinTogetherStrong, DoubleDamage, CorrosiveVenom
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
    public int moveBlock;
    public int cost;
}

[System.Serializable]
public struct EnemyActionData
{
    public EnemyAction enemyAction;
}

[System.Serializable]
public struct Deck
{
    public List<CardTypeData> deckCards;
    public List<CardTypeData> discardCards;
}