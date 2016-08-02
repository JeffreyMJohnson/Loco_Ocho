using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;

public class EmptyDeckException : Exception
{
    public EmptyDeckException()
    {
    }

    public EmptyDeckException(string message) : base(message)
    {
    }

    public EmptyDeckException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class Deck
{
    public enum Suit
    {
        HEARTS,
        DIAMONDS,
        SPADES,
        CLUBS
    }

    public enum Rank
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

    public Card[] Cards { get { return _cards.ToArray(); } }

    private const int TOTAL_CARDS_IN_DECK = 52;
    private const int SHUFFLE_ITERATION_COUNT = 5;

    private List<Card> _cards = null;
    private System.Random rand = new System.Random();
    public Deck(Sprite[] fronts)
    {
        _cards = new List<Card>(TOTAL_CARDS_IN_DECK);
        CreateDeck(fronts);
    }

    public void Shuffle()
    {
        for (int i = 0; i < SHUFFLE_ITERATION_COUNT; ++i)
        {
            //start at end of list
            int listIndex = _cards.Count;
            while (listIndex > 1)
            {
                //working way to the beginning of list
                listIndex--;
                //get random int from 0(inclusive) to 1 + listIndex(exclusive)
                int randomIndex = rand.Next(listIndex + 1);
                //swap the random card with the current card on in list
                Card randomCard = _cards[randomIndex];
                _cards[randomIndex] = _cards[listIndex];
                _cards[listIndex] = randomCard;
            }
        }
    }

    /// <summary>
    /// removes the top card in the deck and returns it.  Throws EmptyDeckException if deck is empty.
    /// </summary>
    /// <returns>Card from deck.</returns>
    public Card DealCard()
    {
        if (_cards.Count == 0)
        {
            throw new EmptyDeckException("Foo");
        }
        Card topCard = _cards[0];
        _cards.RemoveAt(0);
        return topCard;
    }

    #region constructor helper functions
    private void CreateDeck(Sprite[] fronts)
    {

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Card newCard = new Card(suit, rank, GetSuit(suit), GetRank(rank), GetCardFront(fronts, rank, suit));
                _cards.Add(newCard);
            }
        }
    }

    private string GetSuit(Deck.Suit suit)
    {
        switch (suit)
        {
            case Deck.Suit.CLUBS:
                return "Clubs";
            case Deck.Suit.DIAMONDS:
                return "Diamonds";
            case Deck.Suit.HEARTS:
                return "Hearts";
            case Deck.Suit.SPADES:
                return "Spades";
            default:
                return "";
        }
    }

    private string GetRank(Deck.Rank rank)
    {
        switch (rank)
        {
            case Deck.Rank.ACE:
                return "Ace";
            case Deck.Rank.EIGHT:
                return "Eight";
            case Deck.Rank.FIVE:
                return "Five";
            case Deck.Rank.FOUR:
                return "Four";
            case Deck.Rank.JACK:
                return "Jack";
            case Deck.Rank.KING:
                return "King";
            case Deck.Rank.NINE:
                return "Nine";
            case Deck.Rank.QUEEN:
                return "Queen";
            case Deck.Rank.SEVEN:
                return "Seven";
            case Deck.Rank.SIX:
                return "Six";
            case Deck.Rank.TEN:
                return "Ten";
            case Deck.Rank.THREE:
                return "Three";
            case Deck.Rank.TWO:
                return "Two";
            default:
                return "";
        }
    }

    private Sprite GetCardFront(Sprite[] fronts, Rank rank, Suit suit)
    {
        string rankString = Enum.GetName(typeof(Deck.Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Deck.Suit), suit).ToLower();

        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in fronts)
        {
            if (front.name == file)
            {
                return front;
            }
        }
        return null;
    }
    #endregion

    #region old stuff
    /*
    public GameObject CardPrefab;
    public List<Card> cards = new List<Card>();
    public float StackOffset;
    public Sprite back;
    public OnCardClickedHandler OnCardClicked;

    public delegate void OnCardClickedHandler(Card card);

    private RectTransform _deck = null;
    private static System.Random rng = new System.Random();
    Sprite[] fronts;
    void Awake()
    {
        fronts = Resources.LoadAll<Sprite>("cards_classic");

        Canvas canvas = FindObjectsOfType<Canvas>().Where(canvass => canvass.name == "Main Canvas").First();

        _deck = canvas.GetComponentsInChildren<RectTransform>().Where(trans => trans.name == "Deck").First();
        LoadCards();
    }

    //debug only
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Shuffle();
        }
    }

    

    void LoadCards()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Card newCard = (Instantiate(CardPrefab) as GameObject).GetComponent<Card>();
                newCard.transform.SetParent(_deck);
                newCard.transform.localScale = Vector3.one;
                Sprite front = GetCardFront(rank, suit);
                newCard.Init(rank, suit, GetCardValue(rank), front, back);
                //subscribe to click event
                newCard.onClicked += new Card.OnClickedHandler(CardClickHandler);
                cards.Add(newCard);
            }
        }
        
    }

    /// <summary>
    /// Sets a list of cards in order horizontally with given horizontal offset as well as setting appropriate sibling index
    /// to given parent.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="offset"></param>
    /// <param name="cards"></param>
    void SetCardGroupPositions(RectTransform parent, float offset, List<Card> cards)
    {
       for(int i = 0; i < cards.Count; i++)
        {
            RectTransform card = cards[i].GetComponent<RectTransform>();
            card.SetSiblingIndex(i);
            card.offsetMin = new Vector2((offset * i), 0);
            card.offsetMax = new Vector2((offset * i), 0);
        }
    }

    void CardClickHandler(Card card)
    {
        //just bubble up the event to the manager to abstract logic
        if (OnCardClicked != null)
        {
            OnCardClicked(card);
        }
    }

    public void Shuffle()
    {
        //start at end of list
        int listIndex = cards.Count;
        while (listIndex > 1)
        {
            //working way to the beginning of list
            listIndex--;
            //get random int from 0(inclusive) to 1 + listIndex(exclusive)
            int randomIndex = rng.Next(listIndex + 1);
            //swap the random card with the current card on in list
            Card randomCard = cards[randomIndex];
            cards[randomIndex] = cards[listIndex];
            cards[listIndex] = randomCard;
        }
        //SetCardGroupPositions(deckPosition, StackOffset, cards);
    }

    uint GetCardValue(Rank rank)
    {
        uint pointValue = 0;
        switch (rank)
        {
            case Rank.JACK:
                pointValue = 10;
                break;
            case Rank.QUEEN:
                pointValue = 10;
                break;
            case Rank.KING:
                pointValue = 10;
                break;
            default:
                pointValue = (uint)rank;
                break;
        }
        return pointValue;
    }

    Sprite GetCardFront(Rank rank, Suit suit)
    {
        string rankString = Enum.GetName(typeof(Deck.Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Deck.Suit), suit).ToLower();

        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in fronts)
        {
            if (front.name == file)
            {
                return front;
            }
        }
        return null;
    }
    */
    #endregion
}

