using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;



public class Deck : MonoBehaviour
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

    public GameObject CardPrefab;
    public List<Card> cards = new List<Card>();
    public float StackOffset;

    private static System.Random rng = new System.Random();
    Sprite[] fronts;
    RectTransform deckPosition;
    void Awake()
    {
        fronts = Resources.LoadAll<Sprite>("cards_classic");
        
        Canvas canvas = FindObjectOfType<Canvas>();
        deckPosition = GameObject.Find("DeckPosition").GetComponent<RectTransform>();
        LoadCards();
        //transform.parent = deckPosition;
    }

    void LoadCards()
    {
        float currentOffset = 0;
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Card newCard = (Instantiate(CardPrefab) as GameObject).GetComponent<Card>();
                newCard.transform.SetParent(deckPosition);
                newCard.transform.localScale = Vector3.one;
                RectTransform rectr = newCard.GetComponent<RectTransform>();
                float width = rectr.rect.width;
                rectr.offsetMax = new Vector2(currentOffset + width, 0); 
                rectr.offsetMin = new Vector2(currentOffset, 0);
                currentOffset += StackOffset;
                Sprite front = GetCardFront(rank, suit);
                newCard.Init(rank, suit, GetCardValue(rank), front);
                cards.Add(newCard);
                
            }
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
}

