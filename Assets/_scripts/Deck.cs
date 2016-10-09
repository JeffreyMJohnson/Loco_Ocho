using UnityEngine;
using System.Collections.Generic;
using System;

public class Deck
{

    public Deck()
    {
        CreateDeck();
    }

    public void AddCardToMiddle(Card card)
    {
        List<Card> temp = new List<Card>(_cards.ToArray());
        temp.Insert(temp.Count / 2, card);
        _cards = new Queue<Card>(temp);
    }

    public Card DealCard()
    {
        if (_cards.Count < 1)
        {
            return null;
        }
        return _cards.Dequeue();
    }

    public void Shuffle(int times)
    {
        List<Card> tempCards = new List<Card>(_cards);
        for (int i = 0; i < times; ++i)
        {
            for (int j = 0; j < tempCards.Count; ++j)
            {
                int randIndex = UnityEngine.Random.Range(0, tempCards.Count);
                Card temp = tempCards[j];
                tempCards[j] = tempCards[randIndex];
                tempCards[randIndex] = temp;
            }
        }
        _cards = new Queue<Card>(tempCards);
    }

    public Card[] ToArray()
    {
        return _cards.ToArray();
    }

    private const int TOTAL_CARD_COUNT = 52;
    private Queue<Card> _cards = null;
    private GameObject _cardPrefab = null;

    private void CreateDeck()
    {
        if (_cards == null)
        {
            _cards = new Queue<Card>(TOTAL_CARD_COUNT);
        }
        if (_cardPrefab == null)
        {
            _cardPrefab = Resources.Load<GameObject>(@"prefabs/Card");
        }

        Sprite[] fronts = Resources.LoadAll<Sprite>("cards_classic");
        if (fronts == null)
        {
            Debug.LogError("Could not find card front sprites. Are they in the Resources folder?");
            return;
        }
        foreach (Card.Card_Suit suit in Enum.GetValues(typeof(Card.Card_Suit)))
        {
            foreach (Card.Card_Rank rank in Enum.GetValues(typeof(Card.Card_Rank)))
            {
                Card newCard = GameObject.Instantiate<GameObject>(_cardPrefab).GetComponent<Card>();
                //newCard.transform.position = Vector3.zero;
                newCard.Init(rank, suit, GetCardFront(fronts, rank, suit));
                _cards.Enqueue(newCard);
            }
        }
    }
    private Sprite GetCardFront(Sprite[] fronts, Card.Card_Rank rank, Card.Card_Suit suit)
    {
        string rankString = Enum.GetName(typeof(Card.Card_Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Card.Card_Suit), suit).ToLower();

        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in fronts)
        {
            if (front.name == file)
            {
                return front;
            }
        }
        Debug.LogError("Did not find sprite: " + Enum.GetName(typeof(Card.Card_Rank), rank) + " of " + Enum.GetName(typeof(Card.Card_Suit), suit));
        return null;
    }
}
