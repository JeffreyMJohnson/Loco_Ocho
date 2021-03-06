﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//todo this does not need to be monobehaviour 
public class DiscardPile : MonoBehaviour
{
    public Card TopCard { get { return _cards.Peek(); } }
    public Card.Card_Rank CurrentRank { get; private set; }
    public Card.Card_Suit CurrentSuit { get; private set; }
    public bool CurrentIsWild { get; private set; }


    public void AddCard(Card card, Card.Card_Suit wildSuit = Card.Card_Suit.CLUBS)
    {
        if (_cards == null)
        {
            _cards = new Stack<Card>();
        }

        card.transform.SetParent(transform, false);

        RectTransform cardRecTransform = card.GetComponent<RectTransform>();
        RectTransform pileRectTransform = gameObject.GetComponent<RectTransform>();

        //non stretching
        cardRecTransform.sizeDelta = pileRectTransform.sizeDelta;
        cardRecTransform.anchoredPosition = Vector2.zero;
        cardRecTransform.anchorMin = pileRectTransform.anchorMin;
        cardRecTransform.anchorMax = pileRectTransform.anchorMax;
        cardRecTransform.pivot = pileRectTransform.pivot;

        _cards.Push(card);
        CurrentRank = card.Rank;
        if (card.IsWild)
        {
            CurrentSuit = wildSuit;
        }
        else
        {
            CurrentSuit = card.Suit;
            
        }
        CurrentIsWild = card.IsWild;
    }

    public bool IsValidMatch(Card card)
    {
        if (card.IsWild) return true;
        return CurrentSuit == card.Suit || CurrentRank == card.Rank;
    }

    private Stack<Card> _cards;
    //private Image _cardImage;

}
