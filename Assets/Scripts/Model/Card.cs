using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CardSelectedState { NORMAL, SELECTED }
public enum CardDragedState { NORMAL, DRAGING }

[Serializable]
public class Cards
{
    public Dictionary<string, Card> cards;
}

[Serializable]
public class Card
{
    public string name;
    public int cost;
    public int blood;
    public float atttack;
}
