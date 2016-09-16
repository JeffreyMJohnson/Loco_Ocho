using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    #region enums
    public enum Card_Suit
    {
        HEARTS,
        DIAMONDS,
        SPADES,
        CLUBS
    }

    public enum Card_Rank
    {
        ACE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        TEN = 10,
        JACK = 11,
        QUEEN = 12,
        KING = 13
    }
    #endregion

    public Card_Rank Rank { get; private set; }
    public Card_Suit Suit { get; private set; }
    public Sprite Front { get; private set; }
    public bool IsInitialized { get; private set; }

    public void Init(Card_Rank rank, Card_Suit suit, Sprite front)
    {
        Rank = rank;
        Suit = suit;
        Front = front;
        GetComponent<Image>().sprite = front;
        //set name
        name = Enum.GetName(typeof(Card_Rank), rank) + " of " + Enum.GetName(typeof(Card_Suit), suit);

        IsInitialized = true;
    }

    void Awake()
    {
        IsInitialized = false;
    }


}
