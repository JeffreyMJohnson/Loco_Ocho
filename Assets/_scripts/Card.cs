using UnityEngine;
using UnityEngine.UI;
using System;


public class Card : MonoBehaviour, IEquatable<Card>
{
    public Deck.Rank Rank { get; private set; }
    public Deck.Suit Suit { get; private set; }
    public uint Value { get; private set; }
    public Sprite Front { get; private set; }

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }


    public void Init(Deck.Rank a_rank, Deck.Suit a_suit, uint a_pointValue, Sprite a_front)
    {
        Rank = a_rank;
        Suit = a_suit;
        Value = a_pointValue;
        Front = a_front;
        image.sprite = Front;
    }

    public bool Equals(Card other)
    {
        if (other == null)
            return false;

        if (this.Rank == other.Rank && this.Suit == other.Suit && this.Value == other.Value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
            return false;

        Card cardObj = obj as Card;
        if (cardObj == null)
        {
            return false;
        }
        else
        {
            return Equals(cardObj);
        }

    }

    public override int GetHashCode()
    {
        return this.Rank.GetHashCode() ^ this.Suit.GetHashCode() ^ this.Value.GetHashCode();
    }

    public static bool operator ==(Card card1, Card card2)
    {
        if (((object)card1) == null || ((object)card2) == null)
        {
            return System.Object.Equals(card1, card2);
        }
        return card1.Equals(card2);
    }

    public static bool operator !=(Card card1, Card card2)
    {
        if (((object)card1) == null || ((object)card2) == null)
        {
            return !System.Object.Equals(card1, card2);
        }
        return !(card1.Equals(card2));
    }
}
