﻿using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Old_Stuff
{
    public class Card
    {
        public Deck.Rank Rank { get; private set; }
        public Deck.Suit Suit { get; private set; }
        public Sprite Front { get; private set; }

        public Card(Deck.Suit suit, Deck.Rank rank, string suitAsString, string rankAsString, Sprite front)
        {
            Suit = suit;
            Rank = rank;
            this.suitAsString = suitAsString;
            this.rankAsString = rankAsString;
            Front = front;
        }


        private readonly string rankAsString;
        private readonly string suitAsString;

        public override string ToString()
        {
            return String.Format("{0} of {1}", rankAsString, suitAsString);
        }




        #region old stuff

        /*
    public uint Value { get; private set; }
    public Sprite Front { get; private set; }
    public Sprite Back { get; private set; }
    public bool IsFrontFacing { get { return image.sprite == Front; } }
    public OnClickedHandler onClicked;

    Image image;
    




    void Awake()
    {
        image = GetComponent<Image>();
    }

    

    public void Init(Deck.Rank a_rank, Deck.Suit a_suit, uint a_pointValue, Sprite a_front, Sprite a_back)
    {
        Rank = a_rank;
        Suit = a_suit;
        Value = a_pointValue;
        Front = a_front;
        Back = a_back;
        image.sprite = Front;
        rankAsString = GetRank(a_rank);
        suitAsString = GetSuit(a_suit);
    }

    public void Flip()
    {
        image.sprite = IsFrontFacing ? Back : Front;
    }

    public delegate void OnClickedHandler(Card card);

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClicked != null)
        {
            onClicked(this);
        }
    }

    #region equality overrides
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
    public override int GetHashCode()
    {
        return this.Rank.GetHashCode() ^ this.Suit.GetHashCode() ^ this.Value.GetHashCode();
    }
    #endregion
*/

        #endregion
    }
}
