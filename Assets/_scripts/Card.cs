using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Card : MonoBehaviour, IPointerClickHandler
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

    public class CardClickedEvent : UnityEvent<Card>{}

    #region public properties
    public Card_Rank Rank { get; private set; }
    public Card_Suit Suit { get; private set; }
    public Sprite Front
    {
        get
        {
            return _image.sprite;
        }
        set
        {
            _image.sprite = value;
        }
    }
    public bool IsInitialized { get; private set; }

    public CardClickedEvent CardClicked = new CardClickedEvent();

    public bool IsWild{get { return Rank == Card_Rank.EIGHT; }}
    #endregion
    #region public methods
    public void Init(Card_Rank rank, Card_Suit suit, Sprite front)
    {
        Rank = rank;
        Suit = suit;
        Front = front;
        //set name
        name = Enum.GetName(typeof(Card_Rank), rank) + " of " + Enum.GetName(typeof(Card_Suit), suit);

        IsInitialized = true;
    }

    public override string ToString()
    {
        return Enum.GetName(typeof(Card_Rank), Rank) + " of " + Enum.GetName(typeof(Card_Suit), Suit);
    }
    #endregion
    void Awake()
    {
        IsInitialized = false;
        _image = GetComponent<Image>();
        if (_image == null)
        {
            _image = gameObject.AddComponent<Image>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardClicked.Invoke(this);
    }

    private Image _image;
}
