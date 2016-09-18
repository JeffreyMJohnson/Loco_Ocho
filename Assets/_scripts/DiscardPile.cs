using UnityEngine;
using System.Collections.Generic;

public class DiscardPile : MonoBehaviour
{
    public void AddCard(Card card)
    {
        if (_cards == null)
        {
            _cards = new Stack<Card>();
        }
        _cards.Push(card);
    }

    public Card PeekTopCard()
    {
        return _cards.Peek();
    }

    private Stack<Card> _cards;


}
