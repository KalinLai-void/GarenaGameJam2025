using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Move, Attack, Pass, MakeEnemySurrender
}

[System.Serializable]
public struct CardTypeData
{
    public CardType cardType;
}

public struct Deck
{
    public List<CardType> deckCards;
}